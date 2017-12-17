using UnityEngine;

public class AccessEncounter : EncounterBase, IEncounter
{
    private const byte WAIT_PERIOD = 2;
    private const float TIMER_START_VALUE = 0.0f;

    private float timer = TIMER_START_VALUE;
    private bool timerIsRunning = false;
    private bool AlreadyFoughtOnce = false;
    
    void Start()
    {
        Initialize();
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

    public void Interaction(PlayerMovement player)
    {
        if (!timerIsRunning && !AlreadyFoughtOnce)
        {
            actionIndicator.gameObject.SetActive(true);
            actionIndicatorText.text = "authenticating";
            thePlayer = player;
            player.Stay();
            timer = TIMER_START_VALUE;
            timerIsRunning = true;
        }
    }
}
