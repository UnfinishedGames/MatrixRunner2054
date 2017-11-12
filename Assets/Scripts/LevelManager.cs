using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private const byte INTERACTION_SLIDER_INTERVAL = 40;
    private const byte INTERACTION_SLIDER_START = 0;
    private const byte INTERACTION_SLIDER_MAX = 100;

    public PlayerMovement player;
    public IceMovement ice;
    public Slider interactionTimeElapsed;
    public RectTransform actionIndicator;

    void Start()
    {
        actionIndicator.gameObject.SetActive(false);
    }

    void Update()
    {
        if (PlayerAndIceAreInTheSameNode())
        {
            ice.Fight(player, actionIndicator);
        }
    }

    public Node TryToMovePlayer(Direction newDirection, Node currentNode)
    {
        ResetInteractionSlider();
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
        interactionTimeElapsed.value += (Time.deltaTime * INTERACTION_SLIDER_INTERVAL);
        if (interactionTimeElapsed.value >= INTERACTION_SLIDER_MAX)
        {
            interactionTimeElapsed.value = INTERACTION_SLIDER_START;
            currentNode.SwitchState(State.Hacked);
        }
    }

    private bool PlayerAndIceAreInTheSameNode()
    {
        return player.currentNode == ice.currentNode;
    }

    private void ResetInteractionSlider()
    {
        interactionTimeElapsed.value = INTERACTION_SLIDER_START;
    }
}
