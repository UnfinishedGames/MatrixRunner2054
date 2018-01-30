using UnityEngine;

public class DataStoreContents : MonoBehaviour
{
    public SpecificContents Contents;

    internal void Interact(PlayerCharacterSheet player, MissionManager missionManager)
    {
        Contents.Interact(player, missionManager);
    }
}
