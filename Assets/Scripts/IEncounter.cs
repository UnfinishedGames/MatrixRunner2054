using System;
using UnityEngine.UI;

public interface IEncounter
{
    void Fight(PlayerMovement player, IceMovement ice, Text text);
}

