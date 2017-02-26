using UnityEngine;
// 유니티의 UI 모듈을 연결할 때에는 이 UnityEngine.UI 네임스페이스를 포함해야 합니다 
using UnityEngine.UI; 
using System.Collections;
using System.Text;
using Boomlagoon.JSON;
using System;

public class UpgradeController : MonoBehaviour {

// txtUpgradeStatus: 업그레이드 현황을 표시하는 텍스트 오브젝트
// txtHealth: 캐릭터의 체력 업그레이드 버튼의 텍스트 오브젝트입니다.
// txtDefense: 캐릭터의 방어력 업그레이드 버튼의 텍스트 오브젝트입니다.
// txtDamage: 캐릭터의 데미지 업그레이드 버튼의 텍스트 오브젝트입니다.
// txtSpeed: 캐릭터의 스피드 업그레이드 버튼의 텍스트 오브젝트입니다.
	public Text txtUpgradeStatus, txtHealth, txtDefense, txtDamage, txtSpeed;

	void Start () {
		
// 1) UpgradeController가 화면에 나타나면서 NotificationCenter에
// 캐릭터의 정보가 변경되면 자신의 UpdatePlayerData()함수를 호출하도록 등록합니다.
		NotificationCenter.Instance.Add(NotificationCenter.Subject.PlayerData,UpdatePlayerData);
		
// 2) 그리고 시작하자마자 먼저 UserSingleton에서 최신 캐릭터 정보를 화면에 반영하도록 
// UpdatePlayerData() 함수를 호출합니다.
		UpdatePlayerData();
		
	}

// UserSingleton에 저장된 유저의 데이터를 화면에 반영하는 함수입니다. 
	void UpdatePlayerData()
	{
		
// UserSingleton에 저장된 캐릭터의 4가지 능력치를 화면에 표시하는 스크립트입니다.
// string 끼리 + 연산은 성능에 좋지 않으므로, string.Format() 함수를 활용하여
// UserSingleton에 저장된 유저 능력치를 화면에 표시하겠습니다. 
		txtUpgradeStatus.text = string.Format("{0}\n{1}\n{2}\n{3}",
		                                      UserSingleton.Instance.Health,
		                                      UserSingleton.Instance.Defense,
		                                      UserSingleton.Instance.Damage,
		                                      UserSingleton.Instance.Speed);

// 캐릭터의 체력 레벨을 표시합니다.
		txtHealth.text = string.Format("+{0}", UserSingleton.Instance.HealthLevel);
// 캐릭터의 방어력 레벨을 표시합니다.
		txtDefense.text = string.Format("+{0}", UserSingleton.Instance.DefenseLevel);
// 캐릭터의 공격력 레벨을 표시합니다.
		txtDamage.text = string.Format("+{0}", UserSingleton.Instance.DamageLevel);
// 캐릭터의 스피드 레벨을 표시합니다.
		txtSpeed.text = string.Format("+{0}", UserSingleton.Instance.SpeedLevel);

	}

	public void ConfirmUpgrade(string UpgradeType)
	{

		DialogDataConfirm confirm = new DialogDataConfirm ("","",delegate(bool yn) {
			
			if(yn == true){
				Upgrade(UpgradeType);
			}
			
		});

		DialogManager.Instance.Push (confirm);

	}

	public void Upgrade(string UpgradeType)
	{
		JSONObject obj = new JSONObject();
		obj.Add("UserID", UserSingleton.Instance.UserID);
		obj.Add("UpgradeType", UpgradeType);
		HTTPClient.Instance.POST(Singleton.Instance.HOST + "/Upgrade",obj.ToString(),delegate(WWW www) {
			Debug.Log (www.text);
			JSONObject res = JSONObject.Parse(www.text);
			int ResultCode = (int)res["ResultCode"].Number;
			if(ResultCode == 1){ // Success!
				// Upgrade Success => Load User data again
				UserSingleton.Instance.Refresh(delegate() {
					NotificationCenter.Instance.Notify(NotificationCenter.Subject.PlayerData);
				});
				// Alert Dialog
				DialogDataAlert alert = new DialogDataAlert(Language.Instance.GetLanguage("Upgrade Success Title"),
				                                            Language.Instance.GetLanguage("Upgrade Success"),delegate() {
					
				} );
				DialogManager.Instance.Push(alert);
				
				
			}else if (ResultCode == 4) // Max Level
			{
				// Alert Dialog
				DialogDataAlert alert = new DialogDataAlert(Language.Instance.GetLanguage("Upgrade Failed Title"),
				                                            Language.Instance.GetLanguage("Max Level"), delegate() {
					
				});
				DialogManager.Instance.Push(alert);
				
			}else if(ResultCode == 5) // Not enough diamond
			{
				// Alert Dialog
				DialogDataAlert alert = new DialogDataAlert(Language.Instance.GetLanguage("Upgrade Failed Title"),
				                                            Language.Instance.GetLanguage("Not Enouhg Diamond"), delegate() {
					
				});
				DialogManager.Instance.Push(alert);
				
			}
		});
	}

	public void UpgradeHealth()
	{
		Debug.Log ("UpgradeHealth");
		
		String title = Language.Instance.GetLanguage ("Health Upgrade Confirm");
		String message = String.Format(Language.Instance.GetLanguage("Diamonds are required"),UserSingleton.Instance.HealthLevel * 2);

		DialogDataConfirm confirm = new DialogDataConfirm(
			title,
			message,
			delegate(bool yn){

			if(yn){
				
				Upgrade ("Health");

			}

		});
		DialogManager.Instance.Push (confirm);

	}
	public void UpgradeDefense()
	{
		Debug.Log ("UpgradeDefense");
		
		String title = Language.Instance.GetLanguage ("Defense Upgrade Confirm");
		String message = String.Format(Language.Instance.GetLanguage("Diamonds are required"),UserSingleton.Instance.DefenseLevel * 2);

		DialogDataConfirm confirm = new DialogDataConfirm(
			title,
			message,
			delegate(bool yn){
			
			if(yn){
				
				Upgrade ("Defense");
				
			}
			
		});
		
		DialogManager.Instance.Push (confirm);
	}
	public void UpgradeDamage()
	{
		Debug.Log ("UpgradeDamage");
		
		String title = Language.Instance.GetLanguage ("Damage Upgrade Confirm");
		String message = String.Format(Language.Instance.GetLanguage("Diamonds are required"),UserSingleton.Instance.DamageLevel * 2);

		DialogDataConfirm confirm = new DialogDataConfirm(
			title,
			message,
			delegate(bool yn){
			
			if(yn){
				
				Upgrade ("Damage");
				
			}
			
		});
		DialogManager.Instance.Push (confirm);

	}
	public void UpgradeSpeed()
	{
		Debug.Log ("UpgradeSpeed");

		String title = Language.Instance.GetLanguage ("Speed Upgrade Confirm");
		String message = String.Format (Language.Instance.GetLanguage ("Diamonds are required"), UserSingleton.Instance.SpeedLevel * 2);

		DialogDataConfirm confirm = new DialogDataConfirm(
			title,
			message,
			delegate(bool yn){
				
			if(yn){
				
				Upgrade ("Speed");
				
			}
			
		});
		DialogManager.Instance.Push (confirm);
	}
}