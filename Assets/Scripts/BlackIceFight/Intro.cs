using System.Collections;
using System.Collections.Generic;
using BlackIceFight;
using UnityEngine;
using UnityEngine.SceneManagement;

public class intro : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
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