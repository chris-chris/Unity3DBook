using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class UnityAdsSingleton : MonoBehaviour
{
	// Serialize private fields
	//  instead of making them public.
	[SerializeField] string iosGameId = "1062166";
	[SerializeField] string androidGameId = "1062165";
	[SerializeField] bool enableTestMode;

	public void Init ()
	{
		string gameId = null;

		#if UNITY_IOS // If build platform is set to iOS...
		gameId = iosGameId;
		#elif UNITY_ANDROID // Else if build platform is set to Android...
		gameId = androidGameId;
		#endif

		if (string.IsNullOrEmpty(gameId)) { // Make sure the Game ID is set.
			Debug.LogError("Failed to initialize Unity Ads. Game ID is null or empty.");
		} else if (!Advertisement.isSupported) {
			Debug.LogWarning("Unable to initialize Unity Ads. Platform not supported.");
		} else if (Advertisement.isInitialized) {
			Debug.Log("Unity Ads is already initialized.");
		} else {
			Debug.Log(string.Format("Initialize Unity Ads using Game ID {0} with Test Mode {1}.",
				gameId, enableTestMode ? "enabled" : "disabled"));
			Advertisement.Initialize(gameId, enableTestMode);
		}
	}

	//싱글톤 객체를 설정하는 부분입니다.
	static UnityAdsSingleton _instance;
	public static UnityAdsSingleton Instance {
		get {
			if( ! _instance ) {
				GameObject container = new GameObject("UnityAdsSingleton");
				_instance = container.AddComponent( typeof( UnityAdsSingleton ) ) as UnityAdsSingleton;

				DontDestroyOnLoad( container );
			}

			return _instance;
		}
	}
}