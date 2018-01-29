using UnityEngine.UI;

public class MoneyCache : SpecificContents
{
    public int NewYen;
    
    public override void Interact(PlayerCharacterSheet player, Text moneyIndicatorText)
    {
        player.Money += NewYen;
        player.UpdateCharacterSheet();
        moneyIndicatorText.text = player.Money + " ¥";
    }
}