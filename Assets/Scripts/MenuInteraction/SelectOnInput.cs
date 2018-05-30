using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour
{
    public EventSystem EventSystem;
    public GameObject SelectedObject;

    private bool buttonSelected;

    void Update()
    {
        if (Input.GetAxisRaw("Vertical") != 0 && !buttonSelected)
        {
            EventSystem.SetSelectedGameObject(SelectedObject);
            buttonSelected = true;
        }
    }

    private void OnDisable()
    {
        buttonSelected = false;
    }
}
