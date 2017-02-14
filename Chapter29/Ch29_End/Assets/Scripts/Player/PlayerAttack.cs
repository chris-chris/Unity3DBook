using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour {

	// 플레이어가 몬스터에게 주는 데미지 수치입니다. 
	// 레벨/경험치 파츠 업그레이드 챕터에서 캐릭터 성잠시스템을 도입하면 변경될 예정입니다.
	public int NormalDamage = 10;
	public int SkillDamage = 30;
	public int DashDamage = 30;

	// 캐릭터의 공격 반경입니다. 
	// 타겟의 Trigger로 어떤 몬스터가 공격 반경 안에 들어왔는지 판정합니다.
	public NormalTarget normalTarget;
	public SkillTarget skillTarget;

	// 다른 스크립트에서 이 스크립트로 바로 접근하기 위한 통로라고 보시면 됩니다. 
	// PlayerAttack.Instance 이런 식으로 바로 접근 가능합니다.
	public static PlayerAttack Instance;
	// 주인공 캐릭터에 붙어있는 오디오 소스입니다. 
	// 이 오디오 소스로 주인공 캐릭터의 목소리를 재생합니다.
	public AudioSource audioSource;

	// 이 PlayerAttack 스크립트가 붙어있는 게임 오브젝트가 씬에 생성될 때 호출됩니다.
	void Start()
	{
		// Instance에 자기 자신을 할당합니다. 
		// 이로 인해, 외부 스크립트에서 PlayerAttack.Instance 이렇게 접근 가능합니다.
		Instance = this;
		// 현재 이 게임 오브젝트에 붙어있는 오디오 소스 콤포넌트를 변수에 할당합니다.
		audioSource = GetComponent<AudioSource>();

		NormalDamage = UserSingleton.Instance.Damage;
		DashDamage = UserSingleton.Instance.Damage*2;
		SkillDamage = UserSingleton.Instance.Damage*3;
	}

	// 주인공 캐릭터가 일반 공격을 할 때 호출됩니다.
	// 메카님 작동 방식을 업그레이드 하면서 공격이 처리되는 순서가 변경됩니다.
	public void NormalAttack()
	{

		// 일반 공격을 할 때 재생될 수 있는 주인공 캐릭터의 목소리들입니다. 
		// 이 목소리들 중 하나가 랜덤으로 재생됩니다.
		string[] attackSound = {	"VoiceSample/13.attack_B1", 
			"VoiceSample/13.attack_B1", "VoiceSample/14.attack_B2",
			"VoiceSample/15.attack_B3", "VoiceSample/16.attack_C1",
			"VoiceSample/17.attack_C2", "VoiceSample/18.attack_C3"};

		// 목소리 리스트 중에서 하나를 랜덤으로 재생시키는 함수입니다.
		PlayRandomVoice(attackSound);

		// normalTarget에 붙어있는 Trigger Collider에 들어있는 몬스터의 리스트를 조회합니다. 
		List<Collider> targetList 
			= new List<Collider>(normalTarget.targetList);

		// 타겟 리스트 안에 있는 몬스터들을 foreach 문으로 하나하나 다 조회합니다.
		foreach(Collider one in targetList){
			// 타겟의 게임 오브젝트에 EnemyHealth라는 스크립트를 가져옵니다.
			EnemyHealth enemy = one.GetComponent<EnemyHealth>();
			// 만약 EnemyHealth 스크립트가 있다면 몬스터이므로, 몬스터에게 데미지를 줍시다.
			if(enemy != null){
				// 몬스터에게 데미지를 주면서, 데미지를 얼마 줄지, 얼마나 뒤로 밀려날지(pushBack),
				// 타격 이펙트는 뭘 줄지(effectPrefab), 오디오는 뭘 재생할지(audio)를 매개변수로 전달합니다.
				enemy.Damage(NormalDamage,transform.position, 2f,
					 "SkillAttack1","Audio/explosion_enemy");	

			}
			
		}

	}

	public void DashAttack()
	{
		// 대시 공격을 할 때 재생될 수 있는 주인공 캐릭터의 목소리들입니다. 
		// 이 목소리들 중 하나가 랜덤으로 재생됩니다.
		string[] attackSound = {	
			"VoiceSample/10.attack_A1", "VoiceSample/11.attack_A2",
			"VoiceSample/12.attack_A3"};

		// 목소리 리스트 중에서 하나를 랜덤으로 재생시키는 함수입니다.
		PlayRandomVoice(attackSound);

		// 대시 공격을 할 때에는 주인공에게도 이펙트가 발생하도록 합니다.
		PlayEffect("SkillAttack2");

		// normalTarget에 붙어있는 Trigger Collider에 들어있는 몬스터의 리스트를 조회합니다. 
		List<Collider> targetList 
			= new List<Collider>(skillTarget.targetList);

		// 타겟 리스트 안에 있는 몬스터들을 foreach 문으로 하나하나 다 조회합니다.
		foreach(Collider one in targetList){
		
			// 타겟의 게임 오브젝트에 EnemyHealth라는 스크립트를 가져옵니다.
			EnemyHealth enemy = one.GetComponent<EnemyHealth>();
			
			// 만약 EnemyHealth 스크립트가 있다면 몬스터이므로, 몬스터에게 데미지를 줍시다.
			if(enemy != null){
				// 몬스터에게 데미지를 주면서, 데미지를 얼마 줄지, 얼마나 뒤로 밀려날지(pushBack),
				// 타격 이펙트는 뭘 줄지(effectPrefab), 오디오는 뭘 재생할지(audio)를 매개변수로 전달합니다.
				enemy.Damage(DashDamage, transform.position, 4f,
					"SkillAttack2", "Audio/explosion_enemy");
				
			}
		
		}
	}

	public void SkillAttack()
	{	
		// 스킬 공격을 할 때 재생될 수 있는 주인공 캐릭터의 목소리들입니다. 
		// 이 목소리들 중 하나가 랜덤으로 재생됩니다.
		string[] attackSound = {	
			"VoiceSample/44.special_attack_X1", "VoiceSample/45.special_attack_X2",
			"VoiceSample/46.special_attack_X3", "VoiceSample/47.special_attack_X4",
			"VoiceSample/48.special_attack_X5", 
			"VoiceSample/50.special_attack_X7"};

		// 목소리 리스트 중에서 하나를 랜덤으로 재생시키는 함수입니다.
		PlayRandomVoice(attackSound);


		// normalTarget에 붙어있는 Trigger Collider에 들어있는 몬스터의 리스트를 조회합니다. 
		List<Collider> targetList 
			= new List<Collider>(skillTarget.targetList);

		// 타겟 리스트 안에 있는 몬스터들을 foreach 문으로 하나하나 다 조회합니다.
		foreach(Collider one in targetList){
		
			// 타겟의 게임 오브젝트에 EnemyHealth라는 스크립트를 가져옵니다.
			EnemyHealth enemy = one.GetComponent<EnemyHealth>();
			
			// 만약 EnemyHealth 스크립트가 있다면 몬스터이므로, 몬스터에게 데미지를 줍시다.
			if(enemy != null){
				
				// 몬스터에게 데미지를 주면서, 데미지를 얼마 줄지, 얼마나 뒤로 밀려날지(pushBack),
				// 타격 이펙트는 뭘 줄지(effectPrefab), 오디오는 뭘 재생할지(audio)를 매개변수로 전달합니다.
				enemy.Damage(SkillDamage, transform.position, 7f,
					 "SkillAttack1","Audio/explosion_player" );

			}
		
		}
	}

	// 랜덤으로 목소리를 재생하는 함수입니다.
	void PlayRandomVoice(string[] attackSound)
	{
		// 간단하게 string 리스트의 길이 중 0 부터 길이 - 1 사이의 숫자를 아무거나 선택합니다.
		// UnityEngine.Random 라이브러리를 사용합니다.
		int rand = UnityEngine.Random.Range(0,attackSound.Length);

		// 주인공 캐릭터에 붙어있는 오디오 소스를 활용해서 재생합니다. 
		audioSource.PlayOneShot(Resources.Load(attackSound[rand]) as AudioClip);

	}

	// 주인공 캐릭터에게 발생하는 이펙트를 생성합니다.
	void PlayEffect(string prefabName)
	{
		if(prefabName == "SkillAttack1"){
			GameObject effect = SkillAttack1Pool.Instance.GetObject();
			effect.transform.position = transform.position+ new Vector3(0f,0.5f,-0.5f);
			effect.GetComponent<SkillAttack1>().Play();
		}else if(prefabName == "SkillAttack2"){
			GameObject effect = SkillAttack2Pool.Instance.GetObject();
			effect.transform.position = transform.position+ new Vector3(0f,0.5f,-0.5f);
			effect.GetComponent<SkillAttack2>().Play();
		}

	}

}
