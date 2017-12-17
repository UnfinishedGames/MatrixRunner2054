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

    internal void Interact(PlayerMovement player)
    {
        myEncounter.Interaction(player);
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
