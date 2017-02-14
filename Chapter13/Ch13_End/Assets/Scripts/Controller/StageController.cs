using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Boomlagoon.JSON;
using System.Collections.Generic;

// 스테이지를 관리하는 콘트롤러입니다. 스테이지의 시작과 종료 시점에 스테이지의 시작과 마감을 처리합니다. 
// 스테이지에서 획득한 포인트도 여기에서 관리합니다.
public class StageController : MonoBehaviour {

	// 스테이지 콘트롤러의 인스턴스를 저장하는 static 변수입니다.
	public static StageController Instance;
	// StagePoint는 현재 스테이지에서 쌓은 포인트를 저장하는 변수입니다. 
	public int StagePoint = 0;
	// 현재 포인트를 표시하는 Text 게임 오브젝트입니다. 
	public Text PointText;

	// Use this for initialization
	void Start () {
		// Instance 변수에 현재 클래스의 인스턴스를 설정합니다.
		Instance = this;
		// 다이얼로그 데이터를 하나 생성합니다. 제목하고 내용, 그리고 콜백함수를 매개변수로 전달합니다. 
		DialogDataAlert alert = new DialogDataAlert("START", "Game Start!", delegate() {
			Debug.Log ("OK Pressed");
		});
		// 생성한 Alert 다이얼로그 데이터를 DialogManager에 추가합니다.
		DialogManager.Instance.Push(alert);

	}
	// StageController에서는 AddPoint() 함수로 유저가 획득한 포인트를 저장합니다.
	public void AddPoint(int Point)
	{
		// 기존 점수에 새로 획득한 점수를 추가합니다.
		StagePoint += Point;
		// 현재 포인트를 화면의 Text 게임오브젝트에 갱신하여 반영합니다.
		PointText.text = StagePoint.ToString();
	}

	public void FinishGame()
	{
		// DialogDataConfirm 클래스의 인스턴스를 생성합니다.
		// 이때 제목(Title), 내용(Message), 콜백함수(delegate(bool yn))을 매개변수로 전달합니다.
        DialogDataConfirm confirm = new DialogDataConfirm("Restart?", "Please press OK if you want to restart the game.", 
			delegate(bool yn) {
			if(yn) {
				Debug.Log ("OK Pressed");
				Application.LoadLevel (Application.loadedLevel);              
			}else{
				Debug.Log ("Cancel Pressed");
				Application.Quit();
			}
		});
		// 생성한 다이얼로그 데이터를 다이얼로그 매니저에게 전달합니다.
		DialogManager.Instance.Push(confirm);
	}
}
