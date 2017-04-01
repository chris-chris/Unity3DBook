using System.Collections.Generic;
using UnityEngine;

public class NotificationCenter
{
	// singleton pattern
	private static readonly NotificationCenter instance = new NotificationCenter();
	public static NotificationCenter Instance
	{
		get 
		{
			return instance; 
		}
	}
	//
	
	public delegate void UpdateDelegator();
	public enum Subject
	{
		PlayerData,
	}
	
	Dictionary<Subject, UpdateDelegator> _delegateMap;
	
	private NotificationCenter()
	{
		_delegateMap = new Dictionary<Subject, UpdateDelegator> ();
	}
	public void Add(Subject subject, UpdateDelegator delegator)
	{
		if (_delegateMap.ContainsKey (subject) == false) 
		{
			_delegateMap[subject] = delegate() {};
		}
		
		_delegateMap [subject] += delegator;
	}
	
	public void Delete(Subject subject, UpdateDelegator delegator)
	{
		if (_delegateMap.ContainsKey (subject) == false) 
		{
			return;
		}
		
		_delegateMap [subject] -= delegator;
	}
	public void Notify(Subject subject)
	{
		if (_delegateMap.ContainsKey (subject) == false) 
		{
			return;
		}
		
		foreach(UpdateDelegator delegator in
		        _delegateMap[subject].GetInvocationList())
		{
			try
			{
				delegator();
			}
			catch (System.Exception e)
			{
				Debug.LogException(e);
			}
		}
	}
}