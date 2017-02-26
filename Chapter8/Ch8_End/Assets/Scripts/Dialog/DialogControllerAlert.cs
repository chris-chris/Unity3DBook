using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// 확인버튼 하나만 있는 다이얼로그콘트롤러(DialogController)입니다.
public class DialogControllerAlert : DialogController
{
	// 제목(Title)을 설정하기 위해 Text 게임오브젝트를 연결하는 변수입니다.
	public Text LabelTitle;
	// 내용(Message)을 설정하기 위해 Text 게임오브젝트를 연결하는 변수입니다.
	public Text LabelMessage;
	
	// 현재 클래스에 전달될 알림창의 데이터 객체를 선언합니다.
	DialogDataAlert Data {
		get;
		set;
	}

	public override void Awake ()
	{
		base.Awake ();
	}

	public override void Start ()
	{
		base.Start ();
		// DialogManager에 현재 이 다이얼로크 콘트롤러 클래스가 확인창을 다룬다는 사실을 등록합니다.
		DialogManager.Instance.Regist( DialogType.Alert, this );
	}
	
	// 확인 팝업창이 생성될 때 호출되는 함수입니다.
    public override void Build(DialogData data)
    {
		base.Build(data);
		// 데이터가 없는데 Build를 하면 로그를 남기고 예외처리를 합니다.
		if( ! (data is DialogDataAlert) ) {
			Debug.LogError("Invalid dialog data!");
			return;
		}
		
		// DialogDataAlert로 데이터를 받고 화면의 제목과 메시지의 내용을 입력합니다.
		Data = data as DialogDataAlert;
		LabelTitle.text = Data.Title;
		LabelMessage.text = Data.Message;
    }

    public void OnClickOK()
    {
		// 확인 버튼을 누르면, Callback 함수를 호출합니다. 
        // calls child's callback
        if (Data!=null && Data.Callback != null)
            Data.Callback();
		// 모든 과정이 끝났으므로, 현재 팝업을 DialogManager에서 제거합니다.
		DialogManager.Instance.Pop();
    }
}
