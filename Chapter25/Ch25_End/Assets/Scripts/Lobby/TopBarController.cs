using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TopBarController : MonoBehaviour {

// txtName: 유저의 이름
// txtLevel: 유저의 레벨
// txtExp: 유저의 경험치
// txtDiamond: 유저의 다이아몬드 수
// sliderExp: 경험치 게이지 
	public Text txtName, txtLevel, txtExp, txtDiamaon;
	public Slider sliderExp;


	void Start () {
// 1) UpgradeController가 화면에 나타나면서 NotificationCenter에
// 캐릭터의 정보가 변경되면 자신의 UpdatePlayerData()함수를 호출하도록 등록합니다.
		NotificationCenter.Instance.Add(NotificationCenter.Subject.PlayerData,UpdatePlayerData);

// 2) 그리고 시작하자마자 먼저 UserSingleton에서 최신 캐릭터 정보를 화면에 반영하도록 
// UpdatePlayerData() 함수를 호출합니다.
		UpdatePlayerData();
	}

	void UpdatePlayerData()
	{
		
		txtName.text = UserSingleton.Instance.Name;
		txtLevel.text = "Lv " + UserSingleton.Instance.Level.ToString();
		txtExp.text = UserSingleton.Instance.ExpAfterLastLevel.ToString() + " / " + UserSingleton.Instance.ExpForNextLevel.ToString();
		sliderExp.maxValue = UserSingleton.Instance.ExpForNextLevel;
		sliderExp.value = UserSingleton.Instance.ExpAfterLastLevel;
		txtDiamaon.text = UserSingleton.Instance.Diamond.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
