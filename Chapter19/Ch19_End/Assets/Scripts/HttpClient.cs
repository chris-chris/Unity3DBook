using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;

// HTTPClient 클래스는 서버와 통신을 하는 기능을 수행합니다.
public class HTTPClient : MonoBehaviour {
	
	//  _container라는 이름의 게임오브젝트 변수입니다.
	static GameObject _container;
	static GameObject Container {
		get {
			return _container;
		}
	}
	
	// 싱글톤 객체를 만들기 위해 _instance를 선언하는 부분입니다.
	static HTTPClient _instance;
	public static HTTPClient Instance {
		get {
			// 만약 인스턴스가 없으면
			if( ! _instance ) {
				// 새로운 게임오브젝트를 생성하여 _container에 할당합니다.
				_container = new GameObject();
				// 그리고 게임오브젝트의 이름을 HTTPClient라고 명명합니다.
				_container.name = "HTTPClient";
				// 생성한 게임오브젝트에 HTTPClient 스크립트를 콤포넌트로 추가합니다.
				_instance = _container.AddComponent( typeof(HTTPClient) ) as HTTPClient;
			}
			// 인스턴스(_instance)를 반환합니다.
			return _instance;
		}
	}

	// GET 함수로 GET 형식의 HTTP 통신을 수행할 수 있습니다.
	public void GET(string url, Action<WWW> callback) {
		
		// 새로운 WWW 클래스를 생성합니다.
		WWW www = new WWW(url);
		// StartCoroutine으로 IEnumerator 형식의 WaitWWW함수를 실행합니다.
		// WaitWWW 함수는 WWW 요청이 완료될 때 callback 함수로 결과를 전달하도록 합니다.
		StartCoroutine(WaitWWW(www, callback));

	}
	
	// POST 함수로 POST 형식의 HTTP 통신을 수행할 수 있습니다.
	public void POST(string url, string input, Action<WWW> callback) {
		
		// 새로운 Dictionary 변수를 생성합니다. 
		// headers라는 변수는 <string, string>을 매핑하는 딕셔너리 변수입니다.
		Dictionary<string, string> headers = new Dictionary<string, string>();
		headers.Add("Content-Type", "application/json");
		// POST 요청의 본문은 input 문자열로 지정합니다.
		byte[] body = Encoding.UTF8.GetBytes(input);
		// WWW 객체를 새로 생성합니다. body(본문)과 headers(헤더)를 설정합니다.
		WWW www = new WWW(url, body, headers);

		// StartCoroutine으로 IEnumerator 형식의 WaitWWW함수를 실행합니다.
		// WaitWWW 함수는 WWW 요청이 완료될 때 callback 함수로 결과를 전달하도록 합니다.
		StartCoroutine(WaitWWW(www, callback));

	}

	public IEnumerator WaitWWW(WWW www, Action<WWW> callback)
	{
		yield return www;
		callback(www);
	}

}
