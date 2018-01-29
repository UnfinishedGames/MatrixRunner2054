using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Node currentNode;
    public LevelManager levelManager;
    public PlayerCharacterSheet characterSheet;

    private bool canMove = true;
    private PlayerKeyInteractions keyInteractions;
    private Node nextNode;

    public Node startNode { get; private set; }
    
    void Start()
    {
        Debug.Log("Starting Player");
        startNode = currentNode;
        SetToStart();
        keyInteractions = new PlayerKeyInteractions(levelManager);
    }

    public bool Action()
    {
        var done = false;
        if (canMove)
        {
            var releasedKey = keyInteractions.CheckKeyUp();
            if (releasedKey != KeyCode.None)
            {
                currentNode = keyInteractions.SetNodeForArrowInput(currentNode, releasedKey);
                UpdateView();
                done = true;
            }
            keyInteractions.PerformSpaceKeyInteraction(currentNode); // TODO: auf done setzen, wenn Node gehackt
        }
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
