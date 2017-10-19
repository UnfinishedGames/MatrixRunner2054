using System;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    public GameObject leftObject;
    public GameObject rightObject;
    public GameObject upObject;
    public GameObject downObject;

    private Dictionary<Direction, Node> possibleDirections;
    // Use this for initialization
    void Start()
    {
        possibleDirections = new Dictionary<Direction, Node>();

        possibleDirections.Add(Direction.Left, NodeFromObjectOrMyself(leftObject));
        possibleDirections.Add(Direction.Right, NodeFromObjectOrMyself(rightObject));
        possibleDirections.Add(Direction.Up, NodeFromObjectOrMyself(upObject));
        possibleDirections.Add(Direction.Down, NodeFromObjectOrMyself(downObject));
    }

    private Node NodeFromObjectOrMyself(GameObject gameObj)
    {
        return gameObj != null ? gameObj.GetComponent<Node>() : this;
    }
    	
	// Update is called once per frame
	void Update () {
		
	}

    internal Node getNeighbour(Direction direction)
    {
        return possibleDirections[direction];
    }
}
