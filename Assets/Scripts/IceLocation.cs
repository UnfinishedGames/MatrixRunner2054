using UnityEngine;

public class IceLocation : MonoBehaviour
{
    public Node currentNode;
    public SystemComponentSheet componentSheet;

    public Node startNode { get; private set; }
    private IEncounter myEncounter;
    private SpriteRenderer myRenderer;
    protected bool isVisible;

    void Start()
    {
        isVisible = false;
        InitializeForStartup();
    }

    internal void Interact(PlayerMovement player)
    {
        isVisible = true;
        UpdateView();
        myEncounter.Interaction(player);
    }

    protected void InitializeForStartup()
    {
        myEncounter = GetComponentInChildren<IEncounter>();
        myRenderer = GetComponentInChildren<SpriteRenderer>();
        startNode = currentNode;
        UpdateView();
    }

    protected void UpdateView()
    {
        if (currentNode != null)
        {
            myRenderer.enabled = isVisible;
            transform.position = currentNode.transform.position;
        }
    }
}
