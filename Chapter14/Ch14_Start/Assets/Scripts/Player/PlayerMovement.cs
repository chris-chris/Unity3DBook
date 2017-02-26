using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]  
public class PlayerMovement: MonoBehaviour {
	
	// 애니메이터 콘트롤러에 매개변수를 전달할수 있도록 Animator 변수를 선언합니다.
	protected Animator avatar;
	protected PlayerAttack playerAttack;
	
	// 마지막으로 공격, 스킬, 대시를 한 시점을 저장해둡니다.
	float lastAttackTime, lastSkillTime, lastDashTime;
	
	// 현재 주인공이 공격 중인지, 아니면 대시 공격을 하고 있는지 저장합니다.
	public bool attacking = false;
	public bool dashing = false;

	void Start () 
	{
	// 플레이어 게임 오브젝트에 붙어있는 Animator 클래스와 PlayerAttack 클래스를 변수에 할당받습니다.
		avatar = GetComponent<Animator>();
		playerAttack = GetComponent<PlayerAttack>();
	}
	
    // 터치패드에서 위/아래 방향값을 받아 v에 저장합니다.
	// 터치패드에서 좌/우 방향값을 받아 h에 저장합니다.
	float h, v;
	// 터치패드의 방향이 변경되면 OnStickChanged 함수가 호출됩니다.
	public void OnStickChanged(Vector2 stickPos)
	{
		h = stickPos.x;
		v = stickPos.y;
	}

	// 공격 버튼이 눌렸을 때 호출되는 함수입니다.
	public void OnAttackDown()
	{
		attacking = true;
		avatar.SetBool("Combo", true);
		StartCoroutine(StartAttack());
		
	}
	
	// 유저가 공격 버튼에서 마우스나 손가락을 땟을 때 호출되는 함수입니다.
	public void OnAttackUp()
	{
		avatar.SetBool("Combo", false);
		attacking = false;
	}
	
	// 일반 공격을 구현한 비동기 함수입니다. 
	// 공격버튼을 누른지 1초마다 적들에게 데미지를 입힙니다.
	IEnumerator StartAttack()
	{
		if(Time.time - lastAttackTime> 1f){
			lastAttackTime = Time.time;
			while(attacking){
				avatar.SetTrigger("AttackStart");
				playerAttack.NormalAttack();
				yield return new WaitForSeconds(1f);
			}
		}
		
	}
	
	// 유저가 스킬 공격 버튼을 눌렀을 때 호출되는 함수입니다.
	public void OnSkillDown()
	{
		
		if(Time.time - lastSkillTime > 1f)
		{
			avatar.SetBool("Skill", true);
			lastSkillTime = Time.time;
			playerAttack.SkillAttack();
		}
		
	}
	
	// 유저가 스킬 공격 버튼에서 마우스나 손가락을 떼엇을 때 호출되는 함수입니다.
	public void OnSkillUp()
	{

		avatar.SetBool("Skill", false);

	}
	
	// 유저가 대시 공격 버튼을 눌렀을 때 호출되는 함수입니다.
	public void OnDashDown ()
	{
		
		if(Time.time - lastDashTime > 1f){
			
			lastDashTime = Time.time;
			avatar.SetTrigger("Dash");
			playerAttack.DashAttack();
			
		}
		
	}
	
	// 주인공 캐릭터가 터치패드의 방향에 따라 움직이게 만드는 명령입니다.
	// Update() 함수는 매 프레임마다 호출되어 실시간으로 움직임을 반영합니다.
	void Update () 
	{
		
		if(avatar)
		{
			
			avatar.SetFloat("Speed", (h*h+v*v));

			if(h != 0f && v != 0f){

				transform.rotation = Quaternion.LookRotation(new Vector3(h, 0f, v));

			}

		}

	}

}
