using EncounterEngine.enums;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SliderInteraction : MonoBehaviour
{
    private const byte INTERACTION_SLIDER_INTERVAL = 40;
    private const byte INTERACTION_SLIDER_START = 0;
    private const byte INTERACTION_SLIDER_MAX = 100;

    public Slider interactionTimeElapsed;

//	void Start()
//	{
//		PersistentEncounterStatus.FetchPersistentStatus().Reset();
//	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            UpdateInteractionSlider();
        }
    }

    private void UpdateInteractionSlider()
    {
        interactionTimeElapsed.value += (Time.deltaTime * INTERACTION_SLIDER_INTERVAL);
        if (interactionTimeElapsed.value >= INTERACTION_SLIDER_MAX)
        {
            interactionTimeElapsed.value = INTERACTION_SLIDER_START;
            var persistentEncounterStatus = PersistentEncounterStatus.FetchPersistentStatus();
            persistentEncounterStatus.status = EncounterStatus.PlayerWins;
            ClearScene(persistentEncounterStatus.player);
        }
    }

    private void ClearScene(PlayerMovement player)
    {
        SceneManager.UnloadSceneAsync(HackingTypes.HackingSlider.ToString());
        player.GoOn();
    }
}