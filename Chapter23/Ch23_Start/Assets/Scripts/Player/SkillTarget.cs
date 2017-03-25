using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SkillTarget : MonoBehaviour {

	public List<Collider> targetList;

	void Awake()
	{
		targetList = new List<Collider>();
	}

	void OnTriggerEnter(Collider other)
	{
		targetList.Add(other);
	}

	void OnTriggerExit(Collider other)
	{
		targetList.Remove(other);
	}
}
