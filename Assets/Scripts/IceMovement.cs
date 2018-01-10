using System;

public class IceMovement : IceLocation {
    const int NUMBER_OF_DIRECTIONS = 4;
    const int ZERO = 0;
    const float INTERVAL = 1.0f;
    const string NAME_OF_MOVE_FUNC = "Move";

    private System.Random random;
    private bool canMove;

    void Start()
    {
        InitializeForStartup();
        random = new Random((int)DateTime.Now.Ticks);
    }

    void Update()
    {

    }

    public void GoOn()
    {
        canMove = true;
    }

    public void Reset(Node startNode)
    {
        currentNode = startNode;
        UpdateView();
    }

    public void Move()
    {
        if (!canMove)
        {
            return;
        }

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
