using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public PlayerMovement currentPlayer;
    public IceMovement ice;
    public Slider interactionTimeElapsed;

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
        ice.YouCanMoveNext();
        return currentNode.getNeighbour(newDirection);
    }

    public void TryToInteractWithNode(Node currentNode)
    {
        if (!currentNode.CanInteract())
        {
            return;
        }
        UpdateInteractionSlider(currentNode);
    }

    private void UpdateInteractionSlider(Node currentNode)
    {
        interactionTimeElapsed.value += (Time.deltaTime * 40);
        if (interactionTimeElapsed.value >= 100)
        {
            interactionTimeElapsed.value = 0;
            currentNode.SwitchState(State.Hacked);
        }
    }
}
