using UnityEngine;
using System;
using System.Collections;

// 일반 팝업 창과 확인 팝업 창을 관리하는 DialogController***(DialogControllerAlert, DialogControllerConfirm) 의 부모 클래스입니다.
public class DialogController : MonoBehaviour
{
	// 팝업 창의 Transform입니다.
	public Transform window;
	
	// 팝업 창이 보이는지 조회하거나, 보이지 않게 설정하는 변수입니다.
	public bool Visible 
	{
		get
		{
			return window.gameObject.activeSelf;
		}

		private set
		{
			window.gameObject.SetActive(value);
		}
	}

	public virtual void Awake()
	{
	}

	public virtual void Start()
	{

	}
	// 팝업이 화면에 나타날 때 OnEnter() 열거형(IEnumerator) 함수로 애니메이션을 구현할 수 있습니다.
	IEnumerator OnEnter(Action callback)
	{		
		Visible = true;

		if( callback != null ) {
			callback();
		}
		yield break;
	}

	// 팝업이 화면에서 사라질 때 OnEnter() 열거형(IEnumerator) 함수로 애니메이션을 구현할 수 있습니다.
	IEnumerator OnExit(Action callback)
	{
		Visible = false;

		if( callback != null ) {
			callback();
		}
		yield break;
	}

    public virtual void Build(DialogData data)
    {
        
    }

	// 팝업이 화면에 나타날 때 OnEnter() 열거형(IEnumerator) 함수로 애니메이션을 구현할 수 있습니다.
    public void Show(Action callback)
    {
		StartCoroutine ( OnEnter( callback ) );
    }

	// 팝업이 화면에서 사라질 때 OnEnter() 열거형(IEnumerator) 함수로 애니메이션을 구현할 수 있습니다.
    public void Close(Action callback)
    {
		StartCoroutine ( OnExit( callback ) );
    }
}
