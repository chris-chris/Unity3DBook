using UnityEngine;
using System.Collections;

public class Attack4 : StateMachineBehaviour {

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		// PlayerAttack 스크립트의 인스턴스에 일반 공격으로 하라고 NormalAttack()을 호출합니다.
		PlayerAttack.Instance.NormalAttack();

	}
}
