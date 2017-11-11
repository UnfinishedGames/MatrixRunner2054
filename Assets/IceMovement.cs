using UnityEngine;

public class IceMovement : MonoBehaviour
{
    const int numberOfDirections = 3;
    
    public Node currentNode;

    private bool canMove = false;
    private System.Random random;
    
    void Start()
    {
        random = new System.Random((int)System.DateTime.Now.Ticks);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Move();
        }
    }
    
    public void YouCanMoveNext()
    {
        canMove = true;
    }

    private void Move()
    {
        var newDirection = random.Next(0, numberOfDirections + 1);
        currentNode = currentNode.getNeighbour((Direction)newDirection);
        canMove = false;
        UpdateView();
    }

    private void UpdateView()
    {
        if (currentNode != null)
        {
            transform.position = currentNode.transform.position;
        }
    }
}
