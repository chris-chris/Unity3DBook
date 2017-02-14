using UnityEngine;
using System.Collections;
using System;

public class DialogDataRanking : DialogData {

	public string Title { 
		get; 
		private set; 
	}
	
	public int Score { 
		get; 
		private set; 
	}
	
	public Action<bool> Callback { 
		get; 
		private set; 
	}

	public string rankings {
		get;
		private set;
	}
	
	public DialogDataRanking(string title, int score, string rankings, Action<bool> callback = null)
		: base( DialogType.Ranking )
	{
		this.Title = title;
		this.Score = score;
		this.rankings = rankings;
		this.Callback = callback;
	}
}
