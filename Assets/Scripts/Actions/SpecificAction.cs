using System;
using UnityEngine;

public class SpecificAction : MonoBehaviour
{
    public virtual void Interact(Node callingNode, PlayerCharacterSheet player, MissionManager missionManager)
    {
        throw new NotImplementedException("Do not use me");
    }
}



