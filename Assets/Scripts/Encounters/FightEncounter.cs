using MissionEngine;
using UnityEngine;

public class FightEncounter : EncounterBase, IEncounter
{
    private const byte WAIT_PERIOD = 2;
    private const float TIMER_START_VALUE = 0.0f;

    private float timer = TIMER_START_VALUE;
    private bool timerIsRunning = false;

    private IceMovement ice;
    
    void Start()
    {
        ice = GetComponentInParent<IceMovement>();
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (!timerIsRunning)
        {
            return;
        }
        timer += Time.deltaTime;
        if (timer >= WAIT_PERIOD)
        {
            missionManager.Inform(GameAction.FightInProgress);
            Node iceStartNode = thePlayer.currentNode == ice.startNode ? thePlayer.startNode : ice.startNode;
            ice.Reset(iceStartNode);
            thePlayer.GoOn();
            actionIndicator.gameObject.SetActive(false);
            timer = TIMER_START_VALUE;
            timerIsRunning = false;
        }
    }

    public void Interaction(PlayerMovement player)
    {
        if (!timerIsRunning)
        {
            actionIndicator.gameObject.SetActive(true);
            actionIndicatorText.text = "... fight ...";
            thePlayer = player;
            ice.Stay();
            player.Stay();
            timer = TIMER_START_VALUE;
            timerIsRunning = true;
        }
    }
}
