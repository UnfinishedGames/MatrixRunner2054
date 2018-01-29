using System;
using UnityEngine;
using UnityEngine.UI;

public class DataStoreContents : MonoBehaviour
{
    public int NewYen;

    internal void Interact(PlayerCharacterSheet player, Text moneyIndicatorText)
    {
        player.Money += NewYen;
        player.UpdateCharacterSheet();
        moneyIndicatorText.text = player.Money + " ¥";
    }
}
