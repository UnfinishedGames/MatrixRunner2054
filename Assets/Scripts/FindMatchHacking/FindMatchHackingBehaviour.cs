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
	public Text MessageWindow;
	public InputField InputBox;
	public TextAsset WordList;
	public Slider TimerSlider;
	public float TimeInSeconds = 30;
	public AudioSource FailedSound;
	public AudioSource SuccessSound;

	private FindMatchHacking.FindMatchHacking _hackingLogic;
	private float _startTime = 0;
	private bool _pause = false;

	// Use this for initialization
	void Start ()
	{
		_startTime = Time.time;
		var seed = (int) (Time.time * 100.0f);
		_hackingLogic = new FindMatchHacking.FindMatchHacking(seed, WordList.text.Split('\n'));

		if (MessageWindow)
		{
			MessageWindow.transform.parent.gameObject.SetActive(false);
		}
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
			InputBox.ActivateInputField();
		}
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		if (TimerSlider && !_pause)
		{
			TimerSlider.value = 1.0f - (Time.time - _startTime) / TimeInSeconds;
		}

		if ((Time.time - _startTime > TimeInSeconds) && !_pause)
		{
			Debug.Log("You Loose");
			SetEndContition(EncounterStatus.PlayerLost);
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
			SetEndContition(EncounterStatus.PlayerWins);
			Debug.Log("Winning!");
			SuccessSound.Play();
		}
		else
		{
			FailedSound.Play();
		}

		InputBox.ActivateInputField();
	}

	public void SetEndContition(EncounterStatus status)
	{
		InputBox.enabled = false;
		_pause = true;
		if (MessageWindow)
		{
			MessageWindow.transform.parent.gameObject.SetActive(true);

			if (status == EncounterStatus.PlayerWins)
			{
				MessageWindow.text = "Successful hack!";
			}
			else if (status == EncounterStatus.PlayerLost)
			{
				MessageWindow.text = "You have been caught! You're screwed.";
				MessageWindow.color = Color.red;
			}
		}

		StartCoroutine(ExecuteAfterTime(5, status));
	}

	private IEnumerator ExecuteAfterTime(float time, EncounterStatus status)
	{
		yield return new WaitForSeconds(time);

		PersistentEncounterStatus.Instance.status = status;
		Debug.Log("Ending FindMatchHacking");
	}
}
