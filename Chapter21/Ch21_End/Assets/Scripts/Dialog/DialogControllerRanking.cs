using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogControllerRanking : DialogController {
	
	public Text LabelTitle;
	public Text LabelScoreMsg;

	public Text LabelRankings;

	
	DialogDataRanking Data { 
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
		
		DialogManager.Instance.Regist( DialogType.Ranking, this );
	}

	
	public override void Build(DialogData data)
	{
		base.Build(data);
		
		if( ! (data is DialogDataRanking) ) {
			Debug.LogError("Invalid dialog data!");
			return;
		}
		
		Data = data as DialogDataRanking;
		LabelTitle.text = Data.Title;
		LabelScoreMsg.text = "Your score is : " + Data.Score;
		LabelRankings.text = Data.rankings;
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
