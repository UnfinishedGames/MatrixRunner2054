using UnityEngine.UI;

class VirusCache : SpecificAction
{
    public int UpgradeBy;

    public override void Interact(PlayerCharacterSheet player, MissionManager missionManager)
    {
        player.DeceptionProgram += UpgradeBy;
        player.MaskingkAttribute += UpgradeBy;
        player.UpdateCharacterSheet();
    }
}
