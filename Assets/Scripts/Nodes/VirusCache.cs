using UnityEngine.UI;

class VirusCache : SpecificContents
{
    public int UpgradeBy;

    public override void Interact(PlayerCharacterSheet player, Text moneyIndicatorText)
    {
        player.DeceptionProgram += UpgradeBy;
        player.MaskingkAttribute += UpgradeBy;
        player.UpdateCharacterSheet();
    }
}
