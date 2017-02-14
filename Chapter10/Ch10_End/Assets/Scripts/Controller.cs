using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public Transform PopupBackground;

	public void CoroutineTest1()
	{
		StartCoroutine(Coroutine1());
		StartCoroutine(Coroutine2());
	}

	public void SubroutineTest()
	{
		Subroutine1();
		Subroutine2();
	}

	public IEnumerator Coroutine1()
	{
		for(int i = 0 ; i < 100 ; i++)
		{
			Debug.Log ("Coroutine1 : " + i);
			yield return new WaitForFixedUpdate();
		}
	}

	public IEnumerator Coroutine2()
	{
		for(int i = 0 ; i < 100 ; i++)
		{
			Debug.Log ("Coroutine2 : " + i);
			yield return new WaitForFixedUpdate();
		}
	}

	public void Subroutine1()
	{
		for(int i = 0 ; i < 100 ; i++)
		{
			Debug.Log ("Subroutine1 : " + i);
		}
	}

	public void Subroutine2()
	{
		for(int i = 0 ; i < 100 ; i++)
		{
			Debug.Log ("Subroutine2 : " + i);
		}
	}

	public void ShowPopup()
	{
		StartCoroutine(StartShowPopup());
	}

	public IEnumerator StartShowPopup()
	{
		float timeStart = Time.time;
		while(true)
		{
			float timePassed = Time.time - timeStart;
			float rate = timePassed / 0.5f;

			PopupBackground.localPosition = new Vector3(0f,300f - 300f*rate,0f);

			if(timePassed>0.5f){

				PopupBackground.localPosition = new Vector3(0f,0f,0f);

				break;
			}

			yield return new WaitForFixedUpdate();

		}
	}


}
