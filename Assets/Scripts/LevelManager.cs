﻿using System.Linq;
using MissionEngine;
using UnityEngine;
using UnityEngine.UI;

enum GameState
{
    GAMESTATE_PLAYER_ACTION = 0,
    GAMESTATE_ICE_ACTION,
}

public class LevelManager : MonoBehaviour
{
    private const byte INTERACTION_SLIDER_INTERVAL = 40;
    private const byte INTERACTION_SLIDER_START = 0;
    private const byte INTERACTION_SLIDER_MAX = 100;

    public PlayerMovement player;
    public IceMovement ice;
    public IceLocation[] countermeasures;
    public Slider interactionTimeElapsed;
    public RectTransform actionIndicator;
    public RectTransform gameOverIndicator;
    public MissionManager missionManager;

    private Text gameOverText;
    private GameState gameState;
    
    void Start()
    {
        actionIndicator.gameObject.SetActive(false);
        gameOverIndicator.gameObject.SetActive(false);
        gameOverText = gameOverIndicator.GetComponentInChildren<Text>();

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

    private bool PlayerAndIceAreInTheSameNode(PlayerMovement player, IceLocation ice)
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
            ice.Interact(player, actionIndicator);
        }

        foreach(IceLocation ice in countermeasures)
        {
            if (PlayerAndIceAreInTheSameNode(player, ice))
            {
                ice.Interact(player, actionIndicator);
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
}
