using System;
using System.Collections.Generic;

namespace MissionEngine
{
    public class DeprecatedCaMMission : DeprecatedMission {
        private byte fightsAlreadyFought = 0;
        private byte nodesAlreadyHacked = 0;
        private int fightsUntilFail = 0;
        private int nodesToHack = 0;
        private MissionState currentState = MissionState.InProgress;
        private IceMovement theBlackIce;

        public override string GetDescription()
        {
            return "Mission:\r\n Hack " + nodesToHack + " nodes while being caught less than " + fightsUntilFail + " times";
        }

        public override void Parameterzie(Dictionary<string, object> dictionary)
        {
            fightsUntilFail = (int)dictionary[Parameters.NumberOfFightsToLose];
            nodesToHack = (int)dictionary[Parameters.NumberOfNodesToHack];
            theBlackIce = (IceMovement)dictionary[Parameters.IceToActivate];
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
            throw new NotImplementedException();
        }
    }
}
