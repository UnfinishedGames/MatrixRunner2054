using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.Collections.Generic;

namespace MissionEngine.Tests.cs
{
    [TestClass]
    public class CatAndMouseTests
    {
        Mission target;

        [TestInitialize]
        public void TestInitialize()
        {
            target = MissionEngineStrategy.Create(MissionType.CatAndMouse);
            target.Parameterzie(new Dictionary<string, int> { { Parameters.NumberOfFightsToLose, 3 },
                                                              { Parameters.NumberOfNodesToHack, 2 } });
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
            target.Inform(GameAction.FightInProgress);
            target.Inform(GameAction.FightInProgress);
            target.Inform(GameAction.FightInProgress);
            MissionState result = target.AskMissionState();
            result.Should().Be(MissionState.Failed);
        }

        [TestMethod]
        public void AskingForMissionState_AfterAllNodesAreHacked_ResultsInSucceededMission()
        {
            target.Inform(GameAction.NodeHacked);
            target.Inform(GameAction.NodeHacked);
            MissionState result = target.AskMissionState();
            result.Should().Be(MissionState.Succeeded);
        }

        [TestMethod]
        public void AskingForMissionState_AfterAReset_ResultsInMissionInProgress()
        {
            target.Inform(GameAction.NodeHacked);
            target.Inform(GameAction.NodeHacked);
            target.StartMission();
            MissionState result = target.AskMissionState();
            result.Should().Be(MissionState.InProgress);
        }

        [TestMethod]
        public void AskingForMissionDescription_ResultsInMissionDescription()
        {
            string result = target.GetDescription();
            result.Should().Be("Mission:\r\n Hack 2 nodes while being caught less than three times");
        }
    }
}
