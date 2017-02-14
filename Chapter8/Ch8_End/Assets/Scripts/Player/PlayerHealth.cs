using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	// 주인공의 시작 체력입니다. 기본 100으로 설정되있습니다.
	public int startingHealth = 100;
	// 주인공의 현재 체력입니다. 
	public int currentHealth;
	// 체력게이지 UI와 연결된 변수입니다.
	public Slider healthSlider;
	// 주인공이 데미지를 입을 때 화면을 빨갛게 만들기 위한 투명한 이미지입니다.
	public Image damageImage;
	// 주인공이 데미지를 입었을 때 재생할 오디오입니다.
	public AudioClip deathClip;
	// 화면이 빯갛게 변하고나서 다시 투명한 상태로 돌아가는 속도입니다.
	public float flashSpeed = 5f;
	// 주인공이 데미지를 입었을 때 화면이 변하게되는 색상입니다. 
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
	
	// 애니메이터 콘트롤러에 매개변수를 전달하기 위해 연결한 Animator 콤포넌트
	Animator anim;
	// 플레이어 게임오브젝트에 붙어있는 오디오 소스(Audio Source) 콤포넌트
	// 효과음을 재생할 때 필요합니다.
	AudioSource playerAudio;
	// 플레이어의 움직임을 관리하는 PlayerMovement 스크립트 콤포넌트
	PlayerMovement playerMovement;
    // 플레이어가 죽었는지 저장하는 플래그
	bool isDead;
	// 플레이어가 데미지를 입었는지 저장하는 플래그
	bool damaged;
	
	// 오브젝트가 시작하면 호출되는 Awake() 함수입니다.
	void Awake ()
	{
		// Player 게임 오브젝트에 붙어있는 Animator 콤포넌트를 찾아서 변수에 넣습니다.
		anim = GetComponent <Animator> ();
		// Player 게임 오브젝트에 붙어있는 AudioSource 콤포넌트를 찾아서 변수에 넣습니다.
		playerAudio = GetComponent <AudioSource> ();
		// Player 게임 오브젝트에 붙어있는 PlayerMovement 콤포넌트를 찾아서 변수에 넣습니다.
		playerMovement = GetComponent <PlayerMovement> ();
		// 현재 체력을 최대 체력으로 설정합니다.
		currentHealth = startingHealth;
	}
	
	// 매 프레임마다 호출되는 Update() 함수입니다. 
	void Update ()
	{
	// 이 코드는 몬스터에게 공격 받았을 때 화면을 빨갛게 하는 역할을 합니다.
		if(damaged)
		{
			// 공격 받자마자 damageImage의 색상을 빨간색(flashColour)로 변경합니다.
			damageImage.color = flashColour;
		}
		else
		{
			// 공격 받고난 후에는 서서히 투명한 색(Color.clear)로 변경합니다.
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		// damaged 플래그로 damaged가 true일 때 화면을 빨갛게 만드는 명령을 딱 한번만 수행하게 할 수 있습니다.
		damaged = false;
	}
	
	// 플레이어가 공격받았을 때 호출되는 함수입니다.
	public void TakeDamage (int amount)
	{
		// 공격을 받으면 damaged 변수를 true로 변경합니다.
		damaged = true;
		
		// 공격을 받으면 amount만큼 체력을 감소시킵니다.
		currentHealth -= amount;
		
		// 체력게이지에 변경된 체력값을 표시합니다.
		healthSlider.value = currentHealth ;
		
		// 만약 현재 체력이 0이하가 된다면 죽었다는 함수를 호출합니다.
		if(currentHealth <= 0 && !isDead)
		{
			// 플레이어가 죽었을 때 수행할 명령이 정의된 Death() 함수를 호출합니다.
			Death ();
		}else{
			// 죽은 게 아니라면, 데미지를 입었다는 Trigger를 발동시킵니다.
			anim.SetTrigger("Damage");
		}
	}
	
	
	void Death ()
	{
		// 캐릭터가 죽었다면 isDead 플래그를 true로 설정합니다.
		isDead = true;
		// 애니메이션에서 Die라는 트리거를 발동시킵니다.
		anim.SetTrigger ("Die");
		// 플레이어의 움직임을 관리하는 PlayerMovement 스크립트가 비활성화되게 만듭니다. 
		playerMovement.enabled = false;
	}

}
