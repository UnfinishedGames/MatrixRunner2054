using UnityEngine;

public class IceMovement : MonoBehaviour
{
    const int numberOfDirections = 3;

    public Node currentNode;

    private System.Random random;
    private IEncounter myEncounter;
    private Node startNode;

    public IEncounter getEncounter
    {
        get { return myEncounter; }
    }

    void Start()
    {
        startNode = currentNode;
        InvokeRepeating("Move", 1.0f, 1.0f);
        random = new System.Random((int)System.DateTime.Now.Ticks);
        myEncounter = GetComponentInChildren<IEncounter>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Stay()
    {
        CancelInvoke("Move");
    }

    public void Reset()
    {
        InvokeRepeating("Move", 1.0f, 1.0f);
        currentNode = startNode;
        UpdateView();
    }

    private void Move()
    {
        Node newNode = currentNode;

        while (currentNode == newNode)
        {
            var newDirection = random.Next(0, numberOfDirections + 1);
            newNode = currentNode.getNeighbour((Direction)newDirection);
        }
        currentNode = newNode;
        UpdateView();
    }

    private void UpdateView()
    {
        if (currentNode != null)
        {
            transform.position = currentNode.transform.position;
        }
    }
}
