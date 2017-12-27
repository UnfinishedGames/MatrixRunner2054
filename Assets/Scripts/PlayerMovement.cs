using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Node currentNode;
    public LevelManager levelManager;
    public PlayerCharacterSheet characterSheet;

    private bool hasMoved = false;
    private bool canMove = true;
    private PlayerKeyInteractions keyInteractions;
    private Node nextNode;
    private float timeSinceLastMovement = 1.0f;
    private float moveInterval = 1.0f; // The Intervall in which the player can move

    public Node startNode { get; private set; }

    void Start()
    {
        Debug.Log("Starting Player");
        startNode = currentNode;
        keyInteractions = new PlayerKeyInteractions(levelManager);
    }

    public bool Action()
    {
        bool done = false;
        if (!canMove)
        {
            return false;
        }
        hasMoved = keyInteractions.CheckKeyUp(hasMoved, ref done);
        if (!hasMoved)
        {
            currentNode = keyInteractions.SetNodeForArrowInput(currentNode);
            hasMoved = keyInteractions.CheckKeyDown(hasMoved);
            UpdateView();
        }
        keyInteractions.PerformSpaceKeyInteraction(currentNode);
        return done;
    }

    public void Stay()
    {
        canMove = false;
    }

    public void GoOn()
    {
        canMove = true;
    }

    public void SetToStart()
    {
        currentNode = startNode;
    }

    private void UpdateView()
    {
        if (currentNode != null)
        {
            transform.position = currentNode.transform.position;
        }
    }
}
