using System;
using UnityEngine;
using UnityEngine.UI;

public class DataStoreContents : MonoBehaviour
{
    public SpecificContents Contents;

    internal void Interact(PlayerCharacterSheet player, Text moneyIndicatorText)
    {
        Contents.Interact(player, moneyIndicatorText);
    }
}
