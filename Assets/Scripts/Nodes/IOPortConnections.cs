using System;
using UnityEngine;
using UnityEngine.UI;

public class IOPortConnections : MonoBehaviour
{
    public SpecificConnection Connection;

    internal void Interact(MissionManager missionManager)
    {
        Connection.Interact(missionManager);
    }
}
