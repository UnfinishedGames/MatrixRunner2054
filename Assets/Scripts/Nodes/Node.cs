using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Node : MonoBehaviour {
    public GameObject leftObject;
    public GameObject rightObject;
    public GameObject upObject;
    public GameObject downObject;

    public Dictionary<Direction, Node> possibleDirections = null;
    private SpriteRenderer mySprite;
    private DataStoreContents myContents;
    private IOPortConnections myConnection;
    private State currentState = State.Initial;

    void Start()
    {
        mySprite = GetComponentInChildren<SpriteRenderer>();
        myContents = GetComponentInChildren<DataStoreContents>();
        myConnection = GetComponentInChildren<IOPortConnections>();
    }

    void Update()
    {
        InitializeForEditor();
        UpdateView();
    }

    private void InitializeForEditor()
    {
        if (possibleDirections == null)
        {
            possibleDirections = new Dictionary<Direction, Node>();

            possibleDirections.Add(Direction.Left, NodeFromObjectOrMyself(leftObject));
            possibleDirections.Add(Direction.Right, NodeFromObjectOrMyself(rightObject));
            possibleDirections.Add(Direction.Up, NodeFromObjectOrMyself(upObject));
            possibleDirections.Add(Direction.Down, NodeFromObjectOrMyself(downObject));
        }
    }

    public bool CanInteract()
    {
        return currentState != State.Hacked;
    }

    public void SwitchState(State newState)
    {
        currentState = newState;
    }

    public Node getNeighbour(Direction direction)
    {
        return possibleDirections[direction];
    }

    private Node NodeFromObjectOrMyself(GameObject gameObj)
    {
        return gameObj != null ? gameObj.GetComponent<Node>() : this;
    }

    private void UpdateView()
    {
        switch (currentState)
        {
        case State.Initial:
            mySprite.color = Color.white;
            break;
        case State.Hacked:
            mySprite.color = Color.green;
            break;
        default:
            throw new ArgumentOutOfRangeException("currentState");
        }
    }

    internal void InteractWithPlayer(PlayerCharacterSheet playerCharacterSheet, MissionManager missionManager)
    {
        if (myContents != null)
        {
            myContents.Interact(playerCharacterSheet, missionManager);
        }
        if (myConnection != null)
        {
            myConnection.Interact(missionManager);
        }
    }
}
