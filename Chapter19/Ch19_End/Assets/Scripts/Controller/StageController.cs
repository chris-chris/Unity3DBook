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
		DialogDataAlert alert = new DialogDataAlert("START", "Game Start!", delegate() {
			Debug.Log ("OK Pressed");
		});

		DialogManager.Instance.Push(alert);

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
		body.Add("Point", StagePoint.ToString());
		
		HTTPClient.Instance.POST (
			Singleton.Instance.HOST + "/UpdateResult/Post",
			body.ToString(),
			delegate(WWW obj) {
				JSONObject json = JSONObject.Parse(obj.text);
				Debug.Log("Response is : " + json.ToString());

				Application.LoadLevel("Lobby");

		    }
		);

	}
}
