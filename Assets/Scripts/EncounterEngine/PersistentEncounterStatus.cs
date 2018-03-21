using System;
using EncounterEngine.enums;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class PersistentEncounterStatus
{
//    public PlayerMovement player;
    public EncounterStatus status = EncounterStatus.Unavailable;
    public string currentFight = "";
    
    private static readonly PersistentEncounterStatus instance = new PersistentEncounterStatus();

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static PersistentEncounterStatus()
    {
    }

    private PersistentEncounterStatus()
    {
    }

    public static PersistentEncounterStatus Instance
    {
        get
        {
            return instance;
        }
    }
    
    public void Reset()
    {
        Instance.status = EncounterStatus.Unavailable;
        currentFight = "";
    }
}
