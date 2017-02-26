using UnityEngine;
using System.Collections;
using Boomlagoon.JSON;
using System.Collections.Generic;
using System;

public class RankSingleton : MonoBehaviour {

	public Dictionary<int, JSONObject> TotalRank, FriendRank;

	//싱글톤 객체를 설정하는 부분입니다.
	static RankSingleton _instance;
	public static RankSingleton Instance {
		get {
			if( ! _instance ) {
				GameObject container = new GameObject("RankSingleton");
				_instance = container.AddComponent( typeof( RankSingleton ) ) as RankSingleton;
				_instance.Init ();
				DontDestroyOnLoad( container );
			}

			return _instance;
		}
	}

	public void Init()
	{
		
		TotalRank = new Dictionary<int, JSONObject> ();
		FriendRank = new Dictionary<int, JSONObject> ();

	}

	public void LoadTotalRank(Action callback){

		HTTPClient.Instance.GET (Singleton.Instance.HOST + "/Rank/Total?Start=1&Count=50", delegate(WWW www) {
		
			Debug.Log("LoadTotalRank" + www.text);

			string response = www.text;

			JSONObject obj = JSONObject.Parse(response);

			JSONArray arr = obj["Data"].Array;

			foreach(JSONValue item in arr){
				int rank = (int)item.Obj["Rank"].Number;
				if(TotalRank.ContainsKey(rank)){
					TotalRank.Remove(rank);
				}
				TotalRank.Add(rank,item.Obj);
			}

			callback();
		});

	}

	public void LoadFriendRank(Action callback){

		JSONArray friendList = new JSONArray ();

		foreach(JSONValue item in UserSingleton.Instance.FriendList){
			JSONObject friend = item.Obj;
			friendList.Add (friend ["id"]);
		}

		JSONObject requestBody = new JSONObject ();
		requestBody.Add ("UserID", UserSingleton.Instance.UserID);
		requestBody.Add ("FriendList", friendList);

		HTTPClient.Instance.POST (Singleton.Instance.HOST + "/Rank/Friend", requestBody.ToString(), delegate(WWW www) {

			Debug.Log("LoadFriendRank" + www.text);

			string response = www.text;

			JSONObject obj = JSONObject.Parse(response);

			JSONArray arr = obj["Data"].Array;

			foreach(JSONValue item in arr){
				int rank = (int)item.Obj["Rank"].Number;
				if(FriendRank.ContainsKey(rank)){
					FriendRank.Remove(rank);
				}
				FriendRank.Add(rank,item.Obj);
			}

			callback();

		});

	}
}
