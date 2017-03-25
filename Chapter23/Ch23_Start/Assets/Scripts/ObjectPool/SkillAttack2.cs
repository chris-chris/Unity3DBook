using UnityEngine;
using System.Collections;

public class SkillAttack2 : MonoBehaviour {

	public void Play()
	{
		StartCoroutine(StartDisappearAfter(2f));
	}

	IEnumerator StartDisappearAfter(float time)
	{
		yield return new WaitForSeconds(time);
		SkillAttack2Pool.Instance.ReleaseObject(gameObject);
		
	}

}
