using UnityEngine;
using System.Collections;

public class FollowingCamera : MonoBehaviour
{
	public float distanceAway = 7f;			
	public float distanceUp   = 4f;			

	
	public Transform follow;

	
	void LateUpdate ()
	{
		//float random = Random.Range(0f,0.3f);
		//float random_forward = Random.Range(0f,1f);
		transform.position = follow.position + Vector3.up * distanceUp  - Vector3.forward * distanceAway;
	}
}
