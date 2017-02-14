using UnityEngine;
using System.Collections;


public class RankContent : MonoBehaviour {

	public RankCell[] rankCellList;

	public int rankContentHeight;

	string currentTab = "friend";

	public GameObject tabFriendRank, tabTotalRank, btnTabFriendRank, btnTabTotalRank;

	public void TabTotalRank()
	{
		RemoveRankCell ();

		tabFriendRank.SetActive (false);
		tabTotalRank.SetActive (true);

		btnTabFriendRank.SetActive (true);
		btnTabTotalRank.SetActive (false);
		currentTab = "total";
		LoadRankList ();

	}

	public void TabFriendRank()
	{

		RemoveRankCell ();

		tabFriendRank.SetActive (true);
		tabTotalRank.SetActive (false);

		btnTabFriendRank.SetActive (false);
		btnTabTotalRank.SetActive (true);

		currentTab = "friend";
		LoadRankList ();

	}
	public void RemoveRankCell()
	{
		var list = gameObject.GetComponentsInChildren<RankCell> ();
		foreach (var cell in list) {
			RankCellPool.Instance.ReleaseObject (cell.gameObject);
		}
	}

	public void LoadRankList()
	{
		Debug.Log ("LoadRankList()");
		
		if (currentTab == "total") {
			
			LoadTotalRank ();

		} else {
			
			LoadFriendRank ();

		}

	}

	public void LoadTotalRank()
	{
		Debug.Log ("LoadTotalRank()");

		var rankList = RankSingleton.Instance.TotalRank;
		float unit = 50f;
		float start_y = -40f;
		float content_height = 50f;

		foreach (var item in rankList) {
			
			var rank = item.Key;
			var user = item.Value;

			Debug.Log ("user data : " + user.ToString ());

			var obj = RankCellPool.Instance.GetObject ();

			obj.transform.SetParent (transform);

			var cell = obj.GetComponent<RankCell> ();

			cell.SetData (user);

			var rect = obj.GetComponent<RectTransform> ();
			//RectTransformExtensions.SetRightTopPosition
			rect.SetDefaultScale ();
			rect.anchoredPosition = new Vector2 (0f, start_y);
			rect.offsetMin = new Vector2 (20f, start_y);
			rect.offsetMax = new Vector2 (-20f, start_y);
			rect.SetHeight(40f);
			start_y = start_y - unit;
			content_height = content_height + unit;
		}
		GetComponent<RectTransform> ().SetHeight (content_height);

	}

	public void LoadFriendRank()
	{
		Debug.Log ("LoadFriendRank()");

		var rankList = RankSingleton.Instance.FriendRank;

		float unit = 50f;
		float start_y = -40f;
		float content_height = 50f;

		foreach (var item in rankList) {

			var rank = item.Key;
			var user = item.Value;

			Debug.Log ("user data : " + user.ToString ());

			var obj = RankCellPool.Instance.GetObject ();

			obj.transform.SetParent (transform);

			var cell = obj.GetComponent<RankCell> ();

			cell.SetData (user);

			var rect = obj.GetComponent<RectTransform> ();

			// Prefab으로 생성한 게임오브젝트는 Scale을 1,1,1로 재설정 SetDefaultScale ();
			rect.SetDefaultScale ();
			// Prefab으로 생성한 게임오브젝트는 Scale을 1,1,1로 재설정 SetDefaultScale ();
			rect.anchoredPosition = new Vector2 (0f, start_y);
			rect.offsetMin = new Vector2 (20f, start_y);
			rect.offsetMax = new Vector2 (-20f, start_y);
			rect.SetHeight(40f);
			start_y = start_y - unit;
			content_height = content_height + unit;
		}
		GetComponent<RectTransform> ().SetHeight (content_height);

	}

}
