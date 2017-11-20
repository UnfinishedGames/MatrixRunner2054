using MissionEngine;
using UnityEngine;

public class EncounterStub : MonoBehaviour, IEncounter
{
    private const byte WAIT_PERIOD = 2;
    private const float TIMER_START_VALUE = 0.0f; 

    private float timer = TIMER_START_VALUE;
    private bool timerIsRunning = false;
    private PlayerMovement thePlayer;
    private IceMovement ice;
    private RectTransform actionIndicator;
    private MissionManager missionManager;

    void Start()
    {
        ice = GetComponentInParent<IceMovement>();
        missionManager = GameObject.Find("MissionManager").GetComponent<MissionManager>();
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
            ice.Reset();
            thePlayer.GoOn();
            actionIndicator.gameObject.SetActive(false);
            timer = TIMER_START_VALUE;
            timerIsRunning = false;
        }
    }

    public void Fight(PlayerMovement player, RectTransform indicator)
    {
        if (!timerIsRunning)
        {
            actionIndicator = indicator;
            actionIndicator.gameObject.SetActive(true);
            thePlayer = player;
            ice.Stay();
            player.Stay();
            timer = TIMER_START_VALUE;
            timerIsRunning = true;
        }
    }
}
