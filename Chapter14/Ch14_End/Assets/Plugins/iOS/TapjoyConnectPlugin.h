#import <Foundation/Foundation.h>
#import <Tapjoy/Tapjoy.h>

@interface TapjoyConnectPlugin : NSObject <TJCVideoAdDelegate, TJCViewDelegate>
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
	NSMutableDictionary *actionRequestDict_;
}

@property (nonatomic, copy) NSString *callbackHandlerName;
@property (nonatomic, retain) NSMutableDictionary *cSharpDictionaryRefs;
@property (nonatomic, retain) NSMutableDictionary *placementDict;
@property (nonatomic, retain) NSMutableDictionary *actionRequestDict;

+ (TapjoyConnectPlugin*)sharedTapjoyConnectPlugin;

// Tapjoy Events
- (void)requestDidSucceed:(NSString*)guid withContent:(BOOL)contentIsAvailable;
- (void)requestDidFail:(NSString*)guid error:(NSError*)error;
- (void)contentIsReady:(NSString*)guid;
- (void)contentDidAppear:(NSString*)guid;
- (void)contentDidDisappear:(NSString*)guid;
- (void)placement:(NSString *)guid didRequestPurchase:(TJActionRequest*)request productId:(NSString*)productId;
- (void)placement:(NSString *)guid didRequestReward:(TJActionRequest*)request itemId:(NSString*)itemId quantity:(int)quantity;

// Bridge methods between Objective-C and C#
- (void)setKey:(NSString*)key ToValue:(NSString*)value InDictionary:(NSString*)dictionaryToAddTo;
- (void)setKey:(NSString*)key ToDictionaryRefValue:(NSString*)dictionaryRefToAdd InDictionary:(NSString*)dictionaryToAddTo;

- (NSMutableDictionary*)getReferenceDictionary:(NSString*) dictionaryName;
- (NSMutableDictionary*)getReferenceDictionary:(NSString*) dictionaryName AndCreateNewInstance:(BOOL) newInstance;

@end

