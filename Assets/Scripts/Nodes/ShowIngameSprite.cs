using System;
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

    public void SetSpriteColor(Color color)
    {
        if (WeAreInEditMode()) return;
        mySprite.color = color;
    }

    private bool WeAreInEditMode()
    {
        return mySprite == null;
    }
}

