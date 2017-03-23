using UnityEngine;
using TapjoyUnity;
using System.Collections;

public class PlacementExample : MonoBehaviour {
	public TJPlacement directPlayPlacement;
	public TJPlacement offerwallPlacement;
	public TJPlacement samplePlacement;
	public string samplePlacementName = "video_unit";

	public string output = "";

	public bool shouldPreload = false;
	public bool contentIsReadyForPlacement = false;

	void OnEnable() {
		Debug.Log("C# PlacementExample Enable -- Adding Tapjoy Placement delegates");

		// Placement Delegates
		TJPlacement.OnRequestSuccess += HandlePlacementRequestSuccess;
		TJPlacement.OnRequestFailure += HandlePlacementRequestFailure;
		TJPlacement.OnContentReady += HandlePlacementContentReady;
		TJPlacement.OnContentShow += HandlePlacementContentShow;
		TJPlacement.OnContentDismiss += HandlePlacementContentDismiss;
		TJPlacement.OnPurchaseRequest += HandleOnPurchaseRequest;
		TJPlacement.OnRewardRequest += HandleOnRewardRequest;

		// Tapjoy Placement Video Delegates
		TJPlacement.OnVideoStart += HandleVideoStart;
		TJPlacement.OnVideoError += HandleVideoError;
		TJPlacement.OnVideoComplete += HandleVideoComplete;

		// Currency Delegates
		Tapjoy.OnAwardCurrencyResponse += HandleAwardCurrencyResponse;
		Tapjoy.OnAwardCurrencyResponseFailure += HandleAwardCurrencyResponseFailure;
		Tapjoy.OnSpendCurrencyResponse += HandleSpendCurrencyResponse;
		Tapjoy.OnSpendCurrencyResponseFailure += HandleSpendCurrencyResponseFailure;
		Tapjoy.OnGetCurrencyBalanceResponse += HandleGetCurrencyBalanceResponse;
		Tapjoy.OnGetCurrencyBalanceResponseFailure += HandleGetCurrencyBalanceResponseFailure;
		Tapjoy.OnEarnedCurrency += HandleEarnedCurrency;

		// Preload direct play placement
		if (directPlayPlacement == null) {
			directPlayPlacement = TJPlacement.CreatePlacement("video_unit");
			if (directPlayPlacement != null) {
				directPlayPlacement.RequestContent();
			}
		}

		// Create offerwall placement
		if (offerwallPlacement == null) {
			offerwallPlacement = TJPlacement.CreatePlacement("offerwall_unit");
		}

		InitUI();
	}

	void OnDisable() {
		Debug.Log("C#: Disabling and removing Tapjoy Delegates");

		// Placement delegates
		TJPlacement.OnRequestSuccess -= HandlePlacementRequestSuccess;
		TJPlacement.OnRequestFailure -= HandlePlacementRequestFailure;
		TJPlacement.OnContentReady -= HandlePlacementContentReady;
		TJPlacement.OnContentShow -= HandlePlacementContentShow;
		TJPlacement.OnContentDismiss -= HandlePlacementContentDismiss;
		TJPlacement.OnPurchaseRequest -= HandleOnPurchaseRequest;
		TJPlacement.OnRewardRequest -= HandleOnRewardRequest;

		// Tapjoy Placement Video Delegates
		TJPlacement.OnVideoStart -= HandleVideoStart;
		TJPlacement.OnVideoError -= HandleVideoError;
		TJPlacement.OnVideoComplete -= HandleVideoComplete;

		// Currency Delegates
		Tapjoy.OnAwardCurrencyResponse -= HandleAwardCurrencyResponse;
		Tapjoy.OnAwardCurrencyResponseFailure -= HandleAwardCurrencyResponseFailure;
		Tapjoy.OnSpendCurrencyResponse -= HandleSpendCurrencyResponse;
		Tapjoy.OnSpendCurrencyResponseFailure -= HandleSpendCurrencyResponseFailure;
		Tapjoy.OnGetCurrencyBalanceResponse -= HandleGetCurrencyBalanceResponse;
		Tapjoy.OnGetCurrencyBalanceResponseFailure -= HandleGetCurrencyBalanceResponseFailure;
		Tapjoy.OnEarnedCurrency -= HandleEarnedCurrency;
	}


#region Tapjoy Delegate Handlers

#region Placement Delegate Handlers
	public void HandlePlacementRequestSuccess(TJPlacement placement) {
		if (placement.IsContentAvailable()) {
			Debug.Log("C#: Content available for " + placement.GetName());
			output = "Content available for " + placement.GetName();

			if (placement.GetName() == samplePlacementName && samplePlacement != null) {
				contentIsReadyForPlacement = true;
			}
			else if(placement.GetName() == "offerwall_unit") {
				// Show offerwall immediately
				placement.ShowContent();
			}

		} else {
			output = "No content available for " + placement.GetName();
			Debug.Log("C#: No content available for " + placement.GetName());
		}
	}

