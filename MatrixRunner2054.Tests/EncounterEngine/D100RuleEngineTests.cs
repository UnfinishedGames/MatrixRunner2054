using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace MissionEngine.Tests.cs.EncounterEngine
{
    [TestClass]
    public class D100RuleEngineTests
    {
        D100RuleEngine target;

        [TestInitialize]
        public void TestInitialize()
        {
            target = new D100RuleEngine();
        }

        [TestMethod]
        public void CreateTheRuleEngine()
        {
            var targetCapsule = new PrivateObject(target);
            target.Should().NotBeNull();
            targetCapsule.GetFieldOrProperty("diceRoller").Should().NotBeNull();
        }
        
        [TestMethod]
        public void BeginnerPlayer_against_HighLevelIce_fails_DeceptionUtility()
        {
            var player = new CharacterSheet { DeceptionUtility = 2 };
            var ice = new ComponentSheet { IceRating = 7 };
            for (int x = 1; x < 100; x++)
            {
                target.RollOff(player, ice, ExecuteUtility.Deception).Should().Be(Winner.CombatantTwo);
            }
        }

        [TestMethod]
        public void MediumPlayer_against_HighLevelIce_fails_DeceptionUtility()
        {
            var player = new CharacterSheet { DeceptionUtility = 5 };
            var ice = new ComponentSheet { IceRating = 10 };
            for (int x = 1; x < 100; x++)
            {
                target.RollOff(player, ice, ExecuteUtility.Deception).Should().Be(Winner.CombatantTwo);
            }
        }

        [TestMethod]
        public void HighLevelPlayer_against_LowLevelIce_succeeds_DeceptionUtility()
        {
            var player = new CharacterSheet { DeceptionUtility = 7 };
            var ice = new ComponentSheet { IceRating = 2 };
            for (int x = 1; x < 100; x++)
            {
                target.RollOff(player, ice, ExecuteUtility.Deception).Should().Be(Winner.CombatantOne);
            }
        }

        [TestMethod]
        public void MediumLevelPlayer_against_LowLevelIce_succeeds_DeceptionUtility()
        {
            var player = new CharacterSheet { DeceptionUtility = 6 };
            var ice = new ComponentSheet { IceRating = 1 };
            for (int x = 1; x < 100; x++)
            {
                target.RollOff(player, ice, ExecuteUtility.Deception).Should().Be(Winner.CombatantOne);
            }
        }

        [TestMethod]
        public void Probability_Modifications_From_Ratings()
        {
            var target = new D100RuleEngine();
            target.GetProbablityModifierFromRating(10).Should().Be(-50);
            target.GetProbablityModifierFromRating(9).Should().Be(-40);
            target.GetProbablityModifierFromRating(8).Should().Be(-30);
            target.GetProbablityModifierFromRating(7).Should().Be(-20);
            target.GetProbablityModifierFromRating(6).Should().Be(-10);
            target.GetProbablityModifierFromRating(5).Should().Be(0);
            target.GetProbablityModifierFromRating(4).Should().Be(10);
            target.GetProbablityModifierFromRating(3).Should().Be(20);
            target.GetProbablityModifierFromRating(2).Should().Be(30);
            target.GetProbablityModifierFromRating(1).Should().Be(40);
        }
    }
}
