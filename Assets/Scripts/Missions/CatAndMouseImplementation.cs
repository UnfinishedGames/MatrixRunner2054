
using MissionEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class CatAndMouseImplementation
{
    private byte fightsAlreadyFought = 0;
    private byte nodesAlreadyHacked = 0;
    private MissionState currentState = MissionState.InProgress;

    private int fightsUntilFail = 0;
    private int nodesToHack = 0;
    private IceMovement blackIceToActivate;
    private int funds = 0;


    public int FightsUntilFail { set { fightsUntilFail = value; } }
    public int NodesToHack { set { nodesToHack = value; } }
    public IceMovement BlackIceToActivate  { set { blackIceToActivate = value; } }
    public Text PlayerFundsText { get; internal set; }
    public string GetDescription()
    {
        return "Mission:\r\n Hack " + nodesToHack + " nodes while being caught less than " + fightsUntilFail + " times," +
            " or switch off the buildings cameras via the I/O node (triangle). You can also find up to 1000 ¥";
    }

    public void StartMission()
    {
        fightsAlreadyFought = 0;
        nodesAlreadyHacked = 0;
        currentState = MissionState.InProgress;
    }

    public void Inform(GameAction currentAction, Dictionary<Type, object> data)
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
            case GameAction.FoundCurrency:
                var fundsToAdd = (int)data[typeof(int)];
                IncreaseFunds(fundsToAdd);
                break;
            case GameAction.HackedWinningNode:
                currentState = MissionState.Succeeded;
                break;
            default:
                throw new InvalidOperationException("currentAction");
        }
    }

    public MissionState AskMissionState()
    {
        if (fightsAlreadyFought == fightsUntilFail)
        {
            currentState = MissionState.Failed;
        }
        if (nodesAlreadyHacked == nodesToHack)
        {
            currentState = MissionState.Succeeded;
        }

        return currentState;
    }

    private void SendTheBlackIce()
    {
        blackIceToActivate.GoOn();
    }

    private void IncreaseFunds(int fundsToAdd)
    {
        funds += fundsToAdd;
        PlayerFundsText.text = funds + " ¥";
    }
}
