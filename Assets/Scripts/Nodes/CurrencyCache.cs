using UnityEngine.UI;

public class CurrencyCache : SpecificContents
{
    public override void Interact(PlayerCharacterSheet player, MissionManager missionManager)
    {
        missionManager.Inform(MissionEngine.GameAction.FoundCurrency);
    }
}