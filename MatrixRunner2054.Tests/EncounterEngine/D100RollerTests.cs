using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace MissionEngine.Tests.cs.EncounterEngine
{
    [TestClass]
    public class D100RollerTests
    {
        private const int COMMON_SEED = 3;

        D100Roller dieRoller;

        [TestInitialize]
        public void TestInitialize()
        {
            dieRoller = new D100Roller(COMMON_SEED);
        }

        [TestMethod]
        public void HitAnUnmodifiedSeventyFivePercentChance()
        {
            dieRoller.DoRoll(75, 0).Should().Be(true);
        }

        [TestMethod]
        public void MissAnUnmodifiedSeventyFivePercentChance()
        {
            dieRoller.DoRoll(75, 0).Should().Be(true);
            dieRoller.DoRoll(75, 0).Should().Be(true);
            dieRoller.DoRoll(75, 0).Should().Be(false);
        }

        [TestMethod]
        public void MissAnUnmodifiedEighteenPercentChance()
        {
            dieRoller.DoRoll(18, 0).Should().Be(false);
        }

        [TestMethod]
        public void HitAnUnmodifiedEighteenPercentChance()
        {
            dieRoller.DoRoll(18, 0).Should().Be(false);
            dieRoller.DoRoll(18, 0).Should().Be(false);
            dieRoller.DoRoll(18, 0).Should().Be(false);
            dieRoller.DoRoll(18, 0).Should().Be(false);
            dieRoller.DoRoll(18, 0).Should().Be(false);
            dieRoller.DoRoll(18, 0).Should().Be(false);
            dieRoller.DoRoll(18, 0).Should().Be(false);
            dieRoller.DoRoll(18, 0).Should().Be(false);
            dieRoller.DoRoll(18, 0).Should().Be(false);
            dieRoller.DoRoll(18, 0).Should().Be(false);
            dieRoller.DoRoll(18, 0).Should().Be(false);
            dieRoller.DoRoll(18, 0).Should().Be(false);
            dieRoller.DoRoll(18, 0).Should().Be(false);
            dieRoller.DoRoll(18, 0).Should().Be(true);
        }


        [TestMethod]
        public void HitAMmodifiedFiftyOnePercentChance()
        {
            dieRoller.DoRoll(75, -24).Should().Be(true);
        }

        [TestMethod]
        public void MissAMmodifiedFiftyOnePercentChance()
        {
            dieRoller.DoRoll(75, -24).Should().Be(true);
            dieRoller.DoRoll(75, -24).Should().Be(false);
        }
    }
}
