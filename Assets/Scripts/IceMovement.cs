using System;
using UnityEngine;

public class IceMovement : IceLocation {
    const int NUMBER_OF_DIRECTIONS = 4;
    const int ZERO = 0;
    const float INTERVAL = 1.0f;
    const string NAME_OF_MOVE_FUNC = "Move";

    private System.Random random;

    void Start()
    {
        InitializeForStartup();
        InvokeRepeating(NAME_OF_MOVE_FUNC, INTERVAL, INTERVAL);
        random = new System.Random((int)DateTime.Now.Ticks);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Stay()
    {
        CancelInvoke(NAME_OF_MOVE_FUNC);
    }

    public void Reset(Node startNode)
    {
        InvokeRepeating(NAME_OF_MOVE_FUNC, INTERVAL, INTERVAL);
        currentNode = startNode;
        UpdateView();
    }

    private void Move()
    {
        Node newNode = currentNode;

        while (currentNode == newNode)
        {
            var newDirection = random.Next(ZERO, NUMBER_OF_DIRECTIONS);
            newNode = currentNode.getNeighbour((Direction)newDirection);
        }
        currentNode = newNode;
        UpdateView();
    }
}
