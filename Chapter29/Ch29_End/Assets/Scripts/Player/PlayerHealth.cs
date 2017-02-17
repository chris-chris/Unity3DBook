using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	public int startingHealth;
	public int currentHealth;
	public Slider healthSlider;
	public Image damageImage;
	public AudioClip deathClip;
	public float flashSpeed = 5f;
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
	
	Animator anim;
	AudioSource playerAudio;
	PlayerMovement playerMovement;
	bool isDead;
	bool damaged;
	
	void Awake ()
	{
		anim = GetComponent <Animator> ();
		playerAudio = GetComponent <AudioSource> ();
		playerMovement = GetComponent <PlayerMovement> ();
		startingHealth = UserSingleton.Instance.Health;
		healthSlider.maxValue = startingHealth;
		currentHealth = startingHealth;
		healthSlider.value = currentHealth ;

	}
	
	
	void Update ()
	{
		if(damaged)
		{
			damageImage.color = flashColour;
		}
		else
		{
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}
	

	public void TakeDamage (int amount)
	{
		damaged = true;
		
		currentHealth -= amount;
		
		healthSlider.value = currentHealth ;
		
		if(currentHealth <= 0 && !isDead)
		{
			Death ();
		}else{
			anim.SetTrigger("Damage");
		}
	}
	

	void Death ()
	{
		isDead = true;

		StageController.Instance.FinishGame();
		
		anim.SetTrigger ("Die");
		
		//playerAudio.clip = deathClip;
		//playerAudio.Play ();
		
		playerMovement.enabled = false;
	}

}
