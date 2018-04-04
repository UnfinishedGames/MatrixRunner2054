using System;
using MissionEngine;
using UnityEngine;
using UnityEngine.UI;

enum GameState {
    GAMESTATE_PLAYER_ACTION = 0,
    GAMESTATE_ICE_ACTION,
}

public class LevelManager : MonoBehaviour
{
    public PlayerMovement player;
    public IceLocation[] countermeasures;
    public RectTransform actionIndicator;
    public RectTransform gameOverIndicator;
    public MissionManager missionManager;
    public RectTransform moneyIndicator;

    private Text gameOverText;
    private Text moneyIndicatorText;
    private GameState gameState;
    private PlayerCharacterSheet playerCharacterSheet;

    void Start()
    {
        actionIndicator.gameObject.SetActive(false);
        gameOverIndicator.gameObject.SetActive(false);
        gameOverText = gameOverIndicator.GetComponentInChildren<Text>();
        playerCharacterSheet = player.GetComponentInChildren<PlayerCharacterSheet>();
        moneyIndicatorText = moneyIndicator.GetComponentInChildren<Text>();
    }

    void Update()
    {
        CheckState();
        QuitOnEscape();
        CheckVictoryConditions();
        CheckIfFightIsOn();
    }
    public Node TryToMovePlayer(Direction newDirection, Node currentNode)
    {
        var newCurrentNode = currentNode.getNeighbour(newDirection);
        //give the persistentState the new current node
        return newCurrentNode;
    }

    public void TryToInteractWithNode(Node currentNode)
    {
        if (!currentNode.CanInteract())
        {
            return;
        }
        currentNode.Interact(player);
    }

    internal void UncoverNeighbourNode()
    {
        UncoverNeighbourNodes();
    }

    private bool PlayerAndIceAreInTheSameNode(PlayerMovement player, IceLocation ice)
    {
        return player.currentNode == ice.currentNode;
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
        if (state == MissionState.InProgress)
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
        foreach (IceLocation ice in countermeasures)
        {
            if (PlayerAndIceAreInTheSameNode(player, ice))
            {
                ice.Interact(player);
            }
        }
    }

    private void CheckState()
    {
        switch (gameState)
        {
        case GameState.GAMESTATE_PLAYER_ACTION:
            {
                if (true == player.Action())
                {
                    gameState = GameState.GAMESTATE_ICE_ACTION;
                }
                break;
            }
        case GameState.GAMESTATE_ICE_ACTION:
            {
                if (AllICsHaveMoved())
                {
                    gameState = GameState.GAMESTATE_PLAYER_ACTION;
                }
                break;
            }
        }
    }

    private bool AllICsHaveMoved()
    {
        foreach (var iceMovement in countermeasures)
        {
            var movement = iceMovement as IceMovement;
            if (movement != null)
            {
                movement.Move();
            }
        }
        return true;
    }

    private void UncoverNeighbourNodes()
    {
        player.currentNode.UncoverNeighbourNodes();
    }
}
