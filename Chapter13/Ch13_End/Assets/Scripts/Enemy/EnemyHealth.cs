using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	
	public int startingHealth = 100;
	public int currentHealth;
	
	public float flashSpeed = 5f;
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

	public float sinkSpeed = 1f;

	Animator anim;
	AudioSource playerAudio;
	bool isDead;
	bool isSinking;
	bool damaged;

	void Awake ()
	{
		anim = GetComponent <Animator> ();
		playerAudio = GetComponent <AudioSource> ();
		currentHealth = startingHealth;
	}

	
	public void TakeDamage (int amount)
	{
		damaged = true;
		
		currentHealth -= amount;

		if(currentHealth <= 0 && !isDead)
		{
			Death ();
		}
	}

	public IEnumerator StartDamage(int damage, Vector3 playerPosition, float delay, float pushBack)
	{
		yield return new WaitForSeconds(delay);

		try{

			TakeDamage(damage);
			
			Vector3 diff = playerPosition - transform.position;
			diff = diff / diff.sqrMagnitude;
			GetComponent<Rigidbody>().AddForce(diff*-10000f*pushBack);

		}catch(MissingReferenceException e)
		{
			Debug.Log (e.ToString());
		}
	}
	
	
	void Update ()
	{
		if(damaged)
		{
			transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_OutlineColor", flashColour);
		}
		else
		{
			transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.Lerp (transform.GetChild(0).GetComponent<Renderer>().material.GetColor("_OutlineColor"), Color.black, flashSpeed * Time.deltaTime));
		}
		damaged = false;

		if(isSinking)
		{
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
		}
	}

	void Death ()
	{
		isDead = true;
		
		StageController.Instance.AddPoint(10);

		transform.GetChild(0).GetComponent<BoxCollider>().isTrigger = true;
		
		GetComponent <NavMeshAgent> ().enabled = false;
		
		GetComponent <Rigidbody> ().isKinematic = true;
		
		isSinking = true;
		
		//Destroy (gameObject, 2f);
	}


}
