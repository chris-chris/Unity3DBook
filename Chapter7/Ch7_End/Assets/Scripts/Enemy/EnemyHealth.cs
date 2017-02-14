using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	
	public int startingHealth = 100;
	public int currentHealth;
	
	public float flashSpeed = 5f;
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

	public float sinkSpeed = 1f;
	

	AudioSource playerAudio;
	bool isDead;
	bool isSinking;
	bool damaged;
	
	
	void Awake ()
	{
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
			GetComponent<Rigidbody>().AddForce((transform.position - new Vector3(diff.x,diff.y,0f))*50f*pushBack);

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
		
		// If the enemy should be sinking...
		if(isSinking)
		{
			// ... move the enemy down by the sinkSpeed per second.
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
		}
	}


	
	void Death ()
	{
		// The enemy is dead.
		isDead = true;
		
		// Turn the collider into a trigger so shots can pass through it.
		transform.GetChild(0).GetComponent<BoxCollider>().isTrigger = true;
		
		// Tell the animator that the enemy is dead.
		//anim.SetTrigger ("Dead");
		
		// Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
		//enemyAudio.clip = deathClip;
		//enemyAudio.Play ();

		StartSinking();
	}
	
	
	public void StartSinking ()
	{
		// Find and disable the Nav Mesh Agent.
		GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
		
		// Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
		GetComponent <Rigidbody> ().isKinematic = true;
		
		// The enemy should no sink.
		isSinking = true;
		
		// Increase the score by the enemy's score value.
		//ScoreManager.score += scoreValue;
		
		// After 2 seconds destory the enemy.
		Destroy (gameObject, 2f);
	}

}
