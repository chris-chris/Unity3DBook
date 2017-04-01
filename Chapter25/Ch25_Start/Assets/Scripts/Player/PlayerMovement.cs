using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]  
public class PlayerMovement: MonoBehaviour {
	
	protected Animator avatar;
	protected PlayerAttack playerAttack;
	
	float lastAttackTime, lastSkillTime, lastDashTime;
	public bool attacking = false;
	public bool dashing = false;
	public float speedLevel = 1f;
	
	float h, v;

	void Start () 
	{
		avatar = GetComponent<Animator>();
		playerAttack = GetComponent<PlayerAttack>();
	}
    
	public void OnStickChanged(Vector2 stickPos)
	{
		h = stickPos.x;
		v = stickPos.y;
	}

	void Update () 
	{
		if(avatar)
		{
			avatar.SetFloat("Speed", (h*h+v*v) );
		    Rigidbody rigidbody = GetComponent<Rigidbody>();
            if(rigidbody)
            {
				if(h != 0f && v != 0f && (h*h+v*v) > 0.1f){
					transform.rotation = Quaternion.LookRotation(new Vector3(h, 0f, v));
				}
            }
		}		
	}
	
	public void OnAttackDown()
	{
		attacking = true;
		avatar.SetBool("Combo", true);
		avatar.SetTrigger("AttackStart");	
	}
	
	public void OnAttackUp()
	{
		avatar.SetBool("Combo", false);
		attacking = false;
	}
	
	public void OnSkillDown()
	{
		avatar.SetBool("Skill", true);
	}
	
	public void OnSkillUp()
	{
		avatar.SetBool("Skill", false);
	}
	
	public void OnDashDown ()
	{	
		avatar.SetTrigger("Dash");
	}
	
	public void OnDashUp ()
	{
	}

}
