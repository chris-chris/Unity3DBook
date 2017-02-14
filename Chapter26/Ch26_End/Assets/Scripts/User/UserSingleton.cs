using UnityEngine;
using System.Collections;
using System;
using Boomlagoon.JSON;

/*
UserSingleton 클래스는 현재 유저의 개인 정보 및 능력치 정보를 메모리 상에 들고 있는 싱글톤 클래스입니다.
서버로부터 /User/{유저아이디} API로 정보를 가져와서 여기에 저장합니다.
*/
//using Facebook;


public class UserSingleton : MonoBehaviour {

	// UserID 입니다. 서버 상에서 유저를 식별하는 고유번호입니다.
	public int UserID{
		get {
			return PlayerPrefs.GetInt("UserID");
		}
		set {
			PlayerPrefs.SetInt("UserID",value);
		}
	}
	
	// AccessToken은 서버 API에 접근하기 위한 API의 역할을 합니다.
	public string AccessToken{
		get {
			return PlayerPrefs.GetString("AccessToken");
		}
		set {
			PlayerPrefs.SetString("AccessToken",value);
		}
	}
	
	// 페이스북 아이디입니다. 페이스북의 고유번호입니다. App Scoped User ID입니다.
	public string FacebookID{
		get {
			return PlayerPrefs.GetString("FacebookID");
		}
		set {
			PlayerPrefs.SetString("FacebookID",value);
		}
	}
	
	// 페이스북에 인증할 수 있는 유저의 개인 비밀번호 키입니다.
	public string FacebookAccessToken{
		get {
			return PlayerPrefs.GetString("FacebookAccessToken");
		}
		set {
			PlayerPrefs.SetString("FacebookAccessToken",value);
		}
	}
	
	// 유저의 이름입니다. 기본으로 페이스북의 이름을 가져와 적용합니다.
	public string Name{
		get {
			return PlayerPrefs.GetString("Name");
		}
		set {
			PlayerPrefs.SetString("Name",value);
		}
	}
	
	// 페이스북의 프로필사진 주소입니다.
	public string FacebookPhotoURL{
		get {
			return PlayerPrefs.GetString("FacebookPhotoURL");
		}
		set {
			PlayerPrefs.SetString("FacebookPhotoURL",value);
		}
	}
	
	// 유저의 레벨, 경험치, 데미지, 체력, 방어력, 스피드, 데미지 레벨, 체력 레벨, 방어력 레벨, 스피드 레벨입니다. 
	// 다음 레벨까지 남은 경험치, 그리고 다음 레벨로 레벨업하기 위해 필요한 경험치 정보도 가지고 있습니다.
	public int 
		Level, Experience, 
		Damage, Health, Defense, Speed,
		DamageLevel, HealthLevel, DefenseLevel, SpeedLevel,
		Diamond, ExpAfterLastLevel, ExpForNextLevel;

	public JSONArray FriendList;
	
	//싱글톤 객체를 설정하는 부분입니다.
	static UserSingleton _instance;
	public static UserSingleton Instance {
		get {
			if( ! _instance ) {
				GameObject container = new GameObject("UserSingleton");
				_instance = container.AddComponent( typeof( UserSingleton ) ) as UserSingleton;

				DontDestroyOnLoad( container );
			}
			
			return _instance;
		}
	}

/*
저자의 경우 오는 페이스북 로그인 결과

 {"is_logged_in":true,
 "user_id":"10204997009661738",
 "access_token":"~~~",
 "access_token_expires_at":"01/01/0001 00:00:00"}
*/

	public void FacebookLogin(Action<bool, string> callback, int retryCount = 0)
	{

//		FB.Login("email",delegate(FBResult result) { 
//
//			if(result.Error != null && retryCount >= 3){
//				Debug.LogError(result.Error);
//
//				callback(false, result.Error);
//				return;
//			}
//
//			if(result.Error != null){
//				Debug.LogError(result.Error);
//
//				retryCount = retryCount + 1;
//				FacebookLogin(callback, retryCount);
//				return;
//			}
//
//			Debug.Log(result.Text);
//
//			Debug.Log("FB Login Result: " + result.Text);
//
//
//			// 페이스북 로그인 결과를 JSON 파싱합니다.
//			JSONObject obj = JSONObject.Parse(result.Text);
//			// 페이스북 로그인이 성공했는지 여부를 뜻하는 is_logged_in 변수 bool형
//			bool is_logged_in = obj["is_logged_in"].Boolean;
//
//			// 페이스북 아이디를 UserSingleton에 저장합니다. 
//			// 이 변수는 게임이 껏다 켜져도 유지되도록 환경변수에 저장하도록 구현되있습니다.
//			UserSingleton.Instance.FacebookID = obj["user_id"].Str;
//			UserSingleton.Instance.FacebookPhotoURL = "http://graph.facebook.com/" + UserSingleton.Instance.FacebookID + "/picture?type=square";
//
//			// 페이스북 액세스 토큰을 UserSingleton에 저장힙니다.
//			// 이 변수 역시 게임이 껏다 켜져도 유지됩니다.
//			UserSingleton.Instance.FacebookAccessToken = obj["access_token"].Str;
//
//			callback(true, result.Text);
//
//		});
	}

/*

저자의 경우 오는 페이스북 개인정보

{
	"id":"10204997009661738",
	"first_name":"Chris",
	"gender":"male",
	"last_name":"Song",
	"link":"https:\/\/www.facebook.com\/app_scoped_user_id\/10204997009661738\/",
	"locale":"ko_KR",
	"name":"Chris Song",
	"timezone":9,
	"updated_time":"2015-07-26T19:32:26+0000",
	"verified":true
}

*/

