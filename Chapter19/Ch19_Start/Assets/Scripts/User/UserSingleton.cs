using UnityEngine;
using System.Collections;

public class UserSingleton : MonoBehaviour {

	public long UserID{
		get {
			string UserIDStr = PlayerPrefs.GetString("UserID");
			if(UserIDStr == "")
			{
				return 0;
			}else{
				return long.Parse(UserIDStr);
			}
		}
		set {
			PlayerPrefs.SetString("UserID",value.ToString());
		}
	}
	public string AccessToken{
		get {
			return PlayerPrefs.GetString("AccessToken");
		}
		set {
			PlayerPrefs.SetString("AccessToken",value);
		}
	}
	public string FacebookID{
		get {
			return PlayerPrefs.GetString("FacebookID");
		}
		set {
			PlayerPrefs.SetString("FacebookID",value);
		}
	}
	public string FacebookAccessToken{
		get {
			return PlayerPrefs.GetString("FacebookAccessToken");
		}
		set {
			PlayerPrefs.SetString("FacebookAccessToken",value);
		}
	}
	public string Name{
		get {
			return PlayerPrefs.GetString("Name");
		}
		set {
			PlayerPrefs.SetString("Name",value);
		}
	}
	public string FacebookPhotoURL{
		get {
			return PlayerPrefs.GetString("FacebookPhotoURL");
		}
		set {
			PlayerPrefs.SetString("FacebookPhotoURL",value);
		}
	}
	
	//Singleton Member And Method
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
}

