using UnityEngine;

public class SwitchHalo : MonoBehaviour {

    public byte NodeLevel;
    public Sprite LevelTwoHalo;

	// Use this for initialization
	void Start () {
		if(NodeLevel != 0)
        {
            GetComponent<SpriteRenderer>().sprite = LevelTwoHalo;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
