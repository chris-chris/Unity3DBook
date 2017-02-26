using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Boomlagoon.JSON;

public class LobbyController : MonoBehaviour {

	public GameObject rankContent;


	// Use this for initialization
	void Start () {
		Screen.SetResolution(1280, 720, true); 

		RankCellPool.Instance.Init ();
	
		HTTPClient.Instance.GET ("https://api.korbit.co.kr/v1/ticker?currency_pair=btc_krw", delegate(WWW www) {
			Debug.Log(www.text);
			JSONObject obj = JSONObject.Parse(www.text);
			Debug.Log(obj["last"]);
		}
		);

	}
	
	public void GoGame()
	{
		//Application.LoadLevel("Game");
		SceneManager.LoadScene ("Game");

	}

	public void GoRank()
	{
		
		rankContent.SetActive (true);
		rankContent.GetComponentInChildren<RankContent>().LoadRankList ();

	}

	public void CloseRank()
	{

		rankContent.SetActive (false);

	}

}
