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


	// 싱글톤 객체가 초기화될 때 호출하는 초기화 함수입니다.
	public void Init()
	{
		// 전체랭킹을 저장하는 TotalRank 변수와 친구랭킹을 저장하는 FriendRank 변수를 할당합니다.
		TotalRank = new Dictionary<int, JSONObject> ();
		FriendRank = new Dictionary<int, JSONObject> ();

	}

	// 전체랭킹을 서버에서 조회하여 변수에 저장하는 함수입니다.
	public void LoadTotalRank(Action callback){
		// HTTP 콜로 서버의 전체랭킹 API를 조회합니다.
		Debug.Log(Singleton.Instance.HOST + "/Rank/Total?Start=1&Count=50");
		HTTPClient.Instance.GET (Singleton.Instance.HOST + "/Rank/Total?Start=1&Count=50", delegate(WWW www) {
			// API 응답결과를 디버그 로그이 찍습니다.
			Debug.Log("LoadTotalRank" + www.text);
			// 응답결과 본문을 string response 변수에 저장합니다.
			string response = www.text;
			// 응답결과를 JSON 오브젝트로 변환합니다.
			JSONObject obj = JSONObject.Parse(response);
			// 응답결과에서 랭킹 리스트가 들어있는 Data 필드에서 JSONArray를 추출합니다.
			JSONArray arr = obj["Data"].Array;
			// 랭킹 리스트를 하나씩 foreach문으로 조회합니다.
			foreach(JSONValue item in arr){
				// 해당 랭킹 유저 정보에서 Rank 순위를 조회합니다.
				int rank = (int)item.Obj["Rank"].Number;
				if(TotalRank.ContainsKey(rank)){ // 예외처리 이미 전체랭킹 Dictionary에 해당 랭킹 정보가 있다면, 지웁니다.
					TotalRank.Remove(rank);
				}
				// 해당 순위에 유저를 입력합니다.
				TotalRank.Add(rank,item.Obj);
			}
			// 전체 랭킹 로드완료했다고 callback()함수로 알려줍니다.
			callback();
		});

	}
	
	// 친구랭킹을 조회하여 친구랭킹 변수에 저장하는 함수입니다.
	public void LoadFriendRank(Action callback){
		// 친구랭킹조회를 위해 Facebook 로그인 결과 받아놓은 페이스북 친구리스트를 가져옵니다.
		JSONArray friendList = new JSONArray ();

		// 페이스북 친구리스트 결과 JSON에서 페이스북 아이디 배열을 추출합니다.
		foreach(JSONValue item in UserSingleton.Instance.FriendList){
			JSONObject friend = item.Obj;
			friendList.Add (friend ["id"]);
		}
		
		// 친구랭킹 API 호출시 Body에 넣을 친구리스트 JSONObject를 생성합니다.
		JSONObject requestBody = new JSONObject ();
		requestBody.Add ("UserID", UserSingleton.Instance.UserID);
		requestBody.Add ("FriendList", friendList);

		Debug.Log (requestBody.ToString ());

		// 친구랭킹 API로 POST 호출을 합니다.
		HTTPClient.Instance.POST (Singleton.Instance.HOST + "/Rank/Friend", requestBody.ToString(), delegate(WWW www) {
			// 친구랭킹 조회 결과를 디버그 로그에 출력합니다.
			Debug.Log("LoadFriendRank" + www.text);
			// 친구랭킹 조회 결과를 
			string response = www.text;
			// 친구랭킹 응답 본문을 JSONObject로 변환합니다.
			JSONObject obj = JSONObject.Parse(response);
			// 응답 본문 Data 필드에 있는 JSONArray를 추출합니다.
			JSONArray arr = obj["Data"].Array;
			// 친구랭킹 리스트를 하나씩 foreach로 조회합니다.
			foreach(JSONValue item in arr){
				// 해당 랭킹 유저의 Rank 순위를 추출합니다.
				int rank = (int)item.Obj["Rank"].Number;
				// 이미 해당 Rank의 유저가 변수에 있으면 지웁니다.
				if(FriendRank.ContainsKey(rank)){
					FriendRank.Remove(rank);
				}
				// 해당 친구 순위에 랭킹 정보를 할당합니다.
				FriendRank.Add(rank,item.Obj);
			}

			callback();

		});

	}
}
