#import "TapjoyPlacementPlugin.h"
#import "TapjoyConnectPlugin.h"

@implementation TapjoyPlacementPlugin

@synthesize myGuid = myGuid_;

- (id)init
{
	self = [super init];
    
    if (self)
    {
    }

    return self;
}

+ (id)createPlacement:(NSString *)guid withName:(NSString *)name
{
	TapjoyPlacementPlugin *instance = [[TapjoyPlacementPlugin alloc] init];
	[instance setMyGuid:guid];

	return instance;
}

#pragma mark - TJPlacementDelegate

- (void)requestDidSucceed:(TJPlacement*)placement
{
	NSLog(@"trying to send event complete back to TapjoyConnectPlugin");
	[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] requestDidSucceed:myGuid_ withName:placement.placementName withContent:placement.isContentAvailable];
}

- (void)requestDidFail:(TJPlacement*)placement error:(NSError*)error
{
	NSLog(@"trying to send event fail back to TapjoyConnectPlugin");
    [[TapjoyConnectPlugin sharedTapjoyConnectPlugin] requestDidFail:myGuid_ withName:placement.placementName error:error];
}

- (void)contentIsReady:(TJPlacement*)placement
{
	[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] contentIsReady:myGuid_ withName:placement.placementName];
}

- (void)contentDidAppear:(TJPlacement*)placement
{
	NSLog(@"trying to send contentDidAppear to TapjoyConnectPlugin");
	[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] contentDidAppear:myGuid_ withName:placement.placementName];
}

- (void)contentDidDisappear:(TJPlacement*)placement
{
	NSLog(@"trying to send contentDidDisappear to TapjoyConnectPlugin");
	[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] contentDidDisappear:myGuid_ withName:placement.placementName];
}

- (void)placement:(TJPlacement*)placement didRequestPurchase:(TJActionRequest*)request productId:(NSString*)productId
{
	[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] placement:myGuid_ withName:placement.placementName didRequestPurchase:request productId:productId];
}

- (void)placement:(TJPlacement*)placement didRequestReward:(TJActionRequest*)request itemId:(NSString*)itemId quantity:(int)quantity
{
	[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] placement:myGuid_ withName:placement.placementName didRequestReward:request itemId:itemId quantity:quantity];
}

#pragma mark Tapjoy TJPlacementVideoDelegate Methods

- (void)videoDidStart:(TJPlacement*)placement {
	[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] placementVideoDidStart:myGuid_ withName:(NSString *)placement.placementName];
}

- (void)videoDidComplete:(TJPlacement*)placement {
    [[TapjoyConnectPlugin sharedTapjoyConnectPlugin] placementVideoDidComplete:myGuid_ withName:(NSString *)placement.placementName];
}

- (void)videoDidFail:(TJPlacement*)placement error:(NSString*)errorMsg {
	[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] placementVideoDidFail:myGuid_ withName:(NSString *)placement.placementName error:errorMsg];
}
@end
