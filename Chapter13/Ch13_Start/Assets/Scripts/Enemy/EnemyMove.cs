using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {
	
	Transform player;
	UnityEngine.AI.NavMeshAgent nav;

	void Awake () {

		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject p in players) {
			if (p.transform.name == "Player") {
				player = p.transform;
			}
		}
		nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();

	}
	
	void Update () {
		if(nav.enabled){
			nav.SetDestination (player.position);
		}
	}

}
