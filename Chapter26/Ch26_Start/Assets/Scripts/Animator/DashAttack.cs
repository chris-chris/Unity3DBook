using UnityEngine;
using System.Collections;

public class DashAttack : StateMachineBehaviour {

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		// PlayerAttack 스크립트의 인스턴스에 대시 공격 DashAttack()을 호출합니다.
		PlayerAttack.Instance.DashAttack();

	}

}
