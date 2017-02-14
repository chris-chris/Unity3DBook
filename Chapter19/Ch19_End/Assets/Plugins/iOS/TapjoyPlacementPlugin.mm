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
	[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] requestDidSucceed:myGuid_ withContent:placement.isContentAvailable];
}

- (void)requestDidFail:(TJPlacement*)placement error:(NSError*)error
{
	NSLog(@"trying to send event fail back to TapjoyConnectPlugin");
	[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] requestDidFail:myGuid_ error:error];
}

- (void)contentIsReady:(TJPlacement*)placement
{
	[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] contentIsReady:myGuid_];
}

- (void)contentDidAppear:(TJPlacement*)placement
{
	NSLog(@"trying to send contentDidAppear to TapjoyConnectPlugin");
	[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] contentDidAppear:myGuid_];
}

- (void)contentDidDisappear:(TJPlacement*)placement
{
	NSLog(@"trying to send contentDidDisappear to TapjoyConnectPlugin");
	[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] contentDidDisappear:myGuid_];
}

- (void)placement:(TJPlacement*)placement didRequestPurchase:(TJActionRequest*)request productId:(NSString*)productId
{
	[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] placement:myGuid_ didRequestPurchase:request productId:productId];
}

- (void)placement:(TJPlacement*)placement didRequestReward:(TJActionRequest*)request itemId:(NSString*)itemId quantity:(int)quantity
{
	[[TapjoyConnectPlugin sharedTapjoyConnectPlugin] placement:myGuid_ didRequestReward:request itemId:itemId quantity:quantity];
}

@end
