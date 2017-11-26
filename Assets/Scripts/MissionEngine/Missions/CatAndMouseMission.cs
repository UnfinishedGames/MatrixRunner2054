using System;
using System.Collections.Generic;

namespace MissionEngine
{
    public class CatAndMouseMission : Mission {
        private byte fightsAlreadyFought = 0;
        private byte nodesAlreadyHacked = 0;
        private int fightsUntilFail = 0;
        private int nodesToHack = 0;
        private MissionState currentState = MissionState.InProgress;

        public override string GetDescription()
        {
            return "Mission:\r\n Hack " + nodesToHack + " nodes while being caught less than three times";
        }

        public override void Parameterzie(Dictionary<string, int> dictionary)
        {
            fightsUntilFail = dictionary[Parameters.NumberOfFightsToLose];
            nodesToHack = dictionary[Parameters.NumberOfNodesToHack];
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
    }
}
