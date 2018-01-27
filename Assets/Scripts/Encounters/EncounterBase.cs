using UnityEngine;
using UnityEngine.UI;

public class EncounterBase : MonoBehaviour
{
    protected MissionManager missionManager;
    protected RectTransform actionIndicator;
    protected Text actionIndicatorText;
    protected PlayerMovement thePlayer;

    internal void Initialize()
    {
        missionManager = GameObject.Find("MissionManager").GetComponent<MissionManager>();
        actionIndicator = GameObject.Find("/HUDCanvas/ActionIndicator").GetComponent<RectTransform>();
        actionIndicatorText = GameObject.Find("/HUDCanvas/ActionIndicator/ActionText").GetComponent<Text>();
    }

    public EncounterStatus Status()
    {
        return EncounterStatus.Unavailable;
    }

}
