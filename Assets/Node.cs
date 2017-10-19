using System;
using UnityEngine;

public class Node : MonoBehaviour {

    public GameObject leftObject;
    public GameObject rightObject;
    public GameObject upObject;
    public GameObject downObject;

    private Node leftNode;
    private Node rightNode;
    private Node upNode;
    private Node downNode;

    // Use this for initialization
    void Start()
    {
        if (leftObject != null)
        {
            leftNode = leftObject.GetComponent<Node>();
        }
        if (rightObject != null)
        {
            rightNode = rightObject.GetComponent<Node>();
        }
        if (upObject != null)
        {
            upNode = upObject.GetComponent<Node>();
        }
        if (downObject != null)
        {
            downNode = downObject.GetComponent<Node>();
        }
    }
    	
	// Update is called once per frame
	void Update () {
		
	}

    internal Node getNeighbour(Direction direction)
    {
        Node returnNode = this;
        switch(direction)
        {
            case Direction.Left:
                {
                    returnNode = leftNode ?? this;
                    break;
                }
            case Direction.Right:
                {
                    returnNode = rightNode ?? this;
                    break;
                }
            case Direction.Up:
                {
                    returnNode = upNode ?? this;
                    break;
                }
            case Direction.Down:
                {
                    returnNode = downNode ?? this;
                    break;
                }
        }
        return returnNode;
    }
}
