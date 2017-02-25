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
		Application.LoadLevel (Application.loadedLevel);
	}
}
