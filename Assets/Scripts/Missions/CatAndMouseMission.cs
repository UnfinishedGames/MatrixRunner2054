using MissionEngine;
using System;

public class CatAndMouseMission : Mission
{
    private byte fightsAlreadyFought = 0;
    private byte nodesAlreadyHacked = 0;
    private MissionState currentState = MissionState.InProgress;

    public int FightsUntilFail = 0;
    public int NodesToHack = 0;
    public IceMovement BlackIceToActivate;

    public override string GetDescription()
    {
        return "Mission:\r\n Hack " + NodesToHack + " nodes while being caught less than " + FightsUntilFail + " times";
    }

    public override void StartMission()
    {
        fightsAlreadyFought = 0;
        nodesAlreadyHacked = 0;
        currentState = MissionState.InProgress;
    }

    public override void Inform(GameAction currentAction)
    {
        switch (currentAction)
        {
            case GameAction.FightInProgress:
                fightsAlreadyFought++;
                break;
            case GameAction.NodeHacked:
                nodesAlreadyHacked++;
                break;
            case GameAction.AuthenticationFailed:
                SendTheBlackIce();
                break;
            default:
                throw new InvalidOperationException("currentAction");
        }
    }

    public override MissionState AskMissionState()
    {
        if (fightsAlreadyFought == FightsUntilFail)
        {
            currentState = MissionState.Failed;
        }
        if (nodesAlreadyHacked == NodesToHack)
        {
            currentState = MissionState.Succeeded;
        }

        return currentState;
    }

    private void SendTheBlackIce()
    {
        BlackIceToActivate.GoOn();
    }
}
