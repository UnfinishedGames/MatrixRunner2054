using MissionEngine;
using System;

public class CatAndMouseMission : Mission
{
    private CatAndMouseImplementation implementation;

    public int FightsUntilFail = 0;
    public int NodesToHack = 0;
    public IceMovement BlackIceToActivate;

    public CatAndMouseMission()
    {
        implementation = new CatAndMouseImplementation();
    }

    private void Start()
    {
        implementation.FightsUntilFail = FightsUntilFail;
        implementation.NodesToHack = NodesToHack;
        implementation.BlackIceToActivate = BlackIceToActivate;
    }

    public override string GetDescription()
    {
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
