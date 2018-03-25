using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperOverride : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightControl))
        {
            if (Input.GetKey(KeyCode.W))
            {
                Debug.Log("DeveloperOverride RCtrl+W");
                PersistentEncounterStatus.Instance.status = EncounterStatus.PlayerWins;
            }
            else if (Input.GetKey(KeyCode.L))
            {
                Debug.Log("DeveloperOverride RCtrl+L");
                PersistentEncounterStatus.Instance.status = EncounterStatus.PlayerLost;
            }
        }
    }
}