using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogControllerConfirm : DialogController
{
	public Text LabelTitle;
	public Text LabelMessage;

	DialogDataConfirm Data { 
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

		DialogManager.Instance.Regist( DialogType.Confirm, this );
	}

    public override void Build(DialogData data)
    {
        base.Build(data);

		if( ! (data is DialogDataConfirm) ) {
			Debug.LogError("Invalid dialog data!");
			return;
		}

		Data = data as DialogDataConfirm;
		LabelTitle.text = Data.Title;
		LabelMessage.text = Data.Message;
    }

    public void OnClickOK()
    {
		if (Data.Callback != null)
			Data.Callback( true );

		Debug.Log("OnClickOK");

		// dismiss
		DialogManager.Instance.Pop();
    }

    public void OnClickCancel()
    {
		if (Data.Callback != null)
			Data.Callback( false );

        // dismiss
		DialogManager.Instance.Pop();
    }
}
