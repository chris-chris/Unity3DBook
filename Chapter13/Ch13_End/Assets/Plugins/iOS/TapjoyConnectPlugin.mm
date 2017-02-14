#import "TapjoyConnectPlugin.h"
#import "TapjoyPlacementPlugin.h"

UIViewController *UnityGetGLViewController();

static TapjoyConnectPlugin *_sharedInstance = nil; //To make TapjoyConnect Singleton

@implementation TapjoyConnectPlugin
  

NSString *const UNITY_GAME_OBJECT_NAME = @"TapjoyUnity";
NSString *const CONNECT_FLAG_KEY = @"connectFlags";

+ (void)initialize
{
	if (self == [TapjoyConnectPlugin class])
	{
		_sharedInstance = [[self alloc] init];
	}
}

+ (TapjoyConnectPlugin*)sharedTapjoyConnectPlugin
{
	return _sharedInstance;
}

- (id)init
{
	self = [super init];
    
    if (self)
    {
        tapPoints = 0;
        cSharpDictionaryRefs_ = [[NSMutableDictionary alloc] init];
        actionRequestDict_ = [[NSMutableDictionary alloc] init];

        [Tapjoy setViewDelegate:self];
        [Tapjoy setVideoAdDelegate:self];
        
        // Connect Notifications
        [[NSNotificationCenter defaultCenter] addObserver:self
                                                 selector:@selector(onConnectSuccess:)
                                                     name:TJC_CONNECT_SUCCESS
                                                   object:nil];
        [[NSNotificationCenter defaultCenter] addObserver:self 
        										 selector:@selector(onConnectFailure:)
        										     name:TJC_CONNECT_FAILED
        										   object:nil];

        // Offers Notifications
        [[NSNotificationCenter defaultCenter] addObserver:self
                                                 selector:@selector(onOffersResponse:)
                                                     name:TJC_OFFERS_RESPONSE_NOTIFICATION
                                                   object:nil];
        [[NSNotificationCenter defaultCenter] addObserver:self 
        										 selector:@selector(onOffersResponseFailure:)
        										     name:TJC_OFFERS_RESPONSE_NOTIFICATION_ERROR
        										   object:nil];
        
        // Currency
        [[NSNotificationCenter defaultCenter] addObserver:self
                                                 selector:@selector(earnedCurrency:) 
                                                     name:TJC_CURRENCY_EARNED_NOTIFICATION
                                                   object:nil];
    }
        
	return self;
}

#pragma mark Tapjoy Connect Notifications
- (void)onConnectSuccess:(NSNotification*)notifyObj
{
	UnitySendMessage([UNITY_GAME_OBJECT_NAME UTF8String], "OnNativeConnectCallback", "OnConnectSuccess");
}


- (void)onConnectFailure:(NSNotification*)notifyObj
{
	UnitySendMessage([UNITY_GAME_OBJECT_NAME UTF8String], "OnNativeConnectCallback", "OnConnectFailure");
}

#pragma mark Tapjoy Offers Notifications
- (void)onOffersResponse:(NSNotification*)notifyObj
{
	UnitySendMessage([UNITY_GAME_OBJECT_NAME UTF8String], "OnNativeOffersCallback", "OnOffersResponse");
}


- (void)onOffersResponseFailure:(NSNotification*)notifyObj
{
	UnitySendMessage([UNITY_GAME_OBJECT_NAME UTF8String], "OnNativeOffersCallback", "OnOffersResponseFailure");
}

