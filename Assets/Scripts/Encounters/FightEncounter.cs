using MissionEngine;
using UnityEngine;

public class FightEncounter : EncounterBase, IEncounter
{
    private const byte WAIT_PERIOD = 2;
    private const float TIMER_START_VALUE = 0.0f;

    private float timer = TIMER_START_VALUE;
    private bool timerIsRunning = false;
    private IceMovement theIce;


    void Start()
    {
        Initialize();
        theIce = GetComponentInParent<IceMovement>();
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
            Node iceStartNode = thePlayer.currentNode == theIce.startNode ? thePlayer.startNode : theIce.startNode;
            theIce.Reset(iceStartNode);
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
            player.Stay();
            timer = TIMER_START_VALUE;
            timerIsRunning = true;
        }
    }
}
