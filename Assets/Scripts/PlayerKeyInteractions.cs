using System;
using UnityEngine;

internal class PlayerKeyInteractions
{
    private LevelManager levelManager;

    Func<KeyCode, bool> KeyWasReleased = (KeyCode input) => Input.GetKeyUp(input);
    Func<KeyCode, bool> KeyWasPressed = (KeyCode input) => Input.GetKeyDown(input);
    
    public PlayerKeyInteractions(LevelManager manager)
    {
        levelManager = manager;
    }

    internal bool CheckKeyUp(bool moved)
    {
        var keyUpHappened =
            KeyWasReleased(KeyCode.LeftArrow) ||
            KeyWasReleased(KeyCode.RightArrow) ||
            KeyWasReleased(KeyCode.UpArrow) ||
            KeyWasReleased(KeyCode.DownArrow);
        return keyUpHappened ? false : moved;
    }

    internal bool CheckKeyDown(bool moved)
    {
        var keyDownHappened =
            KeyWasPressed(KeyCode.LeftArrow) ||
            KeyWasPressed(KeyCode.RightArrow) ||
            KeyWasPressed(KeyCode.UpArrow) ||
            KeyWasPressed(KeyCode.DownArrow);
        return keyDownHappened ? true : moved;
    }
    
    internal Node SetNodeForArrowInput(Node sourceNode)
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            return levelManager.TryToMovePlayer(Direction.Left, sourceNode);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            return levelManager.TryToMovePlayer(Direction.Right, sourceNode);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            return levelManager.TryToMovePlayer(Direction.Up, sourceNode);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            return levelManager.TryToMovePlayer(Direction.Down, sourceNode);
        }
        return sourceNode;
    }

    internal void PerformSpaceKeyInteraction(Node currentNode)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            levelManager.TryToInteractWithNode(currentNode);
        }
    }
}