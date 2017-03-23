using UnityEngine;
using TapjoyUnity;
using System;
using System.Collections;

public class UserExample : MonoBehaviour {

	private const string inputUserIdPlaceholder = "User Id";
	private const string inputUserLevelPlaceholder = "Level";
	private const string inputUserFriendsPlaceholder = "Friend Count";
	private const string inputCohortPlaceholder = "Cohort";
	private const string inputUserTagPlaceholder = "Tag";

	private string inputUserId = inputUserIdPlaceholder;
	private string inputUserLevel = inputUserLevelPlaceholder;
	private string inputUserFriends = inputUserFriendsPlaceholder;
	private string[] cohortValues = new string[5];
	private string userTag = inputUserTagPlaceholder;

	void OnEnable() {
		Debug.Log("C# UserExample Enable -- Adding Tapjoy User ID delegates");

		Tapjoy.OnSetUserIDSuccess += HandleSetUserIDSuccess;
		Tapjoy.OnSetUserIDFailure += HandleSetUserIDFailure;
	}

	void OnDisable() {
		Debug.Log("C#: UserExample -- Disabling and removing Tapjoy User ID Delegates");
		
		// Placement delegates
		Tapjoy.OnSetUserIDSuccess -= HandleSetUserIDSuccess;
		Tapjoy.OnSetUserIDFailure -= HandleSetUserIDFailure;
	}

	private void HandleSetUserIDSuccess() {
		Debug.Log("C#: HandleSetUserIDSuccess");
	}

	private void HandleSetUserIDFailure(string error) {
		Debug.Log("C#: HandleSetUserIDFailure: " + error);
	}
	
	// Use this for initialization
	void Start () {
		Debug.Log("C#: UserExample Start");
		InitUI();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region Unity GUI code for Placement Example Component
	GUIStyle inputStyle;
	GUIStyle labelStyle;
	GUIStyle outputStyle;
	GUIStyle headerStyle;

	int fontSize = 20;

	float startX;
	float startY;
	float centerX;
	float inputWidth;
	float labelWidth;
	float buttonWidth;
	float buttonHeight;
	float buttonPadding;
	
	float yPadding = 50;
	private void InitUI() {
		headerStyle = new GUIStyle();
		headerStyle.alignment = TextAnchor.MiddleLeft;
		headerStyle.normal.textColor = Color.white;
		headerStyle.fontSize = fontSize;

		labelStyle = new GUIStyle();
		labelStyle.alignment = TextAnchor.MiddleLeft;
		labelStyle.normal.textColor = Color.white;
		labelStyle.fontSize = fontSize;
		
		outputStyle = new GUIStyle();
		outputStyle.alignment = TextAnchor.MiddleCenter;
		outputStyle.normal.textColor = Color.white;
		outputStyle.wordWrap = true;
		outputStyle.fontSize = fontSize;
		
		centerX = Screen.width / 2;
		startX = centerX - (Screen.width * 0.4f);
		startY = Screen.height / 10;

		buttonPadding = 5;

		inputWidth = Screen.width * 0.6f;
		buttonWidth = (Screen.width * 0.8f) / 2;
		labelWidth = Screen.width * 0.2f;
		buttonHeight = Screen.height / 25;
		yPadding = buttonHeight + 10;
	}

	void OnGUI() {
		Rect position;
		float yPosition = startY;
		
		if (inputStyle == null) {
			inputStyle = GUI.skin.textField;
			inputStyle.fontSize = fontSize;
		}
		
		// User id
		position = new Rect(startX, yPosition, labelWidth, buttonHeight);
		GUI.Label(position, "User Id:", labelStyle);
		
		position = new Rect(startX + labelWidth, yPosition, inputWidth, buttonHeight);
		inputUserId = GUI.TextField(position, inputUserId, 30, inputStyle);

		yPosition += yPadding;

		// User Level
		position = new Rect(startX, yPosition, inputWidth, buttonHeight);
		GUI.Label(position, "User Level:", labelStyle);
		
		position = new Rect(startX + labelWidth, yPosition, inputWidth, buttonHeight);
		inputUserLevel = GUI.TextField(position, "" + inputUserLevel, 30, inputStyle);
		
		yPosition += yPadding;

		// User Friends
		position = new Rect(startX, yPosition, inputWidth, buttonHeight);
		GUI.Label(position, "User Friends:", labelStyle);
		
		position = new Rect(startX + labelWidth, yPosition, inputWidth, buttonHeight);
		inputUserFriends = GUI.TextField(position, "" + inputUserFriends, 30, inputStyle);
		
		yPosition += yPadding;

		// User Cohorts Header
		position = new Rect(startX, yPosition, inputWidth, buttonHeight);
		GUI.Label(position, "User Cohorts:", headerStyle);
		yPosition += yPadding;

		// User Cohorts
		for (int i = 0; i < 5; i++) {
			position = new Rect(startX, yPosition, inputWidth, buttonHeight);
			GUI.Label(position, "  Cohort " + (i + 1) + ":", labelStyle);
			
			position = new Rect(startX + labelWidth, yPosition, inputWidth, buttonHeight);
			cohortValues[i] = GUI.TextField(position, "" + cohortValues[i], 30, inputStyle);
			
			yPosition += yPadding;
		}

		yPosition += yPadding;
		
		position = new Rect(centerX - buttonWidth - buttonPadding, yPosition, buttonWidth, buttonHeight);
		if (GUI.Button(position, "Set")) {
			// Set user info
			Tapjoy.SetUserID(inputUserId);

			int temp = 0;
			if (int.TryParse(inputUserLevel, out temp)) {
				Tapjoy.SetUserLevel(temp);
			}

			if (int.TryParse(inputUserFriends, out temp)) {
				Tapjoy.SetUserFriendCount(temp);
			}
			
			// Set User cohorts
			for (int i = 0; i < cohortValues.Length; i++) {
				Tapjoy.SetUserCohortVariable(i + 1, cohortValues[i]);
			}
		}
		
		position = new Rect(centerX + buttonPadding, yPosition, buttonWidth, buttonHeight);
		if (GUI.Button(position, "Clear")) {
			inputUserId = inputUserIdPlaceholder;
			inputUserLevel = inputUserLevelPlaceholder;
			inputUserFriends = inputUserFriendsPlaceholder;
			cohortValues = new string[5];
		}

		yPosition += yPadding;

		position = new Rect(startX, yPosition, inputWidth, buttonHeight);
		GUI.Label(position, "User Tag:", labelStyle);

		position = new Rect(startX + labelWidth, yPosition, inputWidth, buttonHeight);
		userTag = GUI.TextField(position, "" + userTag, 30, inputStyle);
		
		yPosition += yPadding;

		position = new Rect(centerX - buttonWidth - buttonPadding, yPosition, buttonWidth * 2 / 3, buttonHeight);
		if (GUI.Button(position, "Add")) {
			Tapjoy.AddUserTag(userTag);
			userTag = "";
		}
		position = new Rect(centerX - buttonWidth / 3, yPosition, buttonWidth * 2 / 3, buttonHeight);
		if (GUI.Button(position, "Remove")) {
			Tapjoy.RemoveUserTag(userTag);
			userTag = "";
		}
		position = new Rect(centerX + buttonWidth / 3 + buttonPadding, yPosition, buttonWidth * 2 / 3, buttonHeight);
		if (GUI.Button(position, "Clear")) {
			Tapjoy.ClearUserTags();
			userTag = "";
		}

		yPosition += yPadding;
	}
	#endregion
}
