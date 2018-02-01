using System;
using UnityEngine;

public class TrapEncounter : EncounterBase, IEncounter
{
    private const byte WAIT_PERIOD = 1;
    private const float TIMER_START_VALUE = 0.0f;
    private const byte NUMBER_OF_ROUNDS_TO_TRAP_PLAYER = 4;

    private float timer = TIMER_START_VALUE;
    private bool timerIsRunning = false;
    private byte numberOfRoundsTrapped = 0;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        if (NothingIsHappening())
        {
            return;
        }
        Act();
    }

    public void Interaction(PlayerMovement player)
    {
        if (!timerIsRunning && !AlreadyTrapped())
        {
            actionIndicator.gameObject.SetActive(true);
            actionIndicatorText.text = "TRAPPED!";
            thePlayer = player;
            player.Stay();
            timer = TIMER_START_VALUE;
            timerIsRunning = true;
        }
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
            thePlayer.EndRound();
            actionIndicator.gameObject.SetActive(false);
            timerIsRunning = false;
            numberOfRoundsTrapped++;
        }
        if(AlreadyTrapped())
        {
            thePlayer.GoOn();
        }
    }

    private bool TimerElapsed()
    {
        return timer >= WAIT_PERIOD;
    }
    private bool AlreadyTrapped()
    {
        return NUMBER_OF_ROUNDS_TO_TRAP_PLAYER == numberOfRoundsTrapped;
    }
}
