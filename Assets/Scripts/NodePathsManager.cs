using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework.Interfaces;
using NUnit.Framework;

/// <summary>
/// Draws the paths between the nodes.
/// 
/// The NodeManager must be attached to some GameObject that also contains a "path"-Object.
/// The "path" object only contains a line renderer. The "path" object will be 
/// duplicated dynamically for each drawn line.
/// 
/// Optimizations:
/// * draw only when a node was moved.
/// </summary>

[ExecuteInEditMode]
public class NodePathsManager : MonoBehaviour {

    public GameObject path;
   
    private Dictionary<Node, List<Node> > pathStorage = new Dictionary<Node, List<Node> >();

    void Start()
    {
        ClearDrawnPaths();
    }

    void Update()
    {
        ClearDrawnPaths();
        DrawPaths();
    }

    private void ClearDrawnPaths()
    {
        // We need to clean up the root, since the running mode and editor mode seem to use different members...
        GameObject[] rootGameObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject gameObject in rootGameObjects)
        {
            if (gameObject.name == "path(Clone)")
            {
                DestroyImmediate(gameObject);
            }
        }

        foreach (var myList in pathStorage.Values)
        {
            myList.Clear();
        }
        pathStorage.Clear();
    }

    private void DrawPaths()
    {
        GameObject[] rootGameObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject gameObject in rootGameObjects)
        {
            Node foundNode = gameObject.GetComponent<Node>();
            if (foundNode)
            {
                DrawSinglePath(foundNode); 
            }
        }
    }

    private void DrawSinglePath(Node currentNode)
    {
        if (currentNode.possibleDirections != null) // Sometimes the node is not yet fully created
        {
            foreach (var other in currentNode.possibleDirections.Values)
            {
                if (other && (other != currentNode) && !LineAlreadyDrawn(other, currentNode))
                {
                    Vector3 opponentPosition = other.transform.position;
                    Vector3 myPosition = currentNode.transform.position;
                    DrawLine(opponentPosition, myPosition);
                    UpdatePathStorage(currentNode, other);
                }
            }
        }
    }

    private void DrawLine(Vector3 startPosition, Vector3 endPosition)
    {
        GameObject newPath = Instantiate(path);
        LineRenderer newLineRenderer = newPath.GetComponent<LineRenderer>();
        newLineRenderer.enabled = true;
        newPath.hideFlags = HideFlags.HideInHierarchy | HideFlags.DontSave;
        Vector3[] points = new Vector3[2]{ startPosition, endPosition };
        newLineRenderer.SetPositions(points); 
    }

    private bool LineAlreadyDrawn(Node other, Node currentNode)
    {
        bool result = false;
        if (pathStorage.ContainsKey(other))
        {
            if (pathStorage[other] != null && pathStorage[other].Contains(currentNode))
            {
                result = true;
            }
        }
        return result;
    }

    private void UpdatePathStorage(Node currentNode, Node other)
    {
        if (!pathStorage.ContainsKey(currentNode))
        {
            pathStorage.Add(currentNode, new List<Node>());
        }
        pathStorage[currentNode].Add(other);
    }
}
