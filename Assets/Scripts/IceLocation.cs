using UnityEngine;

public class IceLocation : MonoBehaviour
{
    public Node currentNode;
    public Node startNode { get; private set; }
    private IEncounter myEncounter;

    void Start()
    {
        InitializeForStartup();
    }

    internal void Interact(PlayerMovement player, RectTransform actionIndicator)
    {
        myEncounter.Interaction(player, actionIndicator);
    }

    protected void InitializeForStartup()
    {
        myEncounter = GetComponentInChildren<IEncounter>();
        startNode = currentNode;
        UpdateView();
    }

    protected void UpdateView()
    {
        if (currentNode != null)
        {
            transform.position = currentNode.transform.position;
        }
    }
}
