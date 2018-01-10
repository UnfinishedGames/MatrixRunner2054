using MissionEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Mission : MonoBehaviour
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
    public virtual void Parameterzie(Dictionary<string, object> dictionary)
    {
        throw new InvalidOperationException("Do not use me!");
    }

    public virtual string GetDescription()
    {
        throw new InvalidOperationException("Do not use me!");
    }
}