	public void HandlePlacementRequestFailure(TJPlacement placement, string error) {
		Debug.Log("C#: HandlePlacementRequestFailure");
		Debug.Log("C#: Request for " + placement.GetName() + " has failed because: " + error);
		output = "Request for " + placement.GetName() + " has failed because: " + error;
	}

	public void HandlePlacementContentReady(TJPlacement placement) {
		Debug.Log("C#: HandlePlacementContentReady");
		output = "HandlePlacementContentReady";
		if (placement.IsContentAvailable()) {
			//placement.ShowContent();
		} else {
			Debug.Log("C#: no content");
		}
	}

	public void HandlePlacementContentShow(TJPlacement placement) {
		Debug.Log("C#: HandlePlacementContentShow");
	}

	public void HandlePlacementContentDismiss(TJPlacement placement) {
		Debug.Log("C#: HandlePlacementContentDismiss");
		contentIsReadyForPlacement = false;
		output = "TJPlacement " + placement.GetName() + " has been dismissed";
	}

	void HandleOnPurchaseRequest (TJPlacement placement, TJActionRequest request, string productId)
	{
		Debug.Log ("C#: HandleOnPurchaseRequest");
		request.Completed();
	}

	void HandleOnRewardRequest (TJPlacement placement, TJActionRequest request, string itemId, int quantity)
	{
		Debug.Log ("C#: HandleOnRewardRequest");
		request.Completed();
	}

#endregion

#region Currency Delegate Handlers
	public void HandleAwardCurrencyResponse(string currencyName, int balance) {
		Debug.Log("C#: HandleAwardCurrencySucceeded: currencyName: " + currencyName + ", balance: " + balance);
		output = "Awarded Currency -- " + currencyName + " Balance: " + balance;
	}
	
	public void HandleAwardCurrencyResponseFailure(string error) {
		Debug.Log("C#: HandleAwardCurrencyResponseFailure: " + error);
	}
	
	public void HandleGetCurrencyBalanceResponse(string currencyName, int balance) {
		Debug.Log("C#: HandleGetCurrencyBalanceResponse: currencyName: " + currencyName + ", balance: " + balance);
		output = currencyName + " Balance: " + balance;
	}
	
	public void HandleGetCurrencyBalanceResponseFailure(string error) {
		Debug.Log("C#: HandleGetCurrencyBalanceResponseFailure: " + error);
	}
	
	public void HandleSpendCurrencyResponse(string currencyName, int balance) {
		Debug.Log("C#: HandleSpendCurrencyResponse: currencyName: " + currencyName + ", balance: " + balance);
		output = currencyName + " Balance: " + balance;
	}
	
	public void HandleSpendCurrencyResponseFailure(string error) {
		Debug.Log("C#: HandleSpendCurrencyResponseFailure: " + error);
	}
	
