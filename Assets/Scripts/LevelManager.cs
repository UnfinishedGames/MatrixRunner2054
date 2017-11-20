using MissionEngine;
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
    public RectTransform gameOverIndicator;
    public MissionManager missionManager;

    private Text gameOverText;
    
    void Start()
    {
        actionIndicator.gameObject.SetActive(false);
        gameOverIndicator.gameObject.SetActive(false);
        gameOverText = gameOverIndicator.GetComponentInChildren<Text>();

    }

    void Update()
    {
        QuitOnEscape();
        CheckVictoryConditions();
        CheckIfFightIsOn();
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
            missionManager.Inform(GameAction.NodeHacked);
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

    private void QuitOnEscape()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void CheckVictoryConditions()
    {
        var state = missionManager.CheckMissionState();
        if(state == MissionState.InProgress)
        {
            return;
        }
        if (state == MissionState.Failed)
        {
            gameOverIndicator.gameObject.SetActive(true);
            gameOverText.text = "You fail";
        }
        if (state == MissionState.Succeeded)
        {
            gameOverIndicator.gameObject.SetActive(true);
            gameOverText.text = "You succeed";
        }
    }

    private void CheckIfFightIsOn()
    {
        if (PlayerAndIceAreInTheSameNode())
        {
            ice.Fight(player, actionIndicator);
        }
    }
}
