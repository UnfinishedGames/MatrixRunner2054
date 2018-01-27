using UnityEngine;

public interface IEncounter
{
    void Interaction(PlayerMovement player);
    EncounterStatus Status();
}