	public void LoadFacebookMe(Action<bool, string> callback, int retryCount = 0)
	{

//		FB.API("/me", HttpMethod.GET, delegate(FBResult result){
//			
//			if(result.Error != null && retryCount >= 3){
//				Debug.LogError(result.Error);
//
//				callback(false, result.Error);
//				return;
//			}
//
//			if(result.Error != null){
//				Debug.LogError("Error occured, start retrying.. " + result.Error);
//
//				retryCount = retryCount + 1;
//				LoadFacebookMe(callback, retryCount);
//				return;
//			}
//
//			Debug.Log(result.Text);
//
//			JSONObject meObj = JSONObject.Parse(result.Text);
//			UserSingleton.Instance.Name = meObj["name"].Str;
//
//			callback(true, result.Text);
//
//		});
	}
/*

{
	"data":
	{
		"height":130,
		"is_silhouette":false,
		"url":"https:\/\/scontent.xx.fbcdn.net\/hprofile-xaf1\/v\/t1.0-1\/c0.0.130.130\/p130x130\/12541039_10207028519968226_5641563107221179507_n.jpg?oh=a7caa674375a3d58e260d734957a7777&oe=5763C0E7",
		"width":130
	}
}

*/
	/*
	public void LoadFacebookPicture(Action<bool, string> callback, int retryCount = 0)
	{

		FB.API("/me/picture?width=128&height=128&redirect=false", HttpMethod.GET, delegate(FBResult result){

			if(result.Error != null && retryCount >= 3){
				Debug.LogError(result.Error);

				callback(false, result.Error);
				return;
			}

			if(result.Error != null){
				Debug.LogError("Error occured, start retrying.. " + result.Error);

				retryCount = retryCount + 1;
				LoadFacebookPicture(callback, retryCount);
				return;
			}

			Debug.Log(result.Text);

			JSONObject pictureObj = JSONObject.Parse(result.Text);
			UserSingleton.Instance.FacebookPhotoURL = "http://graph.facebook.com/" + UserSingleton.Instance.FacebookID + "/picture?type=square";//pictureObj["data"].Obj["url"].Str;

			callback(true, result.Text);

		});

	}*/
/*

{
	"data":
	[
		{"name":"Chris Song","id":"137418009931831"}
	],
	"paging":{"next":"https:\/\/graph.facebook.com\/v2.2\/10204997009661738\/friends?access_token=CAAUImeIGMdEBAHmbhkz25DFS8dsJCwlCVpzDbHEjmhcGIKe3S8xzkUGUDp7ebNusQLAWOF5vG6LBsiKytu27RR1v1TOkooQXlSzDvQShZBZCICIn2ySQdn7VbgurfBsw98gZAWMUmDvhwJZAdMMgmOamesWwudy7UTWqpjbBnmRTPxTjEIGiJpABWNtgLAldx71FIO8xGTbakudCfZCxR&limit=25&offset=25&__after_id=enc_AdC1zqmTQJITqEeR4rWCWOJTZArTf1aCACsU4ywiR5TJD6oLORQ64DdkN3sIEJTME0gKG3kYDnlZBiIfk3ZAbv8ibKr"},
	"summary":{"total_count":1108}
}

*/
	public void LoadFacebookFriend(Action<bool, string> callback, int retryCount = 0)
	{
		
//		FB.API("/me/friends", HttpMethod.GET, delegate(FBResult result){
//
//			if(result.Error != null && retryCount >= 3){
//				Debug.LogError(result.Error);
//
//				callback(false, result.Error);
//				return;
//			}
//
//			if(result.Error != null){
//				Debug.LogError("Error occured, start retrying.. " + result.Error);
//
//				retryCount = retryCount + 1;
//				LoadFacebookFriend(callback, retryCount);
//				return;
//			}
//
//			Debug.Log(result.Text);
//			JSONObject responseObj = JSONObject.Parse(result.Text);
//			JSONArray array = responseObj["data"].Array;
//
//			UserSingleton.Instance.FriendList = array; 
//
//			callback(true, result.Text);
//
//		});

	}


	//유저의 정보를 서버로부터 받아와서 최신 정보로 업데이트 하는 함수입니다. 
	//콜백변수로, 로드가 완료되면 다시 호출한 스크립트로 로드가 완료되었다고 호출할 수 있습니다.
	public void Refresh(Action callback)
	{
		HTTPClient.Instance.GET(Singleton.Instance.HOST + "/User/Info?UserID="+UserSingleton.Instance.UserID,
		                        delegate(WWW www)
		                        {
			Debug.Log(www.text);
			JSONObject response = JSONObject.Parse(www.text);
			int ResultCode = (int)response["ResultCode"].Number;
			JSONObject data = response["Data"].Obj;
			UserSingleton.Instance.Level = (int)data["Level"].Number;
			UserSingleton.Instance.Experience = (int)data["Experience"].Number;
			UserSingleton.Instance.Damage = (int)data["Damage"].Number;
			UserSingleton.Instance.Health = (int)data["Health"].Number;
			UserSingleton.Instance.Defense = (int)data["Defense"].Number;
			UserSingleton.Instance.Speed = (int)data["Speed"].Number;
			UserSingleton.Instance.DamageLevel = (int)data["DamageLevel"].Number;
			UserSingleton.Instance.HealthLevel = (int)data["HealthLevel"].Number;
			UserSingleton.Instance.DefenseLevel = (int)data["DefenseLevel"].Number;
			UserSingleton.Instance.SpeedLevel = (int)data["SpeedLevel"].Number;
			UserSingleton.Instance.Diamond = (int)data["Diamond"].Number;
			UserSingleton.Instance.ExpForNextLevel = (int)data["ExpForNextLevel"].Number;
			UserSingleton.Instance.ExpAfterLastLevel = (int)data["ExpAfterLastLevel"].Number;
			
			callback();		
		});
	}

}