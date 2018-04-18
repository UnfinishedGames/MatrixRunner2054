using MissionEngine;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    private Text missionText;
    private D100RuleEngine ruleEngine;

    public RectTransform MissionIndicator;
    public Mission CurrentMission;

    public D100RuleEngine RuleEngine
    {
        get { return ruleEngine; }
    }

    void Start()
    {
        ruleEngine = new D100RuleEngine();

        missionText = MissionIndicator.GetComponentInChildren<Text>();
        missionText.text = CurrentMission.GetDescription();
    }

    void Update()
    {

    }

    internal void Inform(GameAction action, Dictionary<Type, object> data)
    {
        CurrentMission.Inform(action, data);
    }

    internal MissionState CheckMissionState()
    {
        return CurrentMission.AskMissionState();
    }

    internal void RestartMission()
    {
        CurrentMission.StartMission();
    }
}
