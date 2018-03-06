using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EncounterBase : MonoBehaviour
{
    protected MissionManager missionManager;
    protected RectTransform actionIndicator;
    protected Text actionIndicatorText;
    protected PlayerMovement thePlayer;

    internal void Initialize()
    {
        missionManager = FindObjectByName("MissionManager").GetComponent<MissionManager>();
        actionIndicator = FindObjectByName("HUDCanvas/ActionIndicator").GetComponent<RectTransform>();
        actionIndicatorText = FindObjectByName("HUDCanvas/ActionIndicator/ActionText").GetComponent<Text>();
    }

    /// <summary>
    /// GameObject.Find does not find inactive objects and it looks like there is no function that does so.
    /// </summary>
    /// <param name="objectName">The name or path to an object of the root tree - paths must NOT start with a '/'</param>
    /// <returns>The found GameObject or null</returns>
    private GameObject FindObjectByName(string objectName)
    {
        var namePaths = objectName.Split('/');
        if (namePaths == null)
        {
            namePaths = new[] { objectName };
        }

        GameObject foundObject = null;
        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject currentGameObject in rootGameObjects)
        {
            if (currentGameObject.name == namePaths[0])
            {
                foundObject = currentGameObject;
                foreach (var path in namePaths.Skip(1))
                {
                    foundObject = foundObject.transform.Find(path).gameObject;
                }
                break;
            }
        }

        return foundObject;
    }

    public EncounterStatus Status()
    {
        return PersistentEncounterStatus.Instance.status;
    }

}
