using UnityEngine;

public class PlayerCharacterSheet : MonoBehaviour
{
    public int MaskingkAttribute;
    public int DeceptionProgram;

    private CharacterSheet characterSheet;
    
    public CharacterSheet Bundeled
    {
        get { return characterSheet; }
    }

	void Start ()
    {
        characterSheet = new CharacterSheet();
        characterSheet.MaskingkAttribute = MaskingkAttribute;
        characterSheet.DeceptionProgram = DeceptionProgram;       	
	}
}
