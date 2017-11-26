using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NodePathsManager : MonoBehaviour {

    public GameObject path;

    private List<GameObject> lineRendererContainer = new List<GameObject>();
    private List<Node> nodeList = new List<Node>();

    void Start()
    {
        
    }

    void Update()
    {
        UpdateNodeList();
        ClearDrawnPaths();
        foreach (Node node in nodeList)
        {
            DrawPath(node);
        }
    }

    private void UpdateNodeList()
    {
        GameObject[] rootGameObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject gameObject in rootGameObjects)
        {
            Node foundNode = gameObject.GetComponent<Node>();
            if (foundNode && !nodeList.Contains(foundNode))
            {
                nodeList.Add(foundNode); 
            }
        }
    }

    private void ClearDrawnPaths()
    {
        foreach (GameObject renderer in lineRendererContainer)
        {
            DestroyImmediate(renderer);
        }
        lineRendererContainer.Clear();
    }

    private bool DrawPath(Node myNode)
    {
        bool result = true;
        try
        {
            foreach (Node otherNode in myNode.possibleDirections.Values)
            {
                if (otherNode && (otherNode != this))
                {
                    Vector3 opponentPosition = otherNode.transform.position;
                    Vector3 myPosition = myNode.transform.position;
                    drawLine(opponentPosition, myPosition);
                }
            }
        }
        catch
        {
            result = false;
        }
        return result;
    }

    private void drawLine(Vector3 startPosition, Vector3 endPosition)
    {
        GameObject newPath = Instantiate(path);
        LineRenderer newLineRenderer = newPath.GetComponent<LineRenderer>();
        this.lineRendererContainer.Add(newPath);
        //        newPath.hideFlags = HideFlags.HideInHierarchy;
        newLineRenderer.enabled = true;
        Vector3[] points = new Vector3[2]{ startPosition, endPosition };
        newLineRenderer.SetPositions(points); 
    }

}
