using UnityEngine;
using System.Collections;

public class SlimePool : ObjectPool {
	
	// 싱글톤(Singleton) 형식으로 슬라임을 생성하는 슬라임 풀을 생성합니다.
	private static SlimePool _instance;
    public static SlimePool Instance
    {
            get
            {
					// Double Check!
                    if (!_instance)
                    {
                            _instance = GameObject.FindObjectOfType(typeof(SlimePool)) as SlimePool;
                            if (!_instance)
                            {
                                    GameObject container = new GameObject();
                                    _instance = container.AddComponent(typeof(SlimePool)) as SlimePool;
                            }
                    }

                    return _instance;
            }
    }

    // 유니티 게임 오브젝트가 시작될 때 싱글톤을 초기화하고, 슬라임을 미리 만들어놓습니다(PreloadPool).
    public void Init()
    {
		prefabName = "Prefab/Slime";
        poolSize = 20;
        gameObject.name =  "SlimePool";
        // 몬스터를 미리 생성해둡니다.
        PreloadPool();
    }

    public override void SetUp(GameObject po)
	{
		// 오브젝트 풀(_available)에 담겨있을 때는 비활성화 상태이므로
		// po.gameObject.SetActive(true); 로 게임 오브젝트를 활성화시킵니다.
		po.SetActive(true);
		// 슬라임 오브젝트의 EnemyHealth 클래스를 불러와서 체력을 초기화하겠습니다.
		EnemyHealth health = po.GetComponent<EnemyHealth>();
		health.Init();
	}
}
