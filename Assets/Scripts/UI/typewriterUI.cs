using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class typewriterUI : MonoBehaviour
{
	Text _text;
	TMP_Text _tmpProText;
	string writer;

	[SerializeField] float delayBeforeStart = 0f;
	[SerializeField] float timeBtwChars = 0.1f;
	[SerializeField] string leadingChar = "";
	[SerializeField] bool leadingCharBeforeDelay = false;

	private string playerPrefsKey;

	// Use this for initialization
	void Start()
	{
		_text = GetComponent<Text>()!;
		_tmpProText = GetComponent<TMP_Text>()!;

		playerPrefsKey = $"{SceneManager.GetActiveScene().name}_{gameObject.name}_TypewriterDone";

		if (_text != null)
        {
			writer = _text.text;

			if (PlayerPrefs.GetInt(playerPrefsKey, 0) == 1)
			{
				_text.text = writer;
			}
			else
			{
				_text.text = "";
				StartCoroutine(TypeWriterText());
			}
		}

		if (_tmpProText != null)
		{
			writer = _tmpProText.text;

			if (PlayerPrefs.GetInt(playerPrefsKey, 0) == 1)
			{
				_tmpProText.text = writer;
			}
			else
			{
				_tmpProText.text = "";
				StartCoroutine(TypeWriterTMP());
			}
		}
	}

	IEnumerator TypeWriterText()
	{
		_text.text = leadingCharBeforeDelay ? leadingChar : "";

		yield return new WaitForSeconds(delayBeforeStart);

		foreach (char c in writer)
		{
			if (_text.text.Length > 0)
			{
				_text.text = _text.text.Substring(0, _text.text.Length - leadingChar.Length);
			}
			_text.text += c;
			_text.text += leadingChar;
			yield return new WaitForSeconds(timeBtwChars);
		}

		if(leadingChar != "")
        {
			_text.text = _text.text.Substring(0, _text.text.Length - leadingChar.Length);
		}

		PlayerPrefs.SetInt(playerPrefsKey, 1);
		PlayerPrefs.Save();
	}

	IEnumerator TypeWriterTMP()
    {
        _tmpProText.text = leadingCharBeforeDelay ? leadingChar : "";

        yield return new WaitForSeconds(delayBeforeStart);

		foreach (char c in writer)
		{
			if (_tmpProText.text.Length > 0)
			{
				_tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
			}
			_tmpProText.text += c;
			_tmpProText.text += leadingChar;
			yield return new WaitForSeconds(timeBtwChars);
		}

		if (leadingChar != "")
		{
			_tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
		}

		PlayerPrefs.SetInt(playerPrefsKey, 1);
		PlayerPrefs.Save();
	}
}