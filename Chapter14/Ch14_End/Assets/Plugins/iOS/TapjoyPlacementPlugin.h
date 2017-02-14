#import <Foundation/Foundation.h>
#import <Tapjoy/Tapjoy.h>

@interface TapjoyPlacementPlugin : NSObject <TJPlacementDelegate>
{
	// This is set to the guid of the object that will implement the callback functions for handling Tapjoy events.
	NSString *myGuid_;	
}

@property (nonatomic, copy) NSString *myGuid;

+ (id)createPlacement:(NSString*)guid withName:(NSString*)name;

// Tapjoy Events
- (void)requestDidSucceed:(TJPlacement*)placement;
- (void)requestDidFail:(TJPlacement*)placement error:(NSError*)error;
- (void)contentIsReady:(TJPlacement*)placement;
- (void)contentDidAppear:(TJPlacement*)placement;
- (void)contentDidDisappear:(TJPlacement*)placement;

@end
