using UnityEngine;
using System.Collections;

public class Effect_Destroy : MonoBehaviour 
{
	public float destroyTime = 1.5f;



	void Start()
	{
		Destroy (gameObject, destroyTime);
	}

}
