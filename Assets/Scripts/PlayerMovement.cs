using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Node currentNode;
    public LevelManager levelManager;

    private bool hasMoved = false;
    private bool canMove = true;

    Func<KeyCode, bool> KeyWasReleased = (KeyCode input) => Input.GetKeyUp(input);
    Func<KeyCode, bool> KeyWasPressed = (KeyCode input) => Input.GetKeyDown(input);

    void Start()
    {
    }
    
    void Update()
    {
        if (!canMove)
        {
            return;
        }
        hasMoved = CheckKeyUp(hasMoved);
        if (!hasMoved)
        {
            SetCurrentNode();
            hasMoved = CheckKeyDown(hasMoved);
            UpdateView();
        }
        CheckNodeInteraction();
    }

    public void Stay()
    {
        canMove = false;
    }

    public void GoOn()
    {
        canMove = true;
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
            currentNode = levelManager.TryToMovePlayer(Direction.Left, currentNode);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentNode = levelManager.TryToMovePlayer(Direction.Right, currentNode);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentNode = levelManager.TryToMovePlayer(Direction.Up, currentNode);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            currentNode = levelManager.TryToMovePlayer(Direction.Down, currentNode);
        }
    }

    private void UpdateView()
    {
        if (currentNode != null)
        {
            transform.position = currentNode.transform.position;
        }
    }

    private void CheckNodeInteraction()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            levelManager.TryToInteractWithNode(currentNode);
        }
    }
}
