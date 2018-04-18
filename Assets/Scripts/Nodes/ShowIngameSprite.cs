using UnityEngine;

public class ShowIngameSprite : MonoBehaviour
{
    public Sprite SpecificImage;

    private SpriteRenderer mySprite;

    // Use this for initialization
    void Start()
    {
        mySprite = GetComponentInChildren<SpriteRenderer>();
        mySprite.enabled = true;
    }

    public void SwitchSpecificSpriteOn()
    {
        mySprite.sprite = SpecificImage;
    }
}

