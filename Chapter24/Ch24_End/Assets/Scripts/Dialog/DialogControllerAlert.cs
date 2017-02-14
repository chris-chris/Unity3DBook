using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogControllerAlert : DialogController
{
	public Text LabelTitle;
	public Text LabelMessage;

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

		DialogManager.Instance.Regist( DialogType.Alert, this );
	}

    public override void Build(DialogData data)
    {
		base.Build(data);
		
		if( ! (data is DialogDataAlert) ) {
			Debug.LogError("Invalid dialog data!");
			return;
		}
		
		Data = data as DialogDataAlert;
		LabelTitle.text = Data.Title;
		LabelMessage.text = Data.Message;
    }

    public void OnClickOK()
    {
        // calls child's callback
        if (Data!=null && Data.Callback != null)
            Data.Callback();

		DialogManager.Instance.Pop();
    }
}
