using MissionEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HackingEncounter : EncounterBase, IEncounter
{
    private EncounterStatus status = EncounterStatus.Unavailable;
    private Scene encounterScene;

    // Use this for initialization
    void Start ()
    {
    }

    // Update is called once per frame
    void Update ()
    {
        if (status == EncounterStatus.OnGoing) {
            //thePlayer.GoOn();
        }
    }

    public new EncounterStatus Status ()
    {
        return status;
    }

    public void Interaction (PlayerMovement player)
    {
        if (status == EncounterStatus.Unavailable) {
            status = EncounterStatus.OnGoing;
            string hackingSceneName = "HackingHangman"; // TODO: make parameter for Interaction - load parameter from Editor
            //actionIndicator.gameObject.SetActive(true);
            //actionIndicatorText.text = "... fight ...";
            thePlayer = player;
            thePlayer.Stay ();
            SceneManager.LoadScene (hackingSceneName, LoadSceneMode.Additive);
            encounterScene = SceneManager.GetSceneByName (hackingSceneName);
            Debug.Log ("Hi");
            //SceneManager.MoveGameObjectToScene(EncounterTransferObject, hackingScene);
        }
    }
}
