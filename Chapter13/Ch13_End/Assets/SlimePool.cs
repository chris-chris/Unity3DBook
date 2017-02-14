using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlimePool : MonoBehaviour {

	public Transform slimeObject;
	public int poolSize;
	public static SlimePool Instance;
	
	private List<Transform> _available = new List<Transform>();
	private List<Transform> _inUse = new List<Transform>();
	
	void Start()
	{
		Instance = this; // Lazy Singleton
	}

	// 
	public Transform GetObject()
	{
		lock(_available)
		{
			if (_available.Count != 0)
			{
				Transform po = _available[0];
				_inUse.Add(po);
				_available.RemoveAt(0);
				return po;
			}
			else
			{
				Transform po = Instantiate(slimeObject);
				_inUse.Add(po);
				return po;
			}
		}
	}
	
	public void ReleaseObject(Transform po)
	{
		CleanUp(po);
		
		lock (_available)
		{
			_available.Add(po);
			_inUse.Remove(po);
		}
	}
	
	private void CleanUp(Transform po)
	{
		po.parent = transform;
		po.gameObject.SetActive(false);
	}
}
