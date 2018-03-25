using System.Collections;
using System.Collections.Generic;
using BlackIceFight;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        if (PersistentEncounterStatus.Instance.currentFight == "")
        {
            PersistentEncounterStatus.Instance.currentFight = SceneManager.GetActiveScene().name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimationFinished()
    {
        // We need to clean up the root, since the running mode and editor mode seem to use different members...
        GameObject[] rootGameObjects = SceneManager.GetSceneByName(PersistentEncounterStatus.Instance.currentFight).GetRootGameObjects();
        foreach (GameObject gameObj in rootGameObjects)
        {
            var pauseArray = gameObj.GetComponentsInChildren<PauseBehaviour>();
            foreach (var pauseBehaviour in pauseArray)
            {
                pauseBehaviour.UnPause();
            }
        }
        Debug.Log("Finsihed!");
    }
}