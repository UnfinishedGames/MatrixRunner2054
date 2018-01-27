using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentEncounterStatus : MonoBehaviour
{
    public PlayerMovement player;
    public EncounterStatus status = EncounterStatus.Unavailable;

    void Awake ()
    {
        DontDestroyOnLoad (transform.gameObject);
    }

    // Use this for initialization
    void Start ()
    {

    }
 
    // Update is called once per frame
    void Update ()
    {

    }
}
