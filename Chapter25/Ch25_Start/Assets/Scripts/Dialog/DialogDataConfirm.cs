using System;

public class DialogDataConfirm : DialogData 
{
	public string Title { 
		get; 
		private set; 
	}

	public string Message { 
		get; 
		private set; 
	}

	public Action<bool> Callback { 
		get; 
		private set; 
	}

	public DialogDataConfirm(string title, string message, Action<bool> callback = null)
		: base( DialogType.Confirm )
	{
		this.Title = title;
		this.Message = message;
		this.Callback = callback;
	}
}
