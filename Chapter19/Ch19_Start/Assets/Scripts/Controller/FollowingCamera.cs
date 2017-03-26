using UnityEngine;
using System.Collections;

public class FollowingCamera : MonoBehaviour
{
	public float distanceAway = 7f;			
	public float distanceUp   = 4f;			

	
	public Transform follow;

	
	void LateUpdate ()
	{

		transform.position = follow.position + Vector3.up * distanceUp - Vector3.forward * distanceAway;
	}
}