	public void HandleEarnedCurrency(string currencyName, int amount) {
		Debug.Log("C#: HandleEarnedCurrency: currencyName: " + currencyName + ", amount: " + amount);
		output = currencyName + " Earned: " + amount;

		Tapjoy.ShowDefaultEarnedCurrencyAlert();
	}
#endregion

#region Video Delegate Handlers
	public void HandleVideoStart(TJPlacement placement) {
		Debug.Log("C#: HandleVideoStarted for placement " + placement.GetName());
	}

	public void HandleVideoError(TJPlacement placement, string message) {
		Debug.Log("C#: HandleVideoError for placement " + placement.GetName() + "with message: " + message);
	}

	public void HandleVideoComplete(TJPlacement placement) {
		Debug.Log("C#: HandleVideoComplete for placement " + placement.GetName());
	}
#endregion
#endregion

#region Unity GUI code for Placement Example Component
	GUIStyle inputStyle;
	GUIStyle headerStyle;
	GUIStyle outputStyle;
	int fontSize = 20;

	float startY;
	float centerX;
	float buttonWidth;
	float buttonHeight;
	float headerHeight;
	float halfButtonWidth;
	float thirdButtonWidth;

	float yPadding = 50;

	private void InitUI() {
		headerStyle = new GUIStyle();
		headerStyle.alignment = TextAnchor.MiddleLeft;
		headerStyle.normal.textColor = Color.white;
		headerStyle.wordWrap = true;
		headerStyle.fontSize = fontSize;

		outputStyle = new GUIStyle();
		outputStyle.alignment = TextAnchor.MiddleCenter;
		outputStyle.normal.textColor = Color.white;
		outputStyle.wordWrap = true;
		outputStyle.fontSize = fontSize;

		startY = Screen.height / 10;
		centerX = Screen.width / 2;
		buttonWidth = Screen.width - (Screen.width / 6);
		buttonHeight = Screen.height / 15;
		halfButtonWidth = buttonWidth / 2;
		thirdButtonWidth = buttonWidth / 3;
		headerHeight = Screen.height / 20;
		yPadding = buttonHeight + 10;
	}

