using UnityEngine;

public class ShowIngameSprite : MonoBehaviour
{
    public Sprite SpecificImage;
    private SpriteRenderer mySprite;
    private ShowMesh myMeshScript;

    // Use this for initialization
    void Start()
    {
        mySprite = GetComponentInChildren<SpriteRenderer>();
        mySprite.enabled = true;
        
        var parentMesh = transform.Find("Tower");
        if (parentMesh == null) return;
        var mesh = parentMesh.Find("Tower.Shape");
        if (mesh == null) return;
        var script = mesh.GetComponentInChildren<ShowMesh>();
        if (script == null) return;
        myMeshScript = script;
    }

    public void SwitchSpecificSpriteOn()
    {
        mySprite.sprite = SpecificImage;
        if(myMeshScript != null)
        {
            myMeshScript.SetVisible();
        }
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

