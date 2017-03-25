using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	// 적군이 시작하는 체력 기본값은 100으로 설정하지만, 인스펙터 창에서 변경 가능합니다.
	public int startingHealth = 100;
	// 적군의 현재 체력
	public int currentHealth;

	// 적군이 타격받을 때 캐릭터의 테두리를 빨간색으로 잠시 바꾸는 데 사라지는 속도를 결정합니다.
	public float flashSpeed = 5f;
	// 적군이 타격받을 때 캐릭터의 테두리가 변하는 색상입니다. 기본 빨간색입니다.
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

	// 적군이 죽으면 땅바닥으로 가라앉는 데 가라앉는 속도를 정해주는 변수입니다.
	public float sinkSpeed = 1f;

	// 몬스터가 죽었는지 체크하는 변수입니다.
	bool isDead;
	// 몬스터가 죽어서 가라앉고 있는지 체크하는 변수입니다.
	bool isSinking;
	// 몬스터가 지금 데미지를 입었는지 체크하는 변수입니다. 데미지를 입었을 때 테두리를 빨갛게 하기 위해 필요합니다.
	bool damaged;

	// 몬스터가 처음 생성될 때 호출되는 Start() 함수입니다.
	void Start ()
	{
		// 몬스터가 죽고나서 다시 쓰일 때를 위해서 초기화는 Init()함수에서 합니다.
		Init();
	}

	// 오브젝트 풀 활용 : 몬스터가 죽고나서 다시 쓰일 때를 위해서 초기화는 Init()함수에서 합니다.
	public void Init()
	{
		// 몬스터가 시작할 때 체력은 만땅으로 초기화합니다.
		currentHealth = startingHealth;

		// 죽지 않앗고, 데미지 안받았고, 가라앉고 있지 않다고 플래그를 설정합니다.
		isDead = false;
		damaged = false;
		isSinking = false;

		// 몬스터의 Collider를 Trigger가 아니도록 변경시킵니다. 
		// Trigger가 true면 지면이나 플레이어와 충돌하지 않습니다.
		BoxCollider collider = transform.GetComponentInChildren<BoxCollider>();
		collider.isTrigger = false;

		// 더이상 플레이어를 찾아 길찾기를 하지 않도록 NavMeshAgent를 비활성화시킵니다. 
		GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = true;
	}

	// 데미지를 받았을 때 처리하는 함수입니다.
	public IEnumerator StartDamage(int damage, Vector3 playerPosition, float pushBack, float delay)
	{
		yield return new WaitForSeconds (delay);

		// 공격은 죽지 않았을때만 받습니다.
		if(!isDead){
			// 가끔 MissingReferenceException 예외가 발생하는데 발생해도 스킵하도록 예외처리합니다.
			try{

				// 데미지1: 데미지를 몬스터에 체력에 반영합니다.
				TakeDamage(damage);

				// 데미지2: 몬스터를 뒤로 밀려나게 합니다. 뭔가 타격 받을 때 액션성을 더해줍니다.
				PushBack(playerPosition, pushBack);


			}catch(MissingReferenceException e)
			{
				// 이 예외는 발생해도 그냥 무시하겠습니다.
				Debug.Log (e.ToString());
			}
		}
	}

	// 몬스터가 데미지를 입었을 때 처리하는 함수입니다.
	public void TakeDamage (int amount)
	{
		// 테두리에 타격 효과를 빨간색으로 주기 위해 플래그를 True로 변경합니다.
		damaged = true;

		// 현재 체력을 데미지 만큼 차감시킵니다.
		currentHealth -= amount;

		// 현재 체력이 0보다 작거나 같으면 이 몬스터는 죽습니다.
		if(currentHealth <= 0 && !isDead)
		{
			Death ();
		}
	}

	// 뒤로 밀려나게 만드는 함수입니다. 매개변수로 주인공의 위치와 밀려나는 정도를 매개변수로 전달합니다.
	void PushBack(Vector3 playerPosition, float pushBack)
	{
		// 주인공 캐릭터의 위치와 몬스터의 위치의 차이 벡터를 구합니다.
		Vector3 diff = playerPosition - transform.position;
		// 주인공과 몬스터 사이의 차이를 정규화시킵니다. (거리가 1로 만드는 것을 정규화라고 함)
		diff = diff / diff.sqrMagnitude;
		// 현재 몬스터의 RigidBody에 힘을 가합니다. 
		// 플레이어 반대방향으로 밀려나는데, pushBack만큼 비례해서 더 밀려납니다.
		GetComponent<Rigidbody>().AddForce(diff*-10000f*pushBack);
	}

	// 매 프레임마다 실행되는 Update()문입니다.
	void Update ()
	{
		// 데미지를 입었을 때, 몬스터의 테두리를 빨갛게 만듭니다.
		if(damaged)
		{
			transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_OutlineColor", flashColour);
		}
		else
		{
			transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.Lerp (transform.GetChild(0).GetComponent<Renderer>().material.GetColor("_OutlineColor"), Color.black, flashSpeed * Time.deltaTime));
		}
		damaged = false;

		// 몬스터가 죽어서 가라앉고 있으면
		if(isSinking)
		{
			// 몬스터의 몸체를 아래로 움직이도록 합니다.
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
		}
	}

	// 몬스터가 체력이 0이하가 되어 죽었을 때 호출되는 함수
	void Death ()
	{
		// 죽었다고 체크합니다.
		isDead = true;

		// StageController에 현재 스테이지 포인트를 증가시킵니다.
		StageController.Instance.AddPoint(10);

		// 몬스터의 Collider를 Trigger가 true가 되도록 변경시킵니다. 
		// Trigger가 true면 지면이나 플레이어와 충돌하지 않습니다.
		BoxCollider collider = transform.GetComponentInChildren<BoxCollider>();
		collider.isTrigger = true;

		// 더이상 플레이어를 찾아 길찾기를 하지 않도록 NavMeshAgent를 비활성화시킵니다. 
		GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;

		// 가라앉도록 플래그를 활성화 합니다.
		isSinking = true;

	}



}