	void OnGUI() {
		Rect position;
		float yPosition = startY;

		if (inputStyle == null) {
			inputStyle = GUI.skin.textField;
			inputStyle.fontSize = fontSize;
		}

		position = new Rect(centerX - (buttonWidth / 2), yPosition, buttonWidth, buttonHeight);
		if (GUI.Button(position, "Show Offerwall")) {
			if (offerwallPlacement != null) {
				offerwallPlacement.RequestContent();
			}
		}

		yPosition += yPadding;

		position = new Rect(centerX - (buttonWidth / 2), yPosition, buttonWidth, buttonHeight);
		if (GUI.Button(position, "Show Direct Play Video Ad")) {
			if (directPlayPlacement.IsContentAvailable()) {
				if (directPlayPlacement.IsContentReady()) {
					directPlayPlacement.ShowContent();
				} else {
					output = "Direct play video not ready to show.";
				}
			} else {
				output = "No direct play video to show.";
			}
		}

		yPosition += yPadding;

		// Managed Currency Header
		position = new Rect(centerX - (buttonWidth / 2), yPosition, buttonWidth, headerHeight);
		GUI.Label(position, "Managed Currency:", headerStyle);

		yPosition += yPadding - (yPadding - headerHeight);

		position = new Rect(centerX - (thirdButtonWidth + (thirdButtonWidth / 2)), yPosition, thirdButtonWidth, buttonHeight);
		if (GUI.Button(position, "Get")) {
			ResetCurrencyLabel();
			Tapjoy.GetCurrencyBalance();
		}

		position = new Rect(centerX - (thirdButtonWidth / 2), yPosition, thirdButtonWidth, buttonHeight);
		if (GUI.Button(position, "Spend")) {
			ResetCurrencyLabel();
			Tapjoy.SpendCurrency(10);
		}

		position = new Rect(centerX + (thirdButtonWidth / 2), yPosition, thirdButtonWidth, buttonHeight);

		if (GUI.Button(position, "Award")) {	
			ResetCurrencyLabel();
			Tapjoy.AwardCurrency(10);
		}

		yPosition += yPadding;

		// Managed Currency Header
		position = new Rect(centerX - (buttonWidth / 2), yPosition, buttonWidth, headerHeight);
		GUI.Label(position, "Content Placement:", headerStyle);

		yPosition += yPadding - (yPadding - headerHeight);

		position = new Rect(centerX - (buttonWidth / 2), yPosition, buttonWidth, headerHeight);
		samplePlacementName = GUI.TextField(position, samplePlacementName, 30, inputStyle);

		yPosition += headerHeight + 10;

		position = new Rect(centerX - halfButtonWidth, yPosition, halfButtonWidth, buttonHeight);
		if (GUI.Button(position, "Request")) {
			// Create a new sample event
			samplePlacement = TJPlacement.CreatePlacement(samplePlacementName);
			if (samplePlacement != null) {
				samplePlacement.RequestContent();
				output = "Requesting content for placement: " + samplePlacementName;
			}
		}

		if (!contentIsReadyForPlacement) {
			GUI.enabled = false;
		}
		position = new Rect(centerX, yPosition, halfButtonWidth, buttonHeight);
		if (GUI.Button(position, "Show")) {
			if (samplePlacement != null) {
				samplePlacement.ShowContent ();
			}
			
		}
		if (!contentIsReadyForPlacement) {
			GUI.enabled = true;
		}

		yPosition += yPadding;

		// Purchase Header
		position = new Rect(centerX - (buttonWidth / 2), yPosition, buttonWidth, headerHeight);
		GUI.Label(position, "Purchase:", headerStyle);

		yPosition += yPadding - (yPadding - headerHeight);

		position = new Rect(centerX - (buttonWidth / 2), yPosition, buttonWidth, buttonHeight);
		if (GUI.Button(position, "Purchase")) {
			Tapjoy.TrackPurchase("product1", "USD", 0.99);
			output = "Sent track purchase";
		}
		yPosition += yPadding;

		position = new Rect(centerX - (buttonWidth / 2), yPosition, buttonWidth, buttonHeight);
		if (GUI.Button(position, "Purchase (Campaign)")) {
			Tapjoy.TrackPurchase("product1", "USD", 1.99, "TestCampaignID");
			output = "Sent track purchase 2";
		}
		yPosition += yPadding;

		position = new Rect(centerX - halfButtonWidth, yPosition, halfButtonWidth, buttonHeight);
		if (GUI.Button(position, "Purchase (GooglePlayStore)")) {
			Tapjoy.TrackPurchaseInGooglePlayStore(getDummySkuDetails(), getDummyPurchaseData(), getDummyDataSignature(), "TestCampaignID");
			output = "Sent TrackPurchaseInGooglePlayStore";
		}

		position = new Rect(centerX, yPosition, halfButtonWidth, buttonHeight);
		if (GUI.Button(position, "Purchase (AppleAppStore)")) {
			Tapjoy.TrackPurchaseInAppleAppStore("product1", "USD", 1.99, "transactionId", "TestCampaignID");
			output = "Sent TrackPurchaseInAppleAppStore";
		}

		yPosition += yPadding;
		position = new Rect(centerX - (buttonWidth / 2), yPosition, buttonWidth, buttonHeight);

		if( GUI.Button(position, "Show Tapjoy Support Page") )
		{
			Application.OpenURL(Tapjoy.GetSupportURL());
		}

		yPosition += yPadding;

		// Display status
		GUI.Label(new Rect(centerX - 200, yPosition, 400, 150), output, outputStyle);
	}

	private void ResetCurrencyLabel() {
		output = "Updating Currency...";
	}

	private string getDummySkuDetails() {
		return "{\"title\":\"TITLE\",\"price\":\"$3.33\",\"type\":\"inapp\",\"description\":\"DESC\",\"price_amount_micros\":3330000,\"price_currency_code\":\"USD\",\"productId\":\"3\"}";
	}

	private string getDummyPurchaseData() {
		return null;
	}

	private string getDummyDataSignature() {
		return null;
	}

#endregion
}
