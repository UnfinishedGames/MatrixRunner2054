using System;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerKeyInteractions
{
    private LevelManager levelManager;

    Func<KeyCode, bool> KeyWasReleased = (KeyCode input) => Input.GetKeyUp(input);
    
    public PlayerKeyInteractions(LevelManager manager)
    {
        levelManager = manager;
    }

    internal KeyCode CheckKeyUp()
    {
        var releasedKey = KeyCode.None;
        var keyList = new List<KeyCode>();
        keyList.Add(KeyCode.LeftArrow);
        keyList.Add(KeyCode.RightArrow);
        keyList.Add(KeyCode.UpArrow);
        keyList.Add(KeyCode.DownArrow);
        foreach(var keyCode in keyList)
        {
            if (KeyWasReleased(keyCode))
            {
                releasedKey = keyCode;
                break;
            }
        }
        return releasedKey;
    }
    
    internal Node SetNodeForArrowInput(Node sourceNode, KeyCode keyCode)
    {
        Direction direction;
        switch (keyCode)
        {
            case KeyCode.LeftArrow:
            {
                direction = Direction.Left;
                break;
            }
            case KeyCode.RightArrow:
            {
                direction = Direction.Right;
                break;
            }
            case KeyCode.UpArrow:
            {
                direction = Direction.Up;
                break;
            }
            case KeyCode.DownArrow:
            {
                direction = Direction.Down;
                break;
            }
            default:
            {
                return sourceNode;
            }
        }
        return levelManager.TryToMovePlayer(direction, sourceNode);
    }

    internal void PerformSpaceKeyInteraction(Node currentNode)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            levelManager.TryToInteractWithNode(currentNode);
        }
    }
}