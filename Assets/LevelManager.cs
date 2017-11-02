using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public PlayerMovement currentPlayer;
    public IceMovement ice;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Node TryToMovePlayer(Direction newDirection, Node currentNode)
    {
        // tell AI enemies they can move
        ice.YouCanMoveNext();

        return currentNode.getNeighbour(newDirection);
    }
}
