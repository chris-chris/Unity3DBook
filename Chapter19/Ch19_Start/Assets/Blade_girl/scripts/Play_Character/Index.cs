using UnityEngine;
using System.Collections;

public class Index : MonoBehaviour {
	public GUISkin customSkin;





	void OnGUI() 
	{
		GUI.skin = customSkin;

		
		GUI.Box (new Rect (112, 50, 656, 428),"");

		if (GUI.Button(new Rect(730, 55, 30, 30),""))
		{
			Destroy (gameObject);
		}
	
	}

}
