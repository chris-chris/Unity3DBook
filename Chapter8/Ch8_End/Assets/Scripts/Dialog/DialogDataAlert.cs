using System;

// DialogDataAlert는 확인 팝업의 데이터를 저장하는 클래스입니다.
public class DialogDataAlert : DialogData 
{
	
	// 제목을 저장하는 string 변수입니다.
	public string Title { 
		get; 
		private set; 
	}
	
	// 팝업 창의 본문을 저장하는 string 변수입니다.
	public string Message { 
		get; 
		private set; 
	}

	// 유저가 확인 버튼을 눌렀을 때 호출되는 콜백함수를 저장하는 변수입니다.
	public Action Callback {
		get; 
		private set; 
	}

	// 새로운 클래스를 생성할 때 변수들을 같이 전달해주어 객체를 생성하는 생성자입니다.
	public DialogDataAlert(string title, string message, Action callback = null)
		: base( DialogType.Alert )
	{
		this.Title = title;
		this.Message = message;
		this.Callback = callback;
	}
}