#pragma mark Tapjoy Currency Methods
- (void)createCurrencyCallback:(NSString *)callbackName withParameters:(NSDictionary *)parameters andError:(NSError *)error
{
	NSString *currencyName = parameters[@"currencyName"];
	int balance = [parameters[@"amount"] intValue];
	
	if (error == nil)
	{
		NSMutableString *parameters = [[NSMutableString alloc] init];
		[parameters appendString:callbackName];
		[parameters appendString:@","];
		[parameters appendString: currencyName];
		[parameters appendString:@","];
		[parameters appendString: [NSString stringWithFormat:@"%i", balance]];
		
		UnitySendMessage([UNITY_GAME_OBJECT_NAME UTF8String], "OnNativeCurrencyCallback", [parameters UTF8String]);
	}
	else
	{
		NSError *errorValue = [NSError errorWithDomain:error.domain code:error.code userInfo:nil];
		
		NSMutableString *parameters = [[NSMutableString alloc] init];
		[parameters appendString:[NSString stringWithFormat:@"%@Failure", callbackName]];
		[parameters appendString:@","];
		[parameters appendString: [errorValue localizedDescription]];
		
		UnitySendMessage([UNITY_GAME_OBJECT_NAME UTF8String], "OnNativeCurrencyCallback", [parameters UTF8String]);
		
	}
}

- (void)earnedCurrency:(NSNotification*)notifyObj
{
	NSNumber *tapPointsEarned = notifyObj.object;
	earnedCurrencyAmount = [tapPointsEarned intValue];

	NSString *currencyName = [[NSUserDefaults standardUserDefaults] stringForKey:@"TJC_CURRENCY_KEY_NAME"];
	if (!currencyName) {
		currencyName = @"";
	}

	NSMutableString *parameters = [[NSMutableString alloc] init];
	[parameters appendString:@"OnEarnedCurrency,"];
	[parameters appendString:currencyName];
	[parameters appendString:@","];
	[parameters appendString: [NSString stringWithFormat:@"%i", earnedCurrencyAmount]];

	UnitySendMessage([UNITY_GAME_OBJECT_NAME UTF8String], "OnNativeCurrencyCallback", [parameters UTF8String]);
}

#pragma mark Tapjoy View Delegate Methods

- (void)viewWillAppearWithType:(int)viewType
{
	UnitySendMessage([UNITY_GAME_OBJECT_NAME UTF8String], "OnNativeViewCallback", [[NSString stringWithFormat:@"OnViewWillOpen,%i", viewType] UTF8String]);
}

- (void)viewDidAppearWithType:(int)viewType
{
	UnitySendMessage([UNITY_GAME_OBJECT_NAME UTF8String], "OnNativeViewCallback", [[NSString stringWithFormat:@"OnViewDidOpen,%i", viewType] UTF8String]);
}

- (void)viewWillDisappearWithType:(int)viewType
{
	UnitySendMessage([UNITY_GAME_OBJECT_NAME UTF8String], "OnNativeViewCallback", [[NSString stringWithFormat:@"OnViewWillClose,%i", viewType] UTF8String]);
}

- (void)viewDidDisappearWithType:(int)viewType
{
	UnitySendMessage([UNITY_GAME_OBJECT_NAME UTF8String], "OnNativeViewCallback", [[NSString stringWithFormat:@"OnViewDidClose,%i", viewType] UTF8String]);
}

#pragma mark Tapjoy Video

- (void)videoAdBegan {
	UnitySendMessage([UNITY_GAME_OBJECT_NAME UTF8String], "OnNativeVideoCallback", "OnVideoStart");
}

- (void)videoAdClosed {
	// No callback
}

- (void)videoAdCompleted {
	UnitySendMessage([UNITY_GAME_OBJECT_NAME UTF8String], "OnNativeVideoCallback", "OnVideoComplete");
}

- (void)videoAdError:(NSString*)errorMsg {
	UnitySendMessage([UNITY_GAME_OBJECT_NAME UTF8String], "OnNativeVideoCallback", [[NSString stringWithFormat:@"OnVideoError,%@", errorMsg] UTF8String]);
}

#pragma mark Tapjoy Placement Methods

