using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePathBehaviour : MonoBehaviour {

    public GameObject path;

    public bool pathDrawn { set; private get; }

    /* Used to indicate the last position the path was drawn */
    private Vector3 lastPosition = new Vector3(0, 0, 0);
    private List<GameObject> lineRendererContainer = new List<GameObject>();

    protected void InitializePathBehavoir()
    {
    }

    public void PathUpdate(Node myNode)
    {
        if (myNode.transform.position != lastPosition)
        {
            lastPosition = myNode.transform.position;
            DrawPath(myNode);
        }
    }

    private void DrawPath(Node myNode)
    {
        foreach (GameObject renderer in lineRendererContainer)
        {
            DestroyImmediate(renderer);
        }
        lineRendererContainer.Clear();

        foreach (Node otherNode in myNode.possibleDirections.Values)
        {
            if (otherNode && (otherNode != this))
            {
                Vector3 opponentPosition = otherNode.transform.position;
                Vector3 myPosition = transform.position;
                drawLine(opponentPosition, myPosition);
            }
        }
        pathDrawn = true;
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
