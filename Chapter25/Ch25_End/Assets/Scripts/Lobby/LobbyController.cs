using UnityEngine;
using System.Collections;

public class LobbyController : MonoBehaviour {



	// Use this for initialization
	void Start () {
		Screen.SetResolution(1280, 720, true); 

		RankCellPool.Instance.Init ();

	}
	
	public void GoGame()
	{
		Application.LoadLevel("Game");
	}

	public void GoRank()
	{
		

	}

	public void CloseRank()
	{


	}

}
