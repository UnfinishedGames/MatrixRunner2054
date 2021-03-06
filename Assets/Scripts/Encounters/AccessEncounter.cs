﻿using MissionEngine;
using UnityEngine;

public class AccessEncounter : EncounterBase, IEncounter
{
    private const byte WAIT_PERIOD = 1;
    private const float TIMER_START_VALUE = 0.0f;

    private float timer = TIMER_START_VALUE;
    private bool timerIsRunning = false;
    private bool alreadyTriedAuthenticating = false;
    private IceLocation theIce;

    void Start()
    {
        Initialize();
        theIce = GetComponentInParent<IceLocation>();
    }

    public void Interaction(PlayerMovement player)
    {
        if (!timerIsRunning && !alreadyTriedAuthenticating)
        {
            actionIndicator.gameObject.SetActive(true);
            actionIndicatorText.text = "authenticating";
            thePlayer = player;
            player.Stay();
            timer = TIMER_START_VALUE;
            timerIsRunning = true;
        }
    }

    void Update()
    {
        if (NothingIsHappening())
        {
            return;
        }
        Act();
    }

    private bool NothingIsHappening()
    {
        return !timerIsRunning;
    }

    private void Act()
    {
        timer += Time.deltaTime;
        if (TimerElapsed())
        {
            timer = TIMER_START_VALUE;
            if (InPlayerFailureState())
            {
                EndEncounterInFailureState();
                return;
            }
            Winner winner = FindWinnerForRollOff();
            switch (winner)
            {
                case Winner.CombatantOne:
                    SetPlayerWinState();
                    break;
                case Winner.CombatantTwo:
                    SetPlayerFailureState();
                    break;
                default:
                    SetDrawState();
                    break;
            }
        }
    }

    private bool TimerElapsed()
    {
        return timer >= WAIT_PERIOD;
    }

    private bool InPlayerFailureState()
    {
        return timerIsRunning && alreadyTriedAuthenticating;
    }

    private void EndEncounterInFailureState()
    {
        missionManager.Inform(GameAction.AuthenticationFailed, null);
        thePlayer.GoOn();
        actionIndicator.gameObject.SetActive(false);
        timerIsRunning = false;
    }

    private Winner FindWinnerForRollOff()
    {
        var player = thePlayer.characterSheet.Bundeled;
        var ice = theIce.componentSheet.Bundeled;
        var winner = missionManager.RuleEngine.RollOff(player, ice, ExecuteUtility.Deception);
        return winner;
    }

    private void SetPlayerWinState()
    {
        alreadyTriedAuthenticating = true;
        thePlayer.GoOn();
        actionIndicator.gameObject.SetActive(false);
        timerIsRunning = false;
    }

    private void SetPlayerFailureState()
    {
        alreadyTriedAuthenticating = true;
        actionIndicatorText.text = "...failed";
        timerIsRunning = true;
    }

    private void SetDrawState()
    {
        actionIndicatorText.text = "...retry";
        timerIsRunning = true;
    }
}
