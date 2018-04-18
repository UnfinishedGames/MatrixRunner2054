using MissionEngine;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatAndMouseMissionPhoneCompany : Mission
{
    private CatAndMouseImplementation implementation;

    public int FightsUntilFail = 0;
    public int NodesToHack = 0;
    public IceMovement BlackIceToActivate;
    public RectTransform FundsIndicator;
    public MonoBehaviour WinningNode;

    public CatAndMouseMissionPhoneCompany()
    {
        implementation = new CatAndMouseImplementation();
    }

    private void Start()
    {
        implementation.BlackIceToActivate = BlackIceToActivate;
        implementation.PlayerFundsText = FundsIndicator.GetComponentInChildren<Text>();
        implementation.WinningNode = WinningNode;
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

    public override void Inform(GameAction currentAction, Dictionary<Type, object> data)
    {
        implementation.Inform(currentAction, data);
    }

    public override MissionState AskMissionState()
    {
        return implementation.AskMissionState();
    }
}
