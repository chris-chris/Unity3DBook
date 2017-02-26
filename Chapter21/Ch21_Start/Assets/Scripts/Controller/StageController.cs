using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Boomlagoon.JSON;
using System.Collections.Generic;

public class StageController : MonoBehaviour {

	public static StageController Instance;

	public int StagePoint = 0;

	public Text PointText;

	public enum Days {Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday};

	// Use this for initialization
	void Start () {
		Instance = this;

		SlimePool.Instance.Init();
		DamageTextPool.Instance.Init();
		SkillAttack1Pool.Instance.Init();
		SkillAttack2Pool.Instance.Init ();

	}

	public void AddPoint(int Point)
	{
		StagePoint += Point;
		PointText.text = StagePoint.ToString();
	}

	public void FinishGame()
	{
		JSONObject body = new JSONObject();
		body.Add("UserID", UserSingleton.Instance.UserID);
		body.Add("Point", StagePoint);
		
		HTTPClient.Instance.POST (
			"http://unity-action.azurewebsites.net/UpdateResult",
			body.ToString(),
			delegate(WWW obj) {
				JSONObject json = JSONObject.Parse(obj.text);
				Debug.Log("Response is : " + json.ToString());
			
				UserSingleton.Instance.Refresh(delegate() {
					Application.LoadLevel ("Lobby"); // 
				});

		    }
		);

	}
	/*
	private void SignIn(){

		JSONObject body = new JSONObject();
		body.Add("FacebookID", "9876");
		body.Add("FacebookName", "Chris");
		body.Add("FacebookPhotoURL", "http://www/1.jpg");
		
		HTTPClient.Instance.POST (
			"http://unity-action.azurewebsites.net/Login",
			body.ToString(),
			delegate(WWW obj) {
			JSONObject json = JSONObject.Parse(obj.text);
			Debug.Log("Response is : " + json.ToString());
		}
		);

	}*/

	// Get Ranking list From server
	private void GetRanking(){
		HTTPClient.Instance.GET (
			"http://unity-action.azurewebsites.net/Rank/1/50",
			delegate(WWW obj) {
				Debug.Log(obj.text);
				// Dialog Push
				JSONObject result = JSONObject.Parse(obj.text);
				JSONArray jarr = result.GetArray("Data");
			
				string rankings = "";
				for(int i=0;i<jarr.Length;i++){
					rankings += jarr[i].Obj["Rank"] + ". " + jarr[i].Obj.GetString("FacebookName") + " \t\tscore :" + jarr[i].Obj["Point"] + "\n\n";
				}
				
				DialogDataRanking ranking = new DialogDataRanking("Game Over", StagePoint, rankings, delegate(bool yn) {
					if(yn)
					{
						Debug.Log ("OK Pressed");
						Application.LoadLevel ("Lobby"); // 
					}else{
						Debug.Log ("Cancel Pressed");
						Application.Quit();
					}
				});
				DialogManager.Instance.Push(ranking);
			}
		);

	}
}
