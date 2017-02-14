using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class TouchPad : MonoBehaviour {
	
	public RectTransform _touchPad;

	public RectTransform _touchBackground;
	
	private int _touchId = -1;

	private Vector3 _startPos = Vector3.zero;
		
	private float _dragRadius = 0.0f;

	public PlayerMovement _player;

	private bool _buttonPressed = false;
		
	void Start () {

		_startPos = _touchBackground.position;

		_dragRadius = _touchBackground.rect.width / 2.0f;
						
	}
	
	public void ButtonDown()
	{
		_buttonPressed = true;
	}

	public void ButtonUp()
	{
		_buttonPressed = false;

		HandleInput(_startPos);

	}


	void FixedUpdate()
	{
		HandleTouchInput();
		
#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_WEBPLAYER

		HandleInput(Input.mousePosition);
		
#endif
			
	}
	
	void HandleTouchInput ()
	{
		int i = 0;

		if(Input.touchCount > 0)
		{
			foreach(Touch touch in Input.touches)
			{
				i++;

				Vector3 touchPos = new Vector3(touch.position.x, touch.position.y);
				
				if(touch.phase == TouchPhase.Began)
				{
					if(touch.position.x 
					   <= (_touchBackground.anchoredPosition.x + _touchBackground.sizeDelta.x))
					{
						_touchId = i;
					}

				}
				
				if(touch.phase == TouchPhase.Moved 
				   || touch.phase == TouchPhase.Stationary)
				{
					if(_touchId == i)
					{
						HandleInput(touchPos);
					}
				}
				
				if(touch.phase == TouchPhase.Ended)
				{
					if(_touchId == i)
					{
						_touchId = -1;
					}
				}

			}
		}
	}

	void HandleInput (Vector3 input)
	{
		if(_buttonPressed)
		{
			Vector3 diffVector = (input - _startPos);

			if (diffVector.sqrMagnitude >
			    _dragRadius * _dragRadius)
			{
				diffVector.Normalize();

				_touchPad.position = _startPos +
					diffVector * _dragRadius;
			}
			else
			{
				_touchPad.position = input;
			}
		}
		else
		{
			_touchPad.position = _startPos;
		}

		Vector3 diff = _touchPad.position - _startPos;

		Vector2 normDiff 
			= new Vector3(diff.x / _dragRadius, diff.y / _dragRadius);


		if(_player != null)
		{
			_player.OnStickChanged(normDiff);
		}
	}


}
