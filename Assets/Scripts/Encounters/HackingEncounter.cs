using System;
using EncounterEngine.enums;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HackingEncounter : EncounterBase, IEncounter
{
    public HackingTypes hackingType;
    private EncounterStatus status = EncounterStatus.Unavailable;

    public new EncounterStatus Status()
    {
        var persistenStatus = PersistentEncounterStatus.FetchPersistentStatus();
        EncounterStatus result = EncounterStatus.Unavailable;
        if (persistenStatus != null)
        {
            result = persistenStatus.status;
        }

        return result;
    }

    public void Interaction(PlayerMovement player)
    {
        if (status == EncounterStatus.Unavailable)
        {
            status = EncounterStatus.OnGoing;
            thePlayer = player;
            thePlayer.Stay();
            try
            {
                SceneManager.LoadScene(hackingType.ToString(), LoadSceneMode.Additive);
            }
            catch(UnityException ex) // TODO: wtf? why does this not work?
            {
                Debug.LogError("Could not load scene " + hackingType.ToString());
                thePlayer.GoOn();
            }
        }
    }
}