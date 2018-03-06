using EncounterEngine.enums;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonInteraction : MonoBehaviour
{
    public void SetWin()
    {
        PersistentEncounterStatus persistentStatus = PersistentEncounterStatus.Instance;

        if (persistentStatus != null)
        {
            persistentStatus.status = EncounterStatus.PlayerWins;
        }
    }

    public void SetLoose()
    {
        PersistentEncounterStatus persistentStatus = PersistentEncounterStatus.Instance;

        if (persistentStatus != null)
        {
            persistentStatus.status = EncounterStatus.PlayerLost;
        }
    }
}