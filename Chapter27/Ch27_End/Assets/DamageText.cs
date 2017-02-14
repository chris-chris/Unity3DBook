using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageText : MonoBehaviour {

// 데미지 수치가 표시되는 텍스트 UI입니다. 
	public Text text;

// Start() 함수에서 시작하지 않고 Public Play 함수로 재싱을 시킵니다.
// 이는 오브젝트 풀로 활용하기 위해서입니다. 
	public void Play(int damage)
	{
// 플레이 시작하면 자식 오브젝트 중 첫번째 오브젝트에 붙어있는 Text 콤포넌트를 가져옵니다.
		text = transform.GetChild(0).GetComponent<Text>();
// 해당 Text 콤포넌트에 데미지 텍스트를 표시합니다.
		text.text = damage.ToString();
// 이 데미지 텍스트를 위쪽 방향으로 3초가 움직입니다. 이 애니메이션은 iTween을 활용하였습니다.
		iTween.MoveBy(gameObject,new Vector3(0f,2f,0f),3f);
// 하지만, 유니티 엔진의 Text에 투명도를 조절하는 애니메이션은 iTween에서 지원하지 않으므로 직접 구현해보겠습니다.
		StartCoroutine(StartEffect());
	}

// 3초간 텍스트의 투명도를 조절하는 코루틴 함수입니다.
	IEnumerator StartEffect()
	{
// 애니메이션의 시작 시간을 저장해놓습니다.
		float startTime = Time.time;
// 우선 무한루프를 돌린 후에
		while(true){
// 매 프레임마다 잠시 쉬면서 기능을 수행합니다.
			yield return new WaitForFixedUpdate();
// 애니메이션 시작 시간으로부터 지나간 시간을 구해봅니다.
			float timePassed = Time.time - startTime;
// 3초를 기준으로 몇 프로의 시간이 지났는 지 구합니다.
			float rate = timePassed/3f;

// Text 콤포넌트의 투명도를 1부터 0까지 3초간 rate 비율대로 줄여나갑니다.
			text.color = new Color(1f,0f,0f,1f - rate);
			if(timePassed>3f)
			{
// 3초가 지나면 DamageTextPool에 이 오브젝트를 반환합니다.
				DamageTextPool.Instance.ReleaseObject(gameObject);
				break;
			}
		}
	}
}
