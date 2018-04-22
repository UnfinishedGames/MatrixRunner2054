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
    public Dictionary<Direction, Node> possibleDirections = null;

    private ShowIngameSprite ingameSprite;
    private SpriteRenderer editorSprite;
    private State currentState = State.Initial;
    private IEncounter myEncounter = null;

    void Start()
    {
        editorSprite = GetComponentInChildren<SpriteRenderer>();
        ingameSprite = GetComponentInChildren<ShowIngameSprite>();
    }

    void Update()
    {
        CheckEncounter();
        InitializeForEditor();
        UpdateView();
    }

    private void OnApplicationQuit()
    {
        editorSprite.enabled = true;
    }


    private void CheckEncounter()
    {
        if (myEncounter != null)
        {
            if (myEncounter.Status() == EncounterStatus.PlayerLost)
            {
                currentState = State.Blocked;
                myEncounter = null;
            }
            else if (myEncounter.Status() == EncounterStatus.PlayerWins)
            {
                currentState = State.Hacked;
                myEncounter = null;
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
        if (myEncounter == null)
        {
            PersistentEncounterStatus.Instance.Reset();
            myEncounter = GetComponentInChildren<IEncounter>();
            if (myEncounter != null)
            {
                myEncounter.Interaction(player);
            }
        }
    }

    public void Access(PlayerCharacterSheet player, MissionManager missionManager)
    {
        if (Action != null)
        {
            Action.Interact(this, player, missionManager);
        }
    }

    public void UncoverNeighbourNodes()
    {
        if (possibleDirections == null || possibleDirections.Count == 0)
        {
            return;
        }
        foreach (var node in possibleDirections.Values)
        {
            node.SwitchSpecificSpriteOn();
        }
    }

    public void SwitchSpecificSpriteOn()
    {
        ingameSprite.SwitchSpecificSpriteOn();
    }

    public void ColorSpecificSprite(Color color)
    {
        ingameSprite.SetSpriteColor(color);
    }
    
    public void GivePlayerAccess(PlayerCharacterSheet playerCharacterSheet, MissionManager missionManager)
    {
        Access(playerCharacterSheet, missionManager);
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
                ColorSpecificSprite(Color.white);
                break;
            case State.Hacked:
                ColorSpecificSprite(Color.green);
                break;
            case State.Blocked:
                ColorSpecificSprite(Color.red);
                break;
            default:
                throw new ArgumentOutOfRangeException("currentState");
        }
    }  
}
