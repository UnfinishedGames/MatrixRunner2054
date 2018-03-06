using MissionEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
using EncounterEngine.enums;

public class FightEncounter : EncounterBase, IEncounter
{
    public FightTypes EncounterType;
    private IceMovement theIce;
    private bool isActive = false;

    void Start()
    {
        Initialize();
        theIce = GetComponentInParent<IceMovement>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            EncounterStatus status = PersistentEncounterStatus.Instance.status;
            if (status == EncounterStatus.PlayerLost || status == EncounterStatus.PlayerWins)
            {
                ClearScene();
            }
        }
    }

    private void ClearScene()
    {
        isActive = false;
        missionManager.Inform(GameAction.FightInProgress, null);

        if (PersistentEncounterStatus.Instance.status == EncounterStatus.PlayerWins)
        {
            
        }
        SceneManager.UnloadSceneAsync(EncounterType.ToString());
        // TODO: the following stuff could be done in the mission - but then the mission must know the player
        Node iceStartNode = thePlayer.currentNode == theIce.startNode ? thePlayer.startNode : theIce.startNode;
        theIce.Reset(iceStartNode);
        thePlayer.GoOn();
        PersistentEncounterStatus.Instance.status = EncounterStatus.Unavailable;
    }

    private void PrepareEncounter(PlayerMovement player)
    {
        isActive = true;
        this.thePlayer = player;
        thePlayer.Stay();
        PersistentEncounterStatus.Instance.status = EncounterStatus.OnGoing;
    }

    public void Interaction(PlayerMovement player)
    {
        if (PersistentEncounterStatus.Instance.status == EncounterStatus.Unavailable)
        {
            PrepareEncounter(player);
            try
            {
                SceneManager.LoadScene(EncounterType.ToString(), LoadSceneMode.Additive);
            }
            catch // TODO: wtf? why does this not work?
            {
                Debug.LogError("Could not load scene " + EncounterType.ToString());
                thePlayer.GoOn();
            }
        }
    }
}
