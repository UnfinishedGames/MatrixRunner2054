using UnityEngine;

public class SystemComponentSheet : MonoBehaviour
{
    public int SecurityCode;
    public int SystemRating;
    public int IceRating;

    private ComponentSheet componentSheet;

    public ComponentSheet Bundeled
    {
        get { return componentSheet; }
    }

    void Start()
    {
        componentSheet = new ComponentSheet();
        componentSheet.SecurityCode = SecurityCode;
        componentSheet.SystemRating = SystemRating;
        componentSheet.IceRating = IceRating;
    }
}
