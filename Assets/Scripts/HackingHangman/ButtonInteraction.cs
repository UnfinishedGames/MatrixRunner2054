using EncounterEngine.enums;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonInteraction : MonoBehaviour
{
    public void SetWin()
    {
        PersistentEncounterStatus persistentStatus = PersistentEncounterStatus.FetchPersistentStatus();

        if (persistentStatus != null)
        {
            persistentStatus.status = EncounterStatus.PlayerWins;
            ClearScene(persistentStatus.player);
        }
    }

    public void SetLoose()
    {
        PersistentEncounterStatus persistentStatus = PersistentEncounterStatus.FetchPersistentStatus();

        if (persistentStatus != null)
        {
            persistentStatus.status = EncounterStatus.PlayerLost;
            ClearScene(persistentStatus.player);
        }
    }

    private void ClearScene(PlayerMovement player)
    {
        SceneManager.UnloadSceneAsync(HackingTypes.HackingButton.ToString());
        player.GoOn();
    }
}