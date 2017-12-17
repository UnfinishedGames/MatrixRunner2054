using System;

namespace MissionEngine.Tests.cs.Encounters
{
    internal class DieRoller
    {
        private Random generator;

        public DieRoller(int seed)
        {
            generator = new Random(seed);
        }

        public byte MakeSuccessTest(int skillLevel, int targetNumber)
        {
            byte result = 0;
            for(int x = 0; x < skillLevel; x++)
            {
                var roll = DoARoll();
                roll += AddAnotherRollOnSix(roll);
                if (TargetNumberHit(targetNumber, roll)) { result++; }
            }
            return result;
        }

        private int DoARoll()
        {
            return generator.Next(1, 7);
        }
        
        private int AddAnotherRollOnSix(int roll)
        {
            return (roll == 6) ? DoARoll() : 0;
        }

        private static bool TargetNumberHit(int targetNumber, int roll)
        {
            return roll >= targetNumber;
        }

        internal Winner ComparativeTest(int skillLevelCombatantOne, 
                                        int skillLevelCombatantTwo)
        {
            var resultOne = MakeSuccessTest(skillLevelCombatantOne, skillLevelCombatantTwo);
            var resultTwo = MakeSuccessTest(skillLevelCombatantTwo, skillLevelCombatantOne);

            return (resultOne >= resultTwo) ? Winner.CombatantOne : Winner.CombatantTwo;
        }
    }
}