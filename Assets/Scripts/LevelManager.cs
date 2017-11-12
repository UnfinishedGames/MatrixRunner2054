using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    public PlayerMovement player;
    public IceMovement ice;
    public Slider interactionTimeElapsed;
    public Text attackAlert;
    public EncounterStub currentEncounter;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (player.currentNode == ice.currentNode)
        {
            ice.getEncounter.Fight(player, ice, attackAlert);
        }
        else
        {
            attackAlert.text = "";
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
        interactionTimeElapsed.value += (Time.deltaTime * 40);
        if (interactionTimeElapsed.value >= 100)
        {
            interactionTimeElapsed.value = 0;
            currentNode.SwitchState(State.Hacked);
        }
    }

    private void ResetInteractionSlider()
    {
        interactionTimeElapsed.value = 0;
    }
}
