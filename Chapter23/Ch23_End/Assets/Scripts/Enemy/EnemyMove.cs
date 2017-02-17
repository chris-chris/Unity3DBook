using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {
	
	Transform player;
	UnityEngine.AI.NavMeshAgent nav;

	void Awake () {
		
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();

	}
	
	void Update () {
		if(nav.enabled && player.GetComponent<PlayerHealth>().currentHealth > 0){
			nav.SetDestination (player.position);
		}
	}

}
