using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Node))]
[CanEditMultipleObjects]
public class NodeEditor : Editor
{
    private Dictionary<KeyCode, bool> pressedKeys = new Dictionary<KeyCode, bool>();

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    private void OnSceneGUI()
    {
        Event myEvent = Event.current;
        bool checkKeys = false;
        switch (myEvent.type)
        {
            case EventType.KeyDown:
            {
                if (pressedKeys.ContainsKey(Event.current.keyCode))
                {
                    if (!pressedKeys[Event.current.keyCode])
                    {
                        pressedKeys[Event.current.keyCode] = true;
                        checkKeys = true;
                    }
                }
                else
                {
                    pressedKeys[Event.current.keyCode] = true;
                }
                break;
            }
            case EventType.KeyUp:
            {
                pressedKeys[Event.current.keyCode] = false;
                break;
            }
            default:
            {
                break;
            }
        }

        if (checkKeys
            && pressedKeys.ContainsKey(KeyCode.RightControl) 
            && pressedKeys.ContainsKey(KeyCode.C))
        {
            if (pressedKeys[KeyCode.RightControl] && pressedKeys[KeyCode.C])
            {
                Debug.Log("got it");
                if (Selection.transforms.Length == 2
                    && AreNodes(Selection.transforms))
                {
                    ConnectNodes(Selection.transforms);
                }
            }
        }
    }

    private void ConnectNodes(Transform[] transforms)
    {
        var distanceX = transforms[0].position.x - transforms[1].position.x;
        var distanceY = transforms[0].position.y - transforms[1].position.y;

        if (Math.Abs(distanceX) > Math.Abs(distanceY))
        {
            ConnectLeftRightNodes(transforms);
        }
        else
        {
            ConnectUpDownNodes(transforms);
        }
    }

    private void ConnectLeftRightNodes(Transform[] transforms)
    {
        var node0 = transforms[0].GetComponent<Node>();
        var node1 = transforms[1].GetComponent<Node>();

        if (transforms[0].position.x > transforms[1].position.x)
        {
            node0.leftObject = transforms[1].gameObject;
            node1.rightObject = transforms[0].gameObject;
        }
        else
        {
            node0.rightObject = transforms[1].gameObject;
            node1.leftObject = transforms[0].gameObject;
        }
    }

    private void ConnectUpDownNodes(Transform[] transforms)
    {
        var node0 = transforms[0].GetComponent<Node>();
        var node1 = transforms[1].GetComponent<Node>();

        if (transforms[0].position.y > transforms[1].position.y)
        {
            node0.downObject = transforms[1].gameObject;
            node1.upObject = transforms[0].gameObject;
        }
        else
        {
            node0.upObject = transforms[1].gameObject;
            node1.downObject = transforms[0].gameObject;
        }
    }

    private bool AreNodes(Transform[] transforms)
    {
        bool areNodes = false;
        foreach (var currentTransform in transforms)
        {
            Node node = currentTransform.GetComponent<Node>();
            if (node != null)
            {
                areNodes = true;
            }
            else
            {
                areNodes = false;
                break;
            }
        }

        return areNodes;
    }
}