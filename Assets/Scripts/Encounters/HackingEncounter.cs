using System;
using EncounterEngine.enums;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HackingEncounter : EncounterBase, IEncounter
{
    public HackingTypes hackingType;
    // The type of the node i.e. the scene to start
    private Node node;
    // the node that was triggered
    private bool isActive = false;
    // If the current encounter ist the one that started a scene

    public void Start()
    {
        Initialize();
    }

    public void Update()
    {
        if (isActive)
        {
            if (this.Status() == EncounterStatus.PlayerLost || this.Status() == EncounterStatus.PlayerWins)
            {
                ClearScene();
            }
        }
    }

    private void ClearScene()
    {
        isActive = false;
        if (this.Status() == EncounterStatus.PlayerWins)
        {
            PlayerCharacterSheet playerCharacterSheet = thePlayer.GetComponentInChildren<PlayerCharacterSheet>();
            node.GivePlayerAccess(playerCharacterSheet, this.missionManager);
        }
        SceneManager.UnloadSceneAsync(hackingType.ToString());
        thePlayer.GoOn();
    }

    public new EncounterStatus Status()
    {
        EncounterStatus result = EncounterStatus.Unavailable;
        var persistenStatus = PersistentEncounterStatus.FetchPersistentStatus();
        if (persistenStatus != null)
        {
            result = persistenStatus.status;
        }

        return result;
    }

    private void PrepareEncounter(PlayerMovement player)
    {
        isActive = true;
        this.thePlayer = player;
        thePlayer.Stay();
        this.node = player.currentNode;
        PersistentEncounterStatus.FetchPersistentStatus().status = EncounterStatus.OnGoing;
    }

    public void Interaction(PlayerMovement player)
    {
        if (this.Status() == EncounterStatus.Unavailable)
        {
            PrepareEncounter(player);

            try
            {
                SceneManager.LoadScene(hackingType.ToString(), LoadSceneMode.Additive);
            }
            catch (UnityException ex) // TODO: wtf? why does this not work?
            {
                Debug.LogError("Could not load scene " + hackingType.ToString());
                thePlayer.GoOn();
            }
        }
    }
}
