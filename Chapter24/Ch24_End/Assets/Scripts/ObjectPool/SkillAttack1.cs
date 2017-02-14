using UnityEngine;
using System.Collections;

public class SkillAttack1 : MonoBehaviour {
	
	public void Play()
	{
		StartCoroutine(StartDisappearAfter(2f));
	}

	IEnumerator StartDisappearAfter(float time)
	{
		yield return new WaitForSeconds(time);
		SkillAttack1Pool.Instance.ReleaseObject(gameObject);
	}

}
