using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Node currentNode;
    private bool hasMoved = false;

    private Dictionary<KeyCode, Direction> translateKey = new Dictionary<KeyCode, Direction>
    {
        {KeyCode.UpArrow, Direction.Up },
        {KeyCode.DownArrow, Direction.Down },
        {KeyCode.LeftArrow, Direction.Left },
        {KeyCode.RightArrow, Direction.Right }
    };

    Func<KeyCode, bool> KeyWasReleased = (KeyCode input) => Input.GetKeyUp(input);
    Func<KeyCode, bool> KeyWasPressed = (KeyCode input) => Input.GetKeyDown(input);

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        hasMoved = CheckKeyUp(hasMoved);
        if (hasMoved) { return; }
        SetCurrentNode();
        hasMoved = CheckKeyDown(hasMoved);
        UpdateView();
    }

    private bool CheckKeyUp(bool moved)
    {
        var keyUpHappened =
            KeyWasReleased(KeyCode.LeftArrow) ||
            KeyWasReleased(KeyCode.RightArrow) ||
            KeyWasReleased(KeyCode.UpArrow) ||
            KeyWasReleased(KeyCode.DownArrow);
        return keyUpHappened ? false : moved;
    }

    private bool CheckKeyDown(bool moved)
    {
        var keyDownHappened =
            KeyWasPressed(KeyCode.LeftArrow) ||
            KeyWasPressed(KeyCode.RightArrow) ||
            KeyWasPressed(KeyCode.UpArrow) ||
            KeyWasPressed(KeyCode.DownArrow);
        return keyDownHappened ? true : moved;
    }

    private void SetCurrentNode()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentNode = currentNode.getNeighbour(Direction.Left);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentNode = currentNode.getNeighbour(Direction.Right);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentNode = currentNode.getNeighbour(Direction.Up);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            currentNode = currentNode.getNeighbour(Direction.Down);
        }
    }
    
    private void UpdateView()
    {
        if (currentNode != null)
        {
            transform.position = currentNode.transform.position;
        }
    }
}
