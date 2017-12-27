using MissionEngine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    private Mission currentMission;
    private Text missionText;
    private RuleEngine ruleEngine;

    public RectTransform MissionIndicator;
    
    public RuleEngine RuleEngine
    {
        get { return ruleEngine; }
    }

    void Start()
    {
        ruleEngine = new RuleEngine();

        currentMission = MissionEngineStrategy.Create(MissionType.CatAndMouse);
        currentMission.Parameterzie(new Dictionary<string, int> { { Parameters.NumberOfFightsToLose, 2 },
                                                                  { Parameters.NumberOfNodesToHack, 9 } });
        missionText = MissionIndicator.GetComponentInChildren<Text>();
        missionText.text = currentMission.GetDescription();
    }

    void Update()
    {

    }

    internal void Inform(GameAction action)
    {
        currentMission.Inform(action);
    }

    internal MissionState CheckMissionState()
    {
        return currentMission.AskMissionState();
    }

    internal void RestartMission()
    {
        currentMission.StartMission();
    }
}
