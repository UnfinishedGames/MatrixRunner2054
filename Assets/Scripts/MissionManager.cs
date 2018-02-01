using MissionEngine;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    private Text missionText;
    private RuleEngine ruleEngine;

    public RectTransform MissionIndicator;
    public CatAndMouseMission CurrentMission;

    public RuleEngine RuleEngine
    {
        get { return ruleEngine; }
    }

    void Start()
    {
        ruleEngine = new RuleEngine();

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
