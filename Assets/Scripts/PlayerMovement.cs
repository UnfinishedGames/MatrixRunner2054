using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public Node currentNode;
    public LevelManager levelManager;

    private bool hasMoved = false;
    private bool canMove = true;
    private PlayerKeyInteractions keyInteractions;

    public Node startNode { get; private set; }

    void Start()
    {
        Debug.Log("Starting Player");
        startNode = currentNode;
        keyInteractions = new PlayerKeyInteractions(levelManager);
    }

    void Update()
    {
        if (!canMove)
        {
            return;
        }
        hasMoved = keyInteractions.CheckKeyUp(hasMoved);
        if (!hasMoved)
        {
            currentNode = keyInteractions.SetNodeForArrowInput(currentNode);
            hasMoved = keyInteractions.CheckKeyDown(hasMoved);
            UpdateView();
        }
        keyInteractions.PerformSpaceKeyInteraction(currentNode);
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
