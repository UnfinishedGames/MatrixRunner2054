﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterStub : MonoBehaviour, IEncounter {
    // Use this for initialization
    void Start()
    {
    }
	
    // Update is called once per frame
    void Update()
    {
    }

    public void Fight(PlayerMovement player, IceMovement ice, Text text)
    {
        text.text = "OMFG!";
    }
}