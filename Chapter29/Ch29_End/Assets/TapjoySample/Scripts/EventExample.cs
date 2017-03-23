using UnityEngine;
using TapjoyUnity;
using System.Collections;

public class EventExample : MonoBehaviour {

	public string output = "";

	private const string DEFAULT_CATEGORY = "SDKTestCategory";
	private const string DEFAULT_EVENT_NAME = "SDKTestEvent";
	private const string DEFAULT_KEY = "TestKey1";
	private const int DEFAULT_VALUE = 100;
	private const string DEFAULT_KEY2 = "TestKey2";
	private const long DEFAULT_VALUE2 = 200;
	private const string DEFAULT_KEY3 = "TestKey3";
	private const long DEFAULT_VALUE3 = 300;
	private const string DEFAULT_PARAM1 = "Param1";
	private const string DEFAULT_PARAM2 = "Param2";

	// Use this for initialization
	void Start () {
		Debug.Log("C#: EventExample Start");
		InitUI();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region Unity GUI code for Placement Example Component
	GUIStyle outputStyle;
	int fontSize = 20;
	
	float startY;
	float centerX;
	float buttonWidth;
	float buttonHeight;
	float buttonPadding;
	float headerHeight;
	
	float yPadding = 50;

	private void InitUI() {
		outputStyle = new GUIStyle();
		outputStyle.alignment = TextAnchor.MiddleCenter;
		outputStyle.normal.textColor = Color.white;
		outputStyle.wordWrap = true;
		outputStyle.fontSize = fontSize;

		startY = Screen.height / 10;
		centerX = Screen.width / 2;
		buttonWidth = Screen.width / 3;
		buttonHeight = Screen.height / 15;
		buttonPadding = 5;
		yPadding = buttonHeight + 10;
	}

	void OnGUI() {
		Rect position;
		float yPosition = startY;
		
		position = new Rect(centerX - buttonWidth - buttonPadding, yPosition, buttonWidth, buttonHeight);
		if (GUI.Button(position, "Basic")) {
			Tapjoy.TrackEvent(DEFAULT_EVENT_NAME, DEFAULT_VALUE);

			output = "Sent track event with name: " + DEFAULT_EVENT_NAME + ", " + DEFAULT_VALUE;
		}
		
		position = new Rect(centerX + buttonPadding, yPosition, buttonWidth, buttonHeight);
		if (GUI.Button(position, "Value")) {
			Tapjoy.TrackEvent(DEFAULT_CATEGORY, DEFAULT_EVENT_NAME, DEFAULT_VALUE);
		}

		yPosition += yPadding;

		position = new Rect(centerX - buttonWidth - buttonPadding, yPosition, buttonWidth, buttonHeight);
		if (GUI.Button(position, "Param1")) {
			Tapjoy.TrackEvent(DEFAULT_CATEGORY, DEFAULT_EVENT_NAME, DEFAULT_PARAM1);
		}

		position = new Rect(centerX + buttonPadding, yPosition, buttonWidth, buttonHeight);
		if (GUI.Button(position, "Param 1 and 2")) {
			Tapjoy.TrackEvent(DEFAULT_CATEGORY, DEFAULT_EVENT_NAME, DEFAULT_PARAM1, DEFAULT_PARAM2);
		}

		yPosition += yPadding;

		position = new Rect(centerX - buttonWidth - buttonPadding, yPosition, buttonWidth, buttonHeight);
		if (GUI.Button(position, "Param 1 with value 1")) {
			Tapjoy.TrackEvent(DEFAULT_CATEGORY, DEFAULT_EVENT_NAME, DEFAULT_PARAM1, null, DEFAULT_VALUE);
		}
		
		position = new Rect(centerX + buttonPadding, yPosition, buttonWidth, buttonHeight);
		if (GUI.Button(position, "Param 1 and 2 with value 1")) {
			Tapjoy.TrackEvent(DEFAULT_CATEGORY, DEFAULT_EVENT_NAME, DEFAULT_PARAM1, DEFAULT_PARAM2, DEFAULT_VALUE);
		}

		yPosition += yPadding;

		position = new Rect(centerX - buttonWidth - buttonPadding, yPosition, buttonWidth, buttonHeight);
		if (GUI.Button(position, "Param 1 and 2 with value 1 and 2")) {
			Tapjoy.TrackEvent(DEFAULT_CATEGORY, DEFAULT_EVENT_NAME, DEFAULT_PARAM1, DEFAULT_PARAM2, 
			                  DEFAULT_KEY, DEFAULT_VALUE, DEFAULT_KEY2, DEFAULT_VALUE2);
		}
		
		position = new Rect(centerX + buttonPadding, yPosition, buttonWidth, buttonHeight);
		if (GUI.Button(position, "All")) {
			Tapjoy.TrackEvent(DEFAULT_CATEGORY, DEFAULT_EVENT_NAME, DEFAULT_PARAM1, DEFAULT_PARAM2, 
			                  DEFAULT_KEY, DEFAULT_VALUE, DEFAULT_KEY2, DEFAULT_VALUE2, DEFAULT_KEY3, DEFAULT_VALUE3);
		}
		
		yPosition += yPadding;

		// Display status
		GUI.Label(new Rect(centerX - 200, yPosition, 400, 150), output, outputStyle);
	}
	#endregion
}
