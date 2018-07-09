using UnityEngine;

public class ShowMesh : MonoBehaviour
{
    MeshRenderer myRenderer;
    float rotationSpeed = 0.1f;

    // Use this for initialization
    void Start()
    {
        myRenderer = GetComponentInChildren<MeshRenderer>();
        myRenderer.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVisible()
    {
        myRenderer.gameObject.SetActive(true);
    }
}
