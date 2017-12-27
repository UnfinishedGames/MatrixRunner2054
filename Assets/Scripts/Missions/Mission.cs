using System;
using System.Collections.Generic;

namespace MissionEngine
{
    public abstract class Mission
    {
        public virtual MissionState AskMissionState()
        {
            throw new InvalidOperationException("Do not use me!");
        }
        public virtual void StartMission()
        {
            throw new InvalidOperationException("Do not use me!");
        }

        public virtual void Inform(GameAction fightInProgress)
        {
            throw new InvalidOperationException("Do not use me!");
        }
        public virtual void Parameterzie(Dictionary<string, int> dictionary)
        {
            throw new InvalidOperationException("Do not use me!");
        }

        public virtual string GetDescription()
        {
            throw new InvalidOperationException("Do not use me!");
        }
    }
}
