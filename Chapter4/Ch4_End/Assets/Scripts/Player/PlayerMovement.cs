using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]  
public class PlayerMovement: MonoBehaviour {
	
	protected Animator avatar;
	void Start () 
	{
		avatar = GetComponent<Animator>();
	}
    
	float h, v;

	public void OnStickChanged(Vector2 stickPos)
	{
		h = stickPos.x;
		v = stickPos.y;
	}

	void Update () 
	{

		if(avatar)
		{
			avatar.SetFloat("Speed", (h*h+v*v));

		    Rigidbody rigidbody = GetComponent<Rigidbody>();

            if(rigidbody)
            {
				if(h != 0f && v != 0f){
					transform.rotation = Quaternion.LookRotation(new Vector3(h, 0f, v));
				}

            }

		}		
	}



}
