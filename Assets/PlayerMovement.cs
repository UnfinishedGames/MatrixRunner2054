using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public GameObject leftNode;
    public GameObject rightNode;
    public GameObject currentNode;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (leftNode != null)
            {
                rightNode = currentNode;
                currentNode = leftNode;
                leftNode = null;
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (rightNode != null)
            {
                leftNode = currentNode;
                currentNode = rightNode;
                rightNode = null;
            }
        }

        UpdateView();
    }

    private void UpdateView()
    {
        this.transform.position = currentNode.transform.position;
    }
}
