using UnityEngine.UI;

class VirusCache : SpecificAction
{
    public int UpgradeBy;

    public override void Interact(Node callingNode, PlayerCharacterSheet player, MissionManager missionManager)
    {
        player.DeceptionProgram += UpgradeBy;
        player.UpdateCharacterSheet();
    }
}
