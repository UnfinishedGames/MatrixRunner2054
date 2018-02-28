using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.Collections.Generic;

namespace MissionEngine.Tests.cs
{
    [TestClass]
    public class CatAndMouseTests
    {
        CatAndMouseImplementation target;

        [TestInitialize]
        public void TestInitialize()
        {
            target = new CatAndMouseImplementation()
            {
                NodesToHack = 2,
                FightsUntilFail = 3
            };
        }

        [TestMethod]
        public void CreateACatAndMouseMission_Succeeds()
        {
            target.Should().NotBeNull();
        }

        [TestMethod]
        public void AskingForMissionState_IsInProgressByDefault()
        {
            MissionState result = target.AskMissionState();
            result.Should().Be(MissionState.InProgress);
        }

        [TestMethod]
        public void AskingForMissionState_AfterThreeFights_ResultsInFailedMission()
        {
            target.Inform(GameAction.FightInProgress, null);
            target.Inform(GameAction.FightInProgress, null);
            target.Inform(GameAction.FightInProgress, null);
            MissionState result = target.AskMissionState();
            result.Should().Be(MissionState.Failed);
        }

        [TestMethod]
        public void AskingForMissionState_AfterAllNodesAreHacked_ResultsInSucceededMission()
        {
            target.Inform(GameAction.NodeHacked, null);
            target.Inform(GameAction.NodeHacked, null);
            MissionState result = target.AskMissionState();
            result.Should().Be(MissionState.Succeeded);
        }

        [TestMethod]
        public void AskingForMissionState_AfterAReset_ResultsInMissionInProgress()
        {
            target.Inform(GameAction.NodeHacked, null);
            target.Inform(GameAction.NodeHacked, null);
            target.StartMission();
            MissionState result = target.AskMissionState();
            result.Should().Be(MissionState.InProgress);
        }

        [TestMethod]
        public void AskingForMissionState_AfterTheWinningNodeWasHacked_ResultsInSucceededMission()
        {
            target.StartMission();
            target.Inform(GameAction.HackedWinningNode, null);
            MissionState result = target.AskMissionState();
            result.Should().Be(MissionState.Succeeded);
        }

        [TestMethod]
        public void AskingForMissionDescription_ResultsInMissionDescription()
        {
            string result = target.GetDescription();
            result.Should().Be(@"Mission:
 Hack 2 nodes while being caught less than 3 times, or switch off the buildings cameras via the slave node (circle). You can also find up to 1000 ¥");
        }

        [TestMethod]
        public void SetMoneyForPlayer_ResultsInMoreMoney()
        {
            target.Inform(GameAction.FoundCurrency, new Dictionary<System.Type, object>() { { typeof(int), 200 }});
            var targetToAssert = new PrivateObject(target);
            var result = targetToAssert.GetFieldOrProperty("funds");
            result.Should().Be(200);
        }
    }
}
