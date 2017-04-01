using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boomlagoon.JSON;

public class Language : MonoBehaviour {

	static Language _instance;
	public static Language Instance {
		get {
			if (!_instance) {
				GameObject container = new GameObject ("Language");
				_instance = container.AddComponent (typeof(Language)) as Language;
				DontDestroyOnLoad (container);
			}
			return _instance;
		}
	}

	public JSONObject LanguageText;

	public void InitLanguage()
	{
		TextAsset txt = Resources.Load ("Text/Language", typeof(TextAsset)) as TextAsset;
		LanguageText = JSONObject.Parse (txt.text);
	}

	public string GetLanguage(string key)
	{
		if (LanguageText == null) {
			InitLanguage ();
		}
		return LanguageText.GetString (key);
	}

}
