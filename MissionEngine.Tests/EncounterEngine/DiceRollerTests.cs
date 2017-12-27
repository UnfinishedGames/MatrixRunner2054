using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace MissionEngine.Tests.cs.Encounters
{
    [TestClass]
    public class DiceRollerTests
    {
        private const int COMMON_SEED = 3;

        DiceRoller dieRoller;

        [TestInitialize]
        public void TestInitialize()
        {
            dieRoller = new DiceRoller(COMMON_SEED);
        }

        [TestMethod]
        public void Roll1Die_AndDontReachTargetNumber()
        {
            dieRoller.MakeSuccessTest(1, 4).Should().Be(0);
        }

        [TestMethod]
        public void Roll2Dice_AndReachTargetNumberOnce()
        {
            dieRoller.MakeSuccessTest(2, 3).Should().Be(1);
        }

        [TestMethod]
        public void Roll3Dice_AndReachTargetNumberTwice()
        {
            dieRoller.MakeSuccessTest(3, 4).Should().Be(2);
        }

        [TestMethod]
        public void Roll3Dice_AndReachTargetNumber7()
        {
            dieRoller.MakeSuccessTest(3, 7).Should().Be(1);
        }

        [TestMethod]
        public void Roll12Dice_AndReachTargetNumber8()
        {
            dieRoller.MakeSuccessTest(12, 8).Should().Be(2);
        }

        [TestMethod]
        public void RollTwoCombatants_CombatantOneWins()
        {
            dieRoller.ComparativeTest(6, 4).Should().Be(Winner.CombatantOne);
        }

        [TestMethod]
        public void RollTwoCombatants_CombatantTwoWins()
        {
            dieRoller.ComparativeTest(2, 7).Should().Be(Winner.CombatantTwo);
        }
    }
}
