using System;

// DialogDataConfirm는 예/아니오 팝업의 데이터를 저장하는 클래스입니다.
public class DialogDataConfirm : DialogData 
{
	// 제목(Title)을 저장하는 string 변수입니다. 
	public string Title { 
		get; 
		private set; 
	}
	
	// 팝업 내용(Message)을 저장하는 string 변수입니다.
	public string Message { 
		get; 
		private set; 
	}
	
	// 팝업에서 예/아니오 버튼을 클릭했을 때 호출되는 콜백을 저장하는 변수입니다.
	public Action<bool> Callback { 
		get; 
		private set; 
	}
	
	// DialogDataConfirm의 생성자입니다. 제목, 내용, 그리고 콜백 함수를 매개변수로 전달합니다.
	public DialogDataConfirm(string title, string message, Action<bool> callback = null)
		: base( DialogType.Confirm )
	{
		this.Title = title;
		this.Message = message;
		this.Callback = callback;
	}
}
