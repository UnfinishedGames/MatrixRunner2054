using System;
using System.Collections.Generic;

public class CurrencyCache : SpecificAction
{
    public int Amount;
    public override void Interact(PlayerCharacterSheet player, MissionManager missionManager)
    {
        var data = new Dictionary<Type, object>
        {
            { typeof(int), Amount }
        };
        missionManager.Inform(MissionEngine.GameAction.FoundCurrency, data);
    }
}