- (void)createPlacement:(NSString *)guid withName:(NSString *)name
{
	// Create dictionary if its empty
	if (!placementDict_) {
		placementDict_ = [[NSMutableDictionary alloc] init];
	}
	
	TapjoyPlacementPlugin *tjPlacement = [TapjoyPlacementPlugin createPlacement:guid withName:name];

	TJPlacement *placement;
	placement = [TJPlacement placementWithName:name delegate:tjPlacement];
	
	if (placement == nil) {
		return;
	}
	
	[placementDict_ setObject:placement forKey:guid];
}

- (void)requestPlacementContentWithGuid:(NSString *)guid
{
 	TJPlacement *placement = [placementDict_ objectForKey:guid];
 	if (placement != nil) {
 		[placement setPresentationViewController:UnityGetGLViewController()];
		[placement requestContent];
	}
}

- (void)showPlacementContentWithGuid:(NSString *)guid
{
	TJPlacement *placement = [placementDict_ objectForKey:guid];
	if (placement != nil) {
		[placement showContentWithViewController:UnityGetGLViewController()];
	}
}

- (bool)isPlacementContentReady:(NSString *) guid
{
	TJPlacement *placement = [placementDict_ objectForKey:guid];
	if (placement != nil) {
		return [placement isContentReady];
	}
	return false;
}

- (bool)isPlacementContentAvailable:(NSString *)guid
{
	TJPlacement *placement = [placementDict_ objectForKey:guid];
	if (placement != nil) {
		return [placement isContentAvailable];
	}
	return false;
}

- (void)actionRequestCompleted:(NSString *) requestId
{
	TJActionRequest *placementRequest = [actionRequestDict_ objectForKey:requestId];
	if (placementRequest)
	{
		NSLog(@"Sending TJPlacementRequest completed");
		[placementRequest completed];
	}
}

- (void)actionRequestCancelled:(NSString *) requestId
{
	TJActionRequest *placementRequest = [actionRequestDict_ objectForKey:requestId];
	if(placementRequest)
	{
		NSLog(@"Sending TJPlacementRequest cancelled");
		[placementRequest cancelled];
	}
}

#pragma mark - Tapjoy Static Placement Delegate Methods

- (void)requestDidSucceed:(NSString *)guid withContent:(BOOL)contentIsAvailable
{
	NSMutableString *parameters = [[NSMutableString alloc] init];
	[parameters appendString:@"OnPlacementRequestSuccess,"];
	[parameters appendString:guid];
	[parameters appendString:@","];
	[parameters appendString:[NSString stringWithFormat:@"%s", contentIsAvailable ? "true" : "false"]];
	UnitySendMessage([UNITY_GAME_OBJECT_NAME UTF8String], "OnNativePlacementCallback", [parameters UTF8String]);
}

- (void)requestDidFail:(NSString *)guid error:(NSError*)error
{
	NSMutableString *parameters = [[NSMutableString alloc] init];
	[parameters appendString:@"OnPlacementRequestFailure,"];
	[parameters appendString:guid];
	
	NSString *errorString = [error localizedDescription];
	if (errorString == nil) {
		errorString = @"";
	}
	
	[parameters appendString:@","];
	[parameters appendString: errorString];
	
	UnitySendMessage([UNITY_GAME_OBJECT_NAME UTF8String], "OnNativePlacementCallback", [parameters UTF8String]);
}

- (void)contentIsReady:(NSString *)guid
{
	NSMutableString *parameters = [[NSMutableString alloc] init];
	[parameters appendString:@"OnPlacementContentReady,"];
	[parameters appendString:guid];
	
	UnitySendMessage([UNITY_GAME_OBJECT_NAME UTF8String], "OnNativePlacementCallback", [parameters UTF8String]);
}

- (void)contentDidAppear:(NSString *)guid
{
	NSMutableString *parameters = [[NSMutableString alloc] init];
	[parameters appendString:@"OnPlacementContentShow,"];
	[parameters appendString:guid];
	UnitySendMessage([UNITY_GAME_OBJECT_NAME UTF8String], "OnNativePlacementCallback", [parameters UTF8String]);
}

