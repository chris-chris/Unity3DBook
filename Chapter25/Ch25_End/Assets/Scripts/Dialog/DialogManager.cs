using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DialogType
{
	Alert,
	Confirm,
	Ranking
}

public sealed class DialogManager
{

    List<DialogData> _dialogQueue;
    Dictionary<DialogType, DialogController> _dialogMap;
	DialogController _currentDialog;

	private static DialogManager instance = new DialogManager();

	public static DialogManager Instance
	{
		get
		{
			return instance;
		}
	}
	
    private DialogManager()
    {
		_dialogQueue = new List<DialogData>();
		_dialogMap = new Dictionary<DialogType, DialogController>();

        //Prepare();
    }

	public void Prepare()
	{
		foreach( var pairs in _dialogMap )
		{
			DialogController controller = pairs.Value.GetComponent<DialogController>();
			controller.Close(null);
		}
	}

	public void Regist( DialogType type, DialogController controller )
	{
		_dialogMap[type] = controller;
	}

    public void Push(DialogData data)
	{
		Debug.Log("PUSH");
		_dialogQueue.Add(data);

        if (_currentDialog == null)
        {
			Debug.Log("ShowNext()");
            ShowNext();
		}
    }

    public void Pop()
    {
		if (_currentDialog != null){
        	_currentDialog.Close(
				delegate {
					_currentDialog = null;
			
					if (_dialogQueue.Count > 0)
					{
						ShowNext();
					}
				}
			);
		}
	}
	
	private void ShowNext()
    {
		DialogData next = _dialogQueue[0];
		Debug.Log (next.Type.ToString());
		DialogController controller = _dialogMap[next.Type].GetComponent<DialogController>();
		_currentDialog = controller;
		_currentDialog.Build(next);
		_currentDialog.Show( delegate {} );
		
		_dialogQueue.RemoveAt(0);
    }

	public bool IsShowing()
	{
		return _currentDialog != null;
	}
}
