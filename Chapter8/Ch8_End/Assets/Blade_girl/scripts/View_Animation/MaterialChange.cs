using UnityEngine;
using System.Collections;

public class MaterialChange : MonoBehaviour {
	
	public Material[] mats;
	public GameObject frog;
	private int index = 0;

	public GUISkin customSkin;
	
	// Use this for initialization
	void Start () {
		frog.GetComponent<Renderer>().material = mats[index];
	
	}
	
	public void OnGUI() {

		GUI.skin = customSkin;

		GUILayout.BeginArea(new Rect(Screen.width / 2 - 100, Screen.height - 70, 200, 50));
		   GUI.Box(new Rect(10, 10, 190, 40),"");
		  
		GUI.Label(new Rect(62, 20, 100, 20), "Expression" + (index +1));
		
		if(GUI.Button(new Rect(15, 15, 30, 30), "<<")){
			index--;
			if(index < 0){
				index = mats.Length -1;
			}
			frog.GetComponent<Renderer>().material = mats[index];
		}
		
		if(GUI.Button(new Rect(165, 15, 30, 30), ">>")){
			index++;
			if(index > mats.Length -1){
				index = 0;
			}
			frog.GetComponent<Renderer>().material = mats[index];
		}
		GUILayout.EndArea ();
		
		
	
	// Update is called once per frame
	
	}
}
