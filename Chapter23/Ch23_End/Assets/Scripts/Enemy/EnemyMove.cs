using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {
	
	Transform player;
	NavMeshAgent nav;

	void Awake () {
		
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		nav = GetComponent <NavMeshAgent> ();

	}
	
	void Update () {
		if(nav.enabled && player.GetComponent<PlayerHealth>().currentHealth > 0){
			nav.SetDestination (player.position);
		}
	}

}
