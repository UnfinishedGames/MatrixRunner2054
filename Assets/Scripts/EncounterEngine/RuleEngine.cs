using System;

public class RuleEngine
{
    private DiceRoller diceRoller;

    public RuleEngine()
    {
        diceRoller = new DiceRoller(DateTime.Today.Ticks);
    }

    public Winner RollOff(PlayerCharacterSheet player, SystemComponentSheet ice, ExecuteUtility utility)
    {
        switch(utility)
        {
            case ExecuteUtility.Deception:
                return PerformDeceptionCheck(player, ice);
            default:
                throw new ArgumentOutOfRangeException("utility");
        }
    }

    private Winner PerformDeceptionCheck(PlayerCharacterSheet player, SystemComponentSheet ice)
    {
        var winner = Winner.Draw;
        var playerRoll = diceRoller.MakeSuccessTest(player.DeceptionProgram, ice.SystemRating);
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
