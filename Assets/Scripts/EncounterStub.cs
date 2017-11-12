using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using System.Threading;

public class EncounterStub : MonoBehaviour, IEncounter {
    // Use this for initialization
    private const byte WAIT_PERIOD = 2;
    private float timer = 0;
    private bool timerIsRunning = false;
    PlayerMovement player;
    IceMovement ice;

    void Start()
    {
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
            player.GoOn();
            timer = 0;
            timerIsRunning = false;
        }
    }

    public void Fight(PlayerMovement player, IceMovement ice, Text text)
    {
        if (!timerIsRunning)
        {
            this.ice = ice;
            this.player = player;
            text.text = "OMFG!";
            ice.Stay();
            player.Stay();
            timer = 0;
            timerIsRunning = true;
        }
    }
}
