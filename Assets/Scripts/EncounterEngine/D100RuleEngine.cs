using System;

public class D100RuleEngine
{
    private D100Roller diceRoller;

    public D100RuleEngine()
    {
        diceRoller = new D100Roller((int)DateTime.Now.Ticks);
    }

    public Winner RollOff(CharacterSheet player, ComponentSheet ice, ExecuteUtility deception)
    {
        var probabilty = player.DeceptionUtility * 10;
        int modifier = GetProbablityModifierFromRating(ice.IceRating);
        return (diceRoller.DoRoll(probabilty, modifier) ? Winner.CombatantOne
                                                        : Winner.CombatantTwo);
    }

    public int GetProbablityModifierFromRating(int rating)
    {
        return -10*(rating-5);
    }
}
