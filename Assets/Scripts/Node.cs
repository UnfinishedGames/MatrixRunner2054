using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Node : MonoBehaviour {
    public GameObject leftObject;
    public GameObject rightObject;
    public GameObject upObject;
    public GameObject downObject;

    private Dictionary<Direction, Node> possibleDirections;
    private SpriteRenderer mySprite;
    private State currentState = State.Initial;

    void Start()
    {
        possibleDirections = new Dictionary<Direction, Node>();

        possibleDirections.Add(Direction.Left, NodeFromObjectOrMyself(leftObject));
        possibleDirections.Add(Direction.Right, NodeFromObjectOrMyself(rightObject));
        possibleDirections.Add(Direction.Up, NodeFromObjectOrMyself(upObject));
        possibleDirections.Add(Direction.Down, NodeFromObjectOrMyself(downObject));

        mySprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (this.transform.hasChanged)
        {
        }
        UpdateView();
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
}
