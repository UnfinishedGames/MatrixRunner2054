using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(Node))]
public class DrawLine : Editor
{
//    public Material mat;
//    private SerializedProperty node;

    private List<Node> nodeList = new List<Node>();
//    private List<KeyValuePair<Vector3, Vector3>> lineList = new List<KeyValuePair<Vector3, Vector3>>();

    private void OnEnable()
    {
//        node = serializedObject.FindProperty("Node");
    }

//    void Update()
//    {
//    }

//    public void SetLine(Vector3 startVector, Vector3 endVector)
//    {
//        lineList.Add(new KeyValuePair<Vector3, Vector3>(startVector, endVector));
//    }

    private void DrawConnection(Node node)
    {
        if (node && !nodeList.Contains(node))
        {
            nodeList.Add(node);
            if (node.leftObject)
            {
                Handles.DrawLine(node.transform.position, node.leftObject.transform.position);
                DrawConnection(node.leftObject.GetComponent<Node>());
            }

            if (node.rightObject)
            {
                Handles.DrawLine(node.transform.position, node.rightObject.transform.position);
                DrawConnection(node.rightObject.GetComponent<Node>());
            }

            if (node.downObject)
            {
                Handles.DrawLine(node.transform.position, node.downObject.transform.position);
                DrawConnection(node.downObject.GetComponent<Node>());
            }

            if (node.upObject)
            {
                Handles.DrawLine(node.transform.position, node.upObject.transform.position);
                DrawConnection(node.upObject.GetComponent<Node>());
            }
        }
    }

    public void OnSceneGUI()
//    public void OnGUI()
    {
        
//        if (!mat)
//        {
//            Debug.LogError("Please Assign a material on the inspector");
//            return;
//        }

//        foreach (KeyValuePair<Vector3, Vector3> line in lineList)
//        {
        Node myNode = (target as Node);
        nodeList.Clear();
        DrawConnection(myNode);

//            Handles.DrawLine(line.Key, line.Value);
//                GL.PushMatrix();
//                mat.SetPass(0);
//                GL.LoadOrtho();
//                GL.Begin(GL.LINES);
//
//                GL.Color(Color.red);
//                GL.Vertex(Camera.main.WorldToScreenPoint(line.Key));
//                GL.Vertex(Camera.main.WorldToScreenPoint(line.Value));
//                GL.End();
//                GL.PopMatrix();
//        }

//        lineList.Clear();
//        }
    }
}