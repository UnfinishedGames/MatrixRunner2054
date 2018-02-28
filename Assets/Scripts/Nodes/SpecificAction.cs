using System;
using UnityEngine;
using UnityEngine.UI;

public class SpecificAction : MonoBehaviour
{
    public virtual void Interact(Node callingNode, PlayerCharacterSheet player, MissionManager missionManager)
    {
        throw new NotImplementedException("Do not use me");
    }
}



