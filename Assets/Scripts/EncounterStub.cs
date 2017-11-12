using UnityEngine;

public class EncounterStub : MonoBehaviour, IEncounter
{
    private const byte WAIT_PERIOD = 2;
    private float timer = 0;
    private bool timerIsRunning = false;
    private PlayerMovement thePlayer;
    private IceMovement ice;

    void Start()
    {
        ice = GetComponentInParent<IceMovement>();
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
            ice.Reset();
            thePlayer.GoOn();
            timer = 0;
            timerIsRunning = false;
        }
    }

    public void Fight(PlayerMovement player, RectTransform actionIndicator)
    {
        if (!timerIsRunning)
        {
            actionIndicator.gameObject.SetActive(true);
            thePlayer = player;
            ice.Stay();
            player.Stay();
            timer = 0;
            timerIsRunning = true;
        }
    }
}
