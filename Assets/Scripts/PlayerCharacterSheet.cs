using UnityEngine;

public class PlayerCharacterSheet : MonoBehaviour
{
    public int MaskingkAttribute;
    public int DeceptionProgram;
    public int Money;

    private CharacterSheet characterSheet;
    
    public CharacterSheet Bundeled
    {
        get { return characterSheet; }
    }

    void Start ()
    {
        characterSheet = new CharacterSheet();
        UpdateCharacterSheet();
	}

    public void UpdateCharacterSheet()
    {
        characterSheet.MaskingkAttribute = MaskingkAttribute;
        characterSheet.DeceptionUtility = DeceptionProgram;
        characterSheet.Money = Money;
    }
}
