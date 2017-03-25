using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Boomlagoon.JSON;

public class Language : MonoBehaviour {
	
	//Singleton Member And Method
	static Language _instance;
	public static Language Instance {
		get {
			if( ! _instance ) {
				GameObject container = new GameObject("Language");
				_instance = container.AddComponent( typeof( Language ) ) as Language;
				_instance.InitLanguage ();
				DontDestroyOnLoad( container );
			}
			
			return _instance;
		}
	}
	
	public JSONObject LanguageText;

	public void InitLanguage()
	{
		TextAsset txt = Resources.Load("Text/Language",typeof(TextAsset)) as TextAsset;
		LanguageText = JSONObject.Parse(txt.text);
	}
	
	public string GetLanguage(string key)
	{
		if(LanguageText == null){
			InitLanguage();
		}
		return LanguageText.GetString(key);
	}

}
