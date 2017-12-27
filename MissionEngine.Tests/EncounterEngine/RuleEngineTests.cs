using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace MissionEngine.Tests.cs.EncounterEngine
{
    [TestClass]
    public class RuleEngineTests
    {
        [TestMethod]
        public void CreateTheRuleEngine()
        {
            var target = new RuleEngine();
            var targetCapsule = new PrivateObject(target);
            target.Should().NotBeNull();
            targetCapsule.GetFieldOrProperty("diceRoller").Should().NotBeNull();
        }

        [TestMethod]
        public void BeginnerPlayer_against_HighLevelIce_fails_DeceptionUtility()
        {
            var target = new RuleEngine();

            var player = new PlayerCharacterSheet
            {
                MaskingkAttribute = 2,
                DeceptionProgram = 2
            };

            var ice = new SystemComponentSheet
            {
                SecurityCode = 4,
                SystemRating = 6,
                IceRating = 8
            };

            target.RollOff(player, ice, ExecuteUtility.Deception).Should().Be(Winner.CombatantTwo);
        }

        [TestMethod]
        public void ExpertPlayer_against_LowLevelIce_succeeds_DeceptionUtility()
        {
            var target = new RuleEngine();

            var player = new PlayerCharacterSheet
            {
                MaskingkAttribute = 5,
                DeceptionProgram = 7
            };

            var ice = new SystemComponentSheet
            {
                SecurityCode = 2,
                SystemRating = 3,
                IceRating = 4
            };

            target.RollOff(player, ice, ExecuteUtility.Deception).Should().Be(Winner.CombatantOne);
        }

        [TestMethod]
        public void BeginnerPlayer_against_LowLevelIce_drawOn_DeceptionUtility()
        {
            var target = new RuleEngine();

            var player = new PlayerCharacterSheet
            {
                MaskingkAttribute = 2,
                DeceptionProgram = 2
            };

            var ice = new SystemComponentSheet
            {
                SecurityCode = 2,
                SystemRating = 3,
                IceRating = 4
            };

            target.RollOff(player, ice, ExecuteUtility.Deception).Should().Be(Winner.Draw);
        }
    }
}
