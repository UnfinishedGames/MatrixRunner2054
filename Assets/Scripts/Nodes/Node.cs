using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Node : MonoBehaviour 
{
    public GameObject leftObject;
    public GameObject rightObject;
    public GameObject upObject;
    public GameObject downObject;
    public SpecificAction Action;
    public Sprite DefaultImage;
    public Dictionary<Direction, Node> possibleDirections = null;

    private Sprite actualImage;
    private SpriteRenderer mySprite;
    private State currentState = State.Initial;
    private IEncounter myEncounter = null;

    void Start()
    {
        mySprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        CheckEncounter();
        InitializeForEditor();
        UpdateView();
    }

    private void CheckEncounter()
    {
        if (this.myEncounter != null)
        {
            if (myEncounter.Status() == EncounterStatus.PlayerLost)
            {
                this.currentState = State.Blocked;
                this.myEncounter = null;
            }
            else if (myEncounter.Status() == EncounterStatus.PlayerWins)
            {
                this.currentState = State.Hacked;
                this.myEncounter = null;
            }
            else
            {
                // Nothing
            }
        }
    }

    private void InitializeForEditor()
    {
        if (possibleDirections == null)
        {
            possibleDirections = new Dictionary<Direction, Node>();

            possibleDirections.Add(Direction.Left, NodeFromObjectOrMyself(leftObject));
            possibleDirections.Add(Direction.Right, NodeFromObjectOrMyself(rightObject));
            possibleDirections.Add(Direction.Up, NodeFromObjectOrMyself(upObject));
            possibleDirections.Add(Direction.Down, NodeFromObjectOrMyself(downObject));
        }
    }

    public bool CanInteract()
    {
        return currentState != State.Hacked;
    }

    public void SwitchState(State newState)
    {
        currentState = newState;
    }

    public Node getNeighbour(Direction direction)
    {
        return possibleDirections[direction];
    }

    public void Interact(PlayerMovement player)
    {
        if (this.myEncounter == null)
        {
            PersistentEncounterStatus.Instance.Reset();
            this.myEncounter = GetComponentInChildren<IEncounter>();
            if (this.myEncounter != null)
            {
                myEncounter.Interaction(player);
            }
        }
    }

    private Node NodeFromObjectOrMyself(GameObject gameObj)
    {
        return gameObj != null ? gameObj.GetComponent<Node>() : this;
    }

    private void UpdateView()
    {
        switch (currentState)
        {
            case State.Initial:
                mySprite.color = Color.white;
                break;
            case State.Hacked:
                mySprite.color = Color.green;
                break;
            case State.Blocked:
                mySprite.color = Color.red;
                break;
            default:
                throw new ArgumentOutOfRangeException("currentState");
        }
    }

    internal void GivePlayerAccess(PlayerCharacterSheet playerCharacterSheet, MissionManager missionManager)
    {
        Access(playerCharacterSheet, missionManager);
    }

    internal void Access(PlayerCharacterSheet player, MissionManager missionManager)
    {
        if (Action != null)
        {
            Action.Interact(this, player, missionManager);
        }
    }
}
