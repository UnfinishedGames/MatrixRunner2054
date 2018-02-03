using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentEncounterStatus : MonoBehaviour {
    public PlayerMovement player;
    public EncounterStatus status = EncounterStatus.Unavailable;
    
    private static PersistentEncounterStatus _persistentStatus;

    public static PersistentEncounterStatus FetchPersistentStatus()
    {
        if (_persistentStatus == null)
        {
            GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (GameObject gameObject in rootGameObjects)
            {
                if (gameObject.name == "PersistentEncounterStatus")
                {
                    _persistentStatus = gameObject.GetComponent<PersistentEncounterStatus>();
                    break;
                }
            }
        }

        return _persistentStatus;
    }

    public void Reset()
    {
        if (_persistentStatus)
        {
            _persistentStatus.status = EncounterStatus.Unavailable;
        }
    }
}
