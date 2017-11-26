using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePath : MonoBehaviour {

    public GameObject path;

    public bool pathDrawn { set; private get; }

    private Vector3 lastPosition = new Vector3(0, 0, 0);
    private List<GameObject> pathLine = new List<GameObject>();

    public void PathUpdate(Node myNode)
    {
        if (!myNode)
        {
            myNode = GetComponent<Node>();
        }

        if (myNode)
        {
            if (myNode.transform.position != lastPosition)
            {
                lastPosition = myNode.transform.position;
                pathDrawn = false;
            }
            if (!pathDrawn)
            {
                DrawPath(myNode);
            }
        }
    }

    private void DrawPath(Node myNode)
    {
        foreach (GameObject renderer in pathLine)
        {
//            LineRenderer newLineRenderer = renderer.GetComponent<LineRenderer>().enabled = false;
            DestroyImmediate(renderer);
        }
        pathLine.Clear();

        foreach (Node otherNode in myNode.possibleDirections.Values)
        {
            if (otherNode && (otherNode != this))
            {
                Vector3 opponentPosition = otherNode.transform.position;
                Vector3 myPosition = transform.position;
                drawLine(opponentPosition, myPosition);
                NodePath otherNodePath = otherNode.GetComponentInParent<NodePath>();
                if (otherNodePath)
                {
                    otherNodePath.pathDrawn = true;
                }
            }
        }
        pathDrawn = true;
    }

    private void drawLine(Vector3 startPosition, Vector3 endPosition)
    {
        GameObject newPath = Instantiate(path);
        LineRenderer newLineRenderer = newPath.GetComponent<LineRenderer>();
        this.pathLine.Add(newPath);
//        newPath.hideFlags = HideFlags.DontSaveInEditor;
        newLineRenderer.enabled = true;
        Vector3[] points = new Vector3[2]{ startPosition, endPosition };
        newLineRenderer.SetPositions(points);
    }
}
