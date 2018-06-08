using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using FindMatchHacking;

public class FindMatchHackingBehaviour : MonoBehaviour
{
	public Text TextWindow;
	public InputField InputBox;

	private FindMatchHacking.FindMatchHacking _hackingLogic;
	// Use this for initialization
	void Start () 
	{
		_hackingLogic = new FindMatchHacking.FindMatchHacking((int)(Time.time*100.0f));
		
		if (TextWindow)
		{
			TextWindow.text = _hackingLogic.GenerateList();
		}

		if (InputBox)
		{
			InputBox.characterLimit = _hackingLogic.CurrentWords.Max(w => w.Length);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CheckInput(string input)
	{
		TextWindow.text = "";
		var charList = _hackingLogic.GetListOfMatchingChars(input);

		foreach (var character in _hackingLogic.EncryptedList)
		{
			if (charList.Contains(character))
			{
				TextWindow.text += "<color=red>" + character + "</color>";
			}
			else
			{
				TextWindow.text += character;
			}
		}


		if (_hackingLogic.CheckPassword(input))
		{
			Debug.Log("Winning!");
		}

		InputBox.ActivateInputField();
	}
}
