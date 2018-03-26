using UnityEngine;

public class ShowSprite : MonoBehaviour
{
    private SpriteRenderer mySprite;

    // Use this for initialization
    void Start()
    {
        mySprite = GetComponentInChildren<SpriteRenderer>();
        mySprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchSpriteOn()
    {
        mySprite.enabled = true;
    }

    public void SwitchSpriteOff()
    {
        mySprite.enabled = false;
    }
}


