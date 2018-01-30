public class CurrencyCache : SpecificAction
{
    public override void Interact(PlayerCharacterSheet player, MissionManager missionManager)
    {
        missionManager.Inform(MissionEngine.GameAction.FoundCurrency);
    }
}