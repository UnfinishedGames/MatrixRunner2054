using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Node currentNode;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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

        UpdateView();
    }

    private void UpdateView()
    {
        if (currentNode != null)
        {
            this.transform.position = currentNode.transform.position;
        }
    }
}
