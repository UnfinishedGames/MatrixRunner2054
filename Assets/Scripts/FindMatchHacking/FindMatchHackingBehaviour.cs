using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using FindMatchHacking;

public class FindMatchHackingBehaviour : MonoBehaviour
{
	public Text TextWindow;
	public Text HexWindow;
	public Text SideWindow;
	public InputField InputBox;
	public TextAsset WordList;
	public Slider TimerSlider;
	public float TimeInSeconds = 30;
	
	private FindMatchHacking.FindMatchHacking _hackingLogic;
	private float StartTime = 0;
	
	// Use this for initialization
	void Start ()
	{
		StartTime = Time.time;
		var seed = (int) (Time.time * 100.0f);
		_hackingLogic = new FindMatchHacking.FindMatchHacking(seed, WordList.text.Split('\n'));
		
		if (TextWindow)
		{
			TextWindow.text = _hackingLogic.GenerateList();
		}

		if (HexWindow)
		{
			HexWindow.text = "";
			foreach (var character in _hackingLogic.EncryptedList)
			{
				char[] characterArray = {character};
				byte[] ba = Encoding.UTF8.GetBytes(characterArray);
				var hexString = BitConverter.ToString(ba).Substring(0,2);
				HexWindow.text += hexString + " ";
			}

//			var hexString = BitConverter.ToString(ba);
//			hexString = hexString.Replace("-", " ");
//			HexWindow.text = hexString;
		}

		if (SideWindow)
		{
			Canvas.ForceUpdateCanvases();
			for (int line = 0; line < TextWindow.cachedTextGenerator.lines.Count; ++line)
			{
				SideWindow.text += "0x" + line.ToString("X4") + "\n";
			}
		}

		if (InputBox)
		{
			InputBox.characterLimit = _hackingLogic.CurrentWords.Max(w => w.Length);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (TimerSlider)
		{
			TimerSlider.value = 1.0f - (Time.time - StartTime) / TimeInSeconds;
		}

		if (Time.time - StartTime > TimeInSeconds)
		{
			Debug.Log("You Loose");
		}
	}

	public void CheckInput(string input)
	{
		TextWindow.text = "";
		HexWindow.text = "";
		var charList = _hackingLogic.GetListOfMatchingChars(input);

		foreach (var character in _hackingLogic.EncryptedList)
		{
			char[] characterArray = {character};
			byte[] ba = Encoding.UTF8.GetBytes(characterArray);
			var hexString = BitConverter.ToString(ba).Substring(0,2);
			
			if (charList.Contains(character))
			{
				TextWindow.text += "<color=red>" + character + "</color>";
				HexWindow.text += "<color=red>" + hexString + "</color>" + " ";
			}
			else
			{
				TextWindow.text += character;
				HexWindow.text += hexString + " ";
			}
		}

		HexWindow.text = HexWindow.text.TrimEnd();

		if (_hackingLogic.CheckPassword(input))
		{
			Debug.Log("Winning!");
		}

		InputBox.ActivateInputField();
	}
}
