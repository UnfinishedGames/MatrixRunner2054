using System;

public class ShadwowrunTwoRuleEngine
{
    private ShadowrunTwoDiceRoller diceRoller;

    public ShadwowrunTwoRuleEngine()
    {
        diceRoller = new ShadowrunTwoDiceRoller(DateTime.Today.Ticks);
    }

    public Winner RollOff(CharacterSheet player, ComponentSheet ice, ExecuteUtility utility)
    {
        switch(utility)
        {
            case ExecuteUtility.Deception:
                return PerformDeceptionCheck(player, ice);
            default:
                throw new ArgumentOutOfRangeException("utility");
        }
    }

    private Winner PerformDeceptionCheck(CharacterSheet player, ComponentSheet ice)
    {
        var winner = Winner.Draw;
        var playerRoll = diceRoller.MakeSuccessTest(player.DeceptionUtility, ice.SystemRating);
        if (playerRoll == 0)
        {
            if (diceRoller.MakeSuccessTest(ice.IceRating, player.MaskingkAttribute) > 0)
            {
                winner = Winner.CombatantTwo;
            }
        }
        if (playerRoll >= ice.SecurityCode)
        {
            winner = Winner.CombatantOne;
        }
        return winner;
    }
}
