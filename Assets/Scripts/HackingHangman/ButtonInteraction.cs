using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{

    public void SetWin ()
    {
        GameObject[] rootGameObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().GetRootGameObjects ();
        foreach (GameObject gameObject in rootGameObjects)
        {
            if (gameObject.name == "PersistentEncounterStatus")
            {
                PersistentEncounterStatus status = gameObject.GetComponent<PersistentEncounterStatus> ();
                if (status != null)
                {
                    status.status = EncounterStatus.PlayerWins;
                }
            }
        }
    }
}
