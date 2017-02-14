using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Dialog의 종류를 구분하는 enum 변수입니다.
// DialogType.Alert , DialogType.Confirm 이런 식으로 다이얼로그의 유형을 지정할 수 있습니다.
public enum DialogType
{
	Alert,
	Confirm,
	Ranking
}

// DialogMAnager는 다이얼로그들을 관리하는 관리 클래스입니다.
public sealed class DialogManager
{
	// 유저에게 보여줄 팝업창들을 저장해놓는 리스트입니다. 리스트에 들어온 순서대로 꺼내서 하나씩 유저에게 보여줍니다.
    List<DialogData> _dialogQueue;
	// 다이얼로그 타입에 따른 콘트롤러를 매핑한 Dictionary 변수입니다. 
	// DialogType.Alert 유형은 DialogControllerAlert
    Dictionary<DialogType, DialogController> _dialogMap;
	// 현재 화면에 떠있는 다이얼로그를 지정합니다.
	DialogController _currentDialog;
	// 싱글톤 패턴으로 하나의 인스턴스를 전역적으로 공유하기 위해 instance를 여기에 생성합니다.
	private static DialogManager instance = new DialogManager();

	public static DialogManager Instance
	{
		get
		{
			return instance;
		}
	}
	
	// 생성자입니다. 클래스의 인스턴스가 생성될 때 인스턴스 변수들을 초기화해줍니다.
    private DialogManager()
    {
		_dialogQueue = new List<DialogData>();
		_dialogMap = new Dictionary<DialogType, DialogController>();

    }
	
	// Regist 함수로 특정 DialogType에 매칭되는 DialogController를 지정합니다.
	public void Regist( DialogType type, DialogController controller )
	{
		_dialogMap[type] = controller;
	}
	// Push함수로 DialogData를 추가합니다. 
    public void Push(DialogData data)
	{
		// 다이얼로그 리스트를 저장하는 변수에 새로운 다이얼로그 데이터를 추가합니다. 
		_dialogQueue.Add(data);

        if (_currentDialog == null)
		{
			// 다음으로 보여줄 
            ShowNext();
		}
    }
	
	// Pop 함수로 리스트에서 마지막으로열린 다이얼로그를 닫습니다.
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
		// 다이얼로그를 리스트에서 첫번째 멤버를 가져옵니다.
		DialogData next = _dialogQueue[0];
		// 가져온 멤버의 다이얼로그 유형을 확인합니다. 
		// 그래서 그 다이얼로그 유형에 맞는 다이얼로그 콘트롤러(DialogController)를 조회합니다.
		DialogController controller = _dialogMap[next.Type].GetComponent<DialogController>();
		// 조회한 다이얼로그 콘트롤러를 현재 열린 팝업의 다이얼로그 콘트롤러로 지정합니다.
		_currentDialog = controller;
		// 현재 보여주열 다이럴로그 데이터를 화면에 표시합니다.
		_currentDialog.Build(next);
		// 다이얼로그를 화면에 보여주는 애니메이션을 시작합니다.
		_currentDialog.Show( delegate {} );
		// 다이얼로그 리스트에서 꺼내온 데이터를 제거합니다.
		_dialogQueue.RemoveAt(0);
    }

	// 현재 팝업 윈도우가 표시되있는지 확인하는 함수입니다.
	// _currentDialog가 비어있으면, 현재 화면에 팝업이 떠있지 않다고 판단합니다.
	public bool IsShowing()
	{
		return _currentDialog != null;
	}
}
