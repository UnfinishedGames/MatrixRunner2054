using MissionEngine;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CatAndMouseMission : Mission
{
    private CatAndMouseImplementation implementation;

    public int FightsUntilFail = 0;
    public int NodesToHack = 0;
    public IceMovement BlackIceToActivate;
    public RectTransform FundsIndicator;

    public CatAndMouseMission()
    {
        implementation = new CatAndMouseImplementation();
    }

    private void Start()
    {
        implementation.BlackIceToActivate = BlackIceToActivate;
        implementation.PlayerFundsText = FundsIndicator.GetComponentInChildren<Text>();
    }

    public override string GetDescription()
    {
        implementation.FightsUntilFail = FightsUntilFail;
        implementation.NodesToHack = NodesToHack;
        return implementation.GetDescription();
    }

    public override void StartMission()
    {
        implementation.StartMission();
    }

    public override void Inform(GameAction currentAction)
    {
        implementation.Inform(currentAction);
    }

    public override MissionState AskMissionState()
    {
        return implementation.AskMissionState();
    }
}
