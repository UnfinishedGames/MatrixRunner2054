using System;
using UnityEngine;

public class AuthenticationEncounter : MonoBehaviour, IEncounter
{
    private const byte WAIT_PERIOD = 2;
    private const float TIMER_START_VALUE = 0.0f;

    private float timer = TIMER_START_VALUE;
    private bool timerIsRunning = false;
    private PlayerMovement thePlayer;
    private RectTransform actionIndicator;
    private bool AlreadyFoughtOnce = false;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!timerIsRunning || AlreadyFoughtOnce)
        {
            return;
        }
        timer += Time.deltaTime;
        if (timer >= WAIT_PERIOD)
        {
            thePlayer.GoOn();
            actionIndicator.gameObject.SetActive(false);
            timer = TIMER_START_VALUE;
            timerIsRunning = false;
            AlreadyFoughtOnce = true;
        }
    }

    public void Interaction(PlayerMovement player, RectTransform indicator)
    {
        if (!timerIsRunning && !AlreadyFoughtOnce)
        {
            actionIndicator = indicator;
            actionIndicator.gameObject.SetActive(true);
            thePlayer = player;
            player.Stay();
            timer = TIMER_START_VALUE;
            timerIsRunning = true;
        }
    }
}
