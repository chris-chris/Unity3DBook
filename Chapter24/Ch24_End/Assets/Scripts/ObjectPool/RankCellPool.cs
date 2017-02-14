using UnityEngine;
using System.Collections;

public class RankCellPool : ObjectPool {
	
// 싱글톤(Singleton) 형식으로 타격 이펙트1을 생성하는 오브젝트 풀을 생성합니다.
	private static RankCellPool _instance;
	public static RankCellPool Instance
	{
		get
		{
			// 이미 이 클래스가 객체로 존재하는 지 Double Check!
			if (!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(RankCellPool)) as RankCellPool;
				if (!_instance)
				{
					GameObject container = new GameObject();
					_instance = container.AddComponent(typeof(RankCellPool)) as RankCellPool;
				}
			}
			
			return _instance;
		}
	}
	
// 유니티 게임 오브젝트가 시작될 때 싱글톤을 초기화하고, 오브젝트를 미리 만들어놓습니다(PreloadPool).
	public void Init()
	{
// 프리팹의 경로입니다. Resources 폴더에서 동적으로 가져오므로
// 실제 경로는  /Assets/Resources/ 하위입니다.
		prefabName = "Prefab/RankCell";
// 초기에 만들 오브젝트 수를 정합니다.
		poolSize = 50;
// 오브젝트 풀의 게임 오브젝트 이름을 설정합니다.
		gameObject.name =  "RankCellPool";
		gameObject.layer = 2;
// 오브젝트를 미리 생성해둡니다.
		PreloadPool();
		Debug.Log ("Preload RankCellPool Completed");
	}
	
	public override void SetUp(GameObject po)
	{
// 오브젝트 풀(_available)에 담겨있을 때는 비활성화 상태이므로
// po.SetActive(true); 로 게임 오브젝트를 활성화시킵니다.
		po.SetActive(true);


	}
}