- (void)contentDidDisappear:(NSString *)guid
{
	NSMutableString *parameters = [[NSMutableString alloc] init];
	[parameters appendString:@"OnPlacementContentDismiss,"];
	[parameters appendString:guid];
	UnitySendMessage([UNITY_GAME_OBJECT_NAME UTF8String], "OnNativePlacementCallback", [parameters UTF8String]);
}

- (void)placement:(NSString *)guid didRequestPurchase:(TJActionRequest*)request productId:(NSString*)productId
{
	[actionRequestDict_ setObject:request forKey:guid];
	
	//TODO: use json encoding
	NSString *message = [NSString stringWithFormat: @"OnPurchaseRequest,%@,%@,%@,%@", guid, request.requestId, request.token, productId];
	UnitySendMessage([UNITY_GAME_OBJECT_NAME UTF8String], "OnNativePlacementCallback", [message UTF8String]);
}

- (void)placement:(NSString *)guid didRequestReward:(TJActionRequest*)request itemId:(NSString*)itemId quantity:(int)quantity
{
	[actionRequestDict_ setObject:request forKey:guid];
	
	//TODO: use json encoding
	NSString *message = [NSString stringWithFormat: @"OnRewardRequest,%@,%@,%@,%@,%i", guid, request.requestId, request.token, itemId, quantity];
	UnitySendMessage([UNITY_GAME_OBJECT_NAME UTF8String], "OnNativePlacementCallback", [message UTF8String]);
}

#pragma mark Bridge methods between C# and Objective-C
  
// Sets a value to a key in specific dictionary
- (void)setKey:(NSString*)key ToValue:(NSString*)value InDictionary:(NSString*)dictionaryToAddTo
  {
    // Get dictionary to add key and value to, creates one if doesn't exist
    NSMutableDictionary* currentDictionary = [[TapjoyConnectPlugin sharedTapjoyConnectPlugin] getReferenceDictionary:dictionaryToAddTo];
    
    [currentDictionary setObject:value forKey:key];
  }
  
// Sets a dictionary as a vaule to a key in specific dictionary
- (void)setKey:(NSString*)key ToDictionaryRefValue:(NSString*)dictionaryRefToAdd InDictionary:(NSString*)dictionaryToAddTo
  {
    // Get dictionary to add key and value to, creates one if doesn't exist
    NSMutableDictionary* dictionaryToTransferTo = [[TapjoyConnectPlugin sharedTapjoyConnectPlugin] getReferenceDictionary:dictionaryToAddTo];
    
    // Get reference to dictionary that needs to be added
    NSMutableDictionary* dictionaryToBeSetAsValue = [[TapjoyConnectPlugin sharedTapjoyConnectPlugin] getReferenceDictionary:dictionaryRefToAdd AndCreateNewInstance:NO];
    if (!dictionaryToBeSetAsValue) {
      NSLog(@"no dictionary reference by the name of: %@", dictionaryRefToAdd);
      return;
    }
    
    [dictionaryToTransferTo setObject:dictionaryToBeSetAsValue forKey:key];
  }
  
// Helper function to check if a dictionary is defined and if not creates one
- (NSMutableDictionary*)getReferenceDictionary:(NSString*) dictionaryName
  {
    return [[TapjoyConnectPlugin sharedTapjoyConnectPlugin] getReferenceDictionary:dictionaryName AndCreateNewInstance:YES];
  }
  
// Helper function to check if a dictionary is defined and will or will not create a new instance if one doesn't exsits
- (NSMutableDictionary*)getReferenceDictionary:(NSString*) dictionaryName AndCreateNewInstance:(BOOL) newInstance
  {
    NSMutableDictionary* currentDictionary = [cSharpDictionaryRefs_ objectForKey:dictionaryName];
    
    if (!currentDictionary && newInstance)
    {
      currentDictionary = [[NSMutableDictionary alloc] init];
      [cSharpDictionaryRefs_ setObject:currentDictionary forKey: dictionaryName];
    }
    return currentDictionary;
  }

