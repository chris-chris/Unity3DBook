#import <Foundation/Foundation.h>
#import <Tapjoy/Tapjoy.h>

@interface TapjoyConnectPlugin : NSObject <TJCVideoAdDelegate, TJPlacementVideoDelegate>
{
	// The amount of Tapjoy Managed currency this user has.
	int tapPoints;
	
	// The amount of Tapjoy Managed currency this user has earned since the app was last run.
	int earnedCurrencyAmount;
	
	// This is used for callbacks to managed code, after Tapjoy events are fired.
	const char* gameObjectName;

	NSMutableDictionary *cSharpDictionaryRefs_;

	// Events
	NSMutableDictionary *placementDict_;
	NSMutableDictionary *placementDelegateDict_;
	NSMutableDictionary *actionRequestDict_;
}

@property (nonatomic, copy) NSString *callbackHandlerName;

+ (TapjoyConnectPlugin*)sharedTapjoyConnectPlugin;

// Tapjoy Events
- (void)requestDidSucceed:(NSString*)guid withName:(NSString*)placementName withContent:(BOOL)contentIsAvailable;
- (void)requestDidFail:(NSString*)guid withName:(NSString*)placementName error:(NSError*)error;
- (void)contentIsReady:(NSString*)guid withName:(NSString*)placementName;
- (void)contentDidAppear:(NSString*)guid withName:(NSString*)placementName;
- (void)contentDidDisappear:(NSString*)guid withName:(NSString*)placementName;
- (void)placement:(NSString *)guid withName:(NSString*)placementName didRequestPurchase:(TJActionRequest*)request productId:(NSString*)productId;
- (void)placement:(NSString *)guid withName:(NSString*)placementName didRequestReward:(TJActionRequest*)request itemId:(NSString*)itemId quantity:(int)quantity;

// Tapjoy placement video callbacks
- (void)placementVideoDidStart:(NSString*)guid withName:(NSString *)placementName;
- (void)placementVideoDidComplete:(NSString*)guid withName:(NSString *)placementName;
- (void)placementVideoDidFail:(NSString*)guid withName:(NSString *)placementName error:(NSString*)errorMsg;

// Bridge methods between Objective-C and C#
- (void)setKey:(NSString*)key ToValue:(NSString*)value InDictionary:(NSString*)dictionaryToAddTo;
- (void)setKey:(NSString*)key ToDictionaryRefValue:(NSString*)dictionaryRefToAdd InDictionary:(NSString*)dictionaryToAddTo;

- (NSMutableDictionary*)getReferenceDictionary:(NSString*) dictionaryName;
- (NSMutableDictionary*)getReferenceDictionary:(NSString*) dictionaryName AndCreateNewInstance:(BOOL) newInstance;

@end