@end

// Converts C style string to NSString
NSString* tjCreateNSString (const char* string)
{
	if (string)
		return [NSString stringWithUTF8String: string];
	else
		return @"";
}

// When native code plugin is implemented in .mm / .cpp file, then functions
// should be surrounded with extern "C" block to conform C function naming rules
extern "C" {
	void Tapjoy_Connect(const char* sdkKey)
	{
		[[Tapjoy sharedTapjoyConnect] setPlugin:@"unity"];
		
		NSDictionary* connectFlags = [[TapjoyConnectPlugin sharedTapjoyConnectPlugin] getReferenceDictionary:CONNECT_FLAG_KEY];
		if (connectFlags)
			[Tapjoy connect:tjCreateNSString(sdkKey) options:connectFlags];

		else
			[Tapjoy connect:tjCreateNSString(sdkKey)];
	}

	const char* Tapjoy_GetSDKVersion(){
		return [Tapjoy getVersion].UTF8String;
	}
	
	void Tapjoy_SetDebugEnabled(bool enabled)
	{
		[Tapjoy setDebugEnabled:enabled];
		[Tapjoy enableLogging:enabled];
	}

	void Tapjoy_SetUnityVersion(const char* version)
	{
		// TODO: Set unity version
	}

	void Tapjoy_SetAppDataVersion(const char*  dataVersion) {
		[Tapjoy setAppDataVersion: tjCreateNSString(dataVersion)];
	}

	void Tapjoy_ShowOffers(void)
	{
		// Displays the offer wall.
		[Tapjoy showOffersWithViewController:UnityGetGLViewController()];
	}
    
    void Tapjoy_ShowOffersWithCurrencyID(const char* currencyID, bool isSelectorVisible)
    {
        // Displays the offer wall.
		[Tapjoy showOffersWithCurrencyID:tjCreateNSString(currencyID) withViewController:UnityGetGLViewController() withCurrencySelector:isSelectorVisible];
    }

    void Tapjoy_GetCurrencyBalance(void)
	{
		[Tapjoy getCurrencyBalanceWithCompletion:^(NSDictionary *parameters, NSError *error) {
			[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] createCurrencyCallback:@"OnGetCurrencyBalanceResponse" withParameters:parameters andError:error];
		}];
	}
	
	void Tapjoy_SpendCurrency(int amount)
	{
		[Tapjoy spendCurrency:amount completion:^(NSDictionary *parameters, NSError *error) {
			[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] createCurrencyCallback:@"OnSpendCurrencyResponse" withParameters:parameters andError:error];
		}];
	}
	
	void Tapjoy_AwardCurrency(int amount)
	{
		[Tapjoy awardCurrency:amount completion:^(NSDictionary *parameters, NSError *error) {
			[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] createCurrencyCallback:@"OnAwardCurrencyResponse" withParameters:parameters andError:error];
		}];
	}

	float Tapjoy_GetCurrencyMultiplier()
    {
    	return [Tapjoy getCurrencyMultiplier];
    }

    void Tapjoy_SetCurrencyMultiplier(float multiplier)
    {
    	// Sets the currency multiplier, and fires off a network call to notify the server.
    	[Tapjoy setCurrencyMultiplier:multiplier];
    }
	
	void Tapjoy_ShowDefaultEarnedCurrencyAlert(void)
	{
		// Pops up a UIAlert notifying the user that they have successfully earned some currency.
		// This is the default alert, so you may place a custom alert here if you choose to do so.
		[Tapjoy showDefaultEarnedCurrencyAlert];
	}

	//#pragma mark - Tapjoy Event C Layer

	void Tapjoy_CreatePlacement(const char* guid, const char* name)
	{
		[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] createPlacement:tjCreateNSString(guid) withName:tjCreateNSString(name)];
	}

	void Tapjoy_RequestPlacementContent(const char* guid)
	{
		[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] requestPlacementContentWithGuid:tjCreateNSString(guid)];
	}

	void Tapjoy_ShowPlacementContent(const char* guid)
	{
		[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] showPlacementContentWithGuid:tjCreateNSString(guid)];	
	}

	bool Tapjoy_IsPlacementContentAvailable(const char* guid)
	{
		return [[TapjoyConnectPlugin sharedTapjoyConnectPlugin] isPlacementContentAvailable:tjCreateNSString(guid)];
	}

	bool Tapjoy_IsPlacementContentReady(const char* guid)
	{
		return [[TapjoyConnectPlugin sharedTapjoyConnectPlugin] isPlacementContentReady:tjCreateNSString(guid)];
	}

	void Tapjoy_ActionRequestCompleted(const char* requestId)
	{
		[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] actionRequestCompleted:tjCreateNSString(requestId)];
	}

	void Tapjoy_ActionRequestCancelled(const char* requestId)
	{
		[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] actionRequestCancelled:tjCreateNSString(requestId)];
	}

	void Tapjoy_EnablePaidAppWithActionID(const char* requestId)
	{
		/* Not supported on iOS -- Apple does not support paid app installs */
	}

	void Tapjoy_StartSession()
	{
		/* Done automagically on native iOS SDK */
	}

	void Tapjoy_EndSession()
	{
		/* Done automagically on native iOS SDK */
	}
	
	/* User */
	void Tapjoy_SetUserID(const char* userID)
	{
		[Tapjoy setUserID:tjCreateNSString(userID)];
	}

	void Tapjoy_SetUserLevel(int userLevel)
	{
		[Tapjoy setUserLevel:userLevel];
	}

	void Tapjoy_SetUserFriendCount(int friendCount)
	{
		[Tapjoy setUserFriendCount:friendCount];
	}

	void Tapjoy_SetUserCohortVariable(int variableIndex, const char* value)
	{
		[Tapjoy setUserCohortVariable:variableIndex value:tjCreateNSString(value)];
	}

	/* Track Event */
	void Tapjoy_TrackEvent(const char* category, const char* name,
                          const char* parameter1, const char* parameter2,
                          const char* value1Name, int64_t value1,
                          const char* value2Name, int64_t value2,
                          const char* value3Name, int64_t value3) {

		[Tapjoy trackEvent:tjCreateNSString(name)
                 category:tjCreateNSString(category)
               parameter1:tjCreateNSString(parameter1)
               parameter2:tjCreateNSString(parameter2)
               value1name:tjCreateNSString(value1Name)
                   value1:value1
               value2name:tjCreateNSString(value2Name)
                   value2:value2
               value3name:tjCreateNSString(value3Name)
                   value3:value3];
	}

	/* Track Purchase */
	void Tapjoy_TrackPurchase(const char* productId, const char* currencyCode, double price, const char* campaignId)
	{
		[Tapjoy trackPurchase:tjCreateNSString(productId)
                currencyCode:tjCreateNSString(currencyCode)
                       price:price
                  campaignId:tjCreateNSString(campaignId)];
	}

	// Bridge methods between Objective-C and C#
	void Tapjoy_SetKeyToValueInDictionary(const char* key, const char* value, const char* dictionaryToAddTo)
	{
		[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] setKey:tjCreateNSString(key) ToValue:tjCreateNSString(value) InDictionary:tjCreateNSString(dictionaryToAddTo)];
	}

	void Tapjoy_SetKeyToDictionaryRefValueInDictionary(const char* key, const char* dictionaryRefToAdd, const char* dictionaryToAddTo)
	{
		[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] setKey:tjCreateNSString(key) ToDictionaryRefValue:tjCreateNSString(dictionaryRefToAdd) InDictionary:tjCreateNSString(dictionaryToAddTo)];
	}
}

