// Copyright (C) 2014 by Tapjoy Inc.
//
// This file is part of the Tapjoy SDK.
//
// By using the Tapjoy SDK in your software, you agree to the terms of the Tapjoy SDK License Agreement.
//
// The Tapjoy SDK is bound by the Tapjoy SDK License Agreement and can be found here: https://www.tapjoy.com/sdk/license

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>


typedef enum TJCActionRequestTypeEnum {
	TJActionRequestInAppPurchase = 1,
	TJActionRequestVirtualGood,
	TJActionRequestCurrency,
	TJActionRequestNavigation
} TJCActionRequestType;


//TODO - check if these comments are valid and or can be added in here - thought it was moved to own components
/**
 A request for an app to take an action, triggered by TJplacement content.
 
 Your app should perform an action based on the value of the request type property:
 
 - `TJActionRequestInAppPurchase`: an in-app purchase request.  Your app should initiate an in-app
 purchase of the product identified by the identifier property.  If the purchase is completed
 successfully, the app should invoke the completed method; otherwise, it must invoke the
 cancelled method.
 - `TJActionRequestVirtualGood`: a virtual good award request.  Your app should award the user the
 item specified by the identifier with the amount specified by quantity.  If the virtual good
 was successfully rewarded, the app should invoke the completed method; otherwise, it must invoke the
 cancelled method
 - `TJActionRequestCurrency`: a currency change notification.  The user has been awarded the currency
 specified with identifier, with the amount specified by quantity.  The app should invoke the completed
 method once the request is handled.
 - `TJActionRequestNavigation`: an in-app navigation request.  Your app should attempt to navigate
 to the location specified by the identifier.  If the identifier is recognized and the app navigates
 successfully, the app should invoke the completed method; otherwise, it must invoke the
 cancelled method.
 
 The "identifiers" provided by the request here are the identifiers that you configure in the Tapjoy
 dashboard when creating content.  So, for example, you will only receive TJActionRequest VirtualGood
 requests with identifiers that you have configured yourself.  As such, you should carefully consider
 in advance what kinds of virtual good and navigation locations your app should support.  By implementing
 as many possible options upfront, your app will be more flexible and be able to make the best use
 of Tapjoy's publisher messaging.
 
 Your app *must* call either completed or cancelled to complete the lifecycle of the request, otherwise
 the content may fail to dismiss and yield control back to your app.
 */
@interface TJActionRequest : NSObject

/**
 * The type of the request
 *
 * This request type indicates the action that your app should take.  The value is one of
 * `TJActionRequestInAppPurchase`, `TJActionRequestVirtualGood`, `TJActionRequestCurrency`  or  `TJActionRequestNavigation`.
 * Your app should take action according to the request type as described in the class documentation above.
 */
@property (nonatomic,assign) TJCActionRequestType type;

/**
 * Called by your app to indicate that the request was processed successfully.
 *
 * Should be called when the placement request is completed successfully.  In the case of
 * `TJActionRequestInAppPurchase`, this indicates specifically that the user successfully
 * completed the purchase.
 */
- (void)completed;

/**
 * Called by your app to indicate that the request was not processed successfully.
 *
 * Should be called when the placement request is cancelled or otherwise not successfully completed.
 */
- (void)cancelled;

@property (nonatomic, copy) id callback;

//TODO - more description
/**
 * The identifier associated with the request.
 */
@property (nonatomic, copy) NSString* requestId;

//TODO - more description
/**
 * The identifier associated with the request.
 */
@property (nonatomic, copy) NSString* token;

@end

@class TJPlacement;

/**
  The Tapjoy placement Delegate protocol. Adopt this protocol in order to handle responses that send are received upon sending a TJPlacement.
 
  The methods to prepare are:
 
  - sendPlacementComplete:withContent: Called when an placement is sent successfully
  - sendPlacementFail:error: Called when an error occurs while sending the placement
  - contentIsReady:Called when content for an placement is successfully cached
  - contentDidAppear: Called when placement content did appear
  - contentDidDisappear: Called when placement content did disappear
  - placement:didRequestAction: Called when an action occurs, such as in-app purchases, and currency or virtual goods rewards
 
  As a result of executing the send method, one of these delegate callbacks will be invoked, and the application should act accordingly, presenting any content that is returned. For example:
 
    -(void) sendPlacementComplete: (TJPlacement*)placement withContent:(BOOL)contentIsAvailable
    {
        if(contentIsAvailable){
            [placement presentContentWithViewController:self];
        }
        else{
            NSLog(@"No content is available");
        }
    }
 */
@protocol TJPlacementDelegate <NSObject>

@optional

/**
 * Callback issued by TJ to publisher to state that placement request is successful
 * @param TJPlacement that was successful
 * @return n/a
 */
- (void)requestDidSucceed:(TJPlacement*)placement;                                          //Unified

/**
 * Called when an error occurs while sending the placement
 * @param placement The TJPlacement that was sent
 * @error error code
 * @return n/a
 */
- (void)requestDidFail:(TJPlacement*)placement error:(NSError*)error;                       //Unified

/**
 * Called when content for an placement is successfully cached
 * @param placement The TJPlacement that was sent
 */
- (void)contentIsReady:(TJPlacement*)placement;

/**
 * Called when placement content did appear
 * @param placement The TJPlacement that was sent
 * @return n/a
 */
- (void)contentDidAppear:(TJPlacement*)placement;                                           //Unified

/**
 * Called when placement content did disappear
 * @param placement The TJPlacement that was sent
 * @return n/a
 */
- (void)contentDidDisappear:(TJPlacement*)placement;                                        //Unified


/**
 * Callback issued by TJ to publisher when the user has successfully completed a purchase request
 * @param request - The TJActionRequest object
 * @param productId - the id of the offer that sent the request
 */
- (void)placement:(TJPlacement*)placement didRequestPurchase:(TJActionRequest*)request productId:(NSString*)productId;                        //Unified

/**
 * Callback issued by TJ to publisher when the user has successfully requests a reward
 * @param placement - The TJPlacement that triggered the action request
 * @param request   - The TJActionRequest object
 * @param itemId    - The itemId for whose reward has been requested
 * @param quantity  - The quantity of the reward for the requested itemId
 */

- (void)placement:(TJPlacement*)placement didRequestReward:(TJActionRequest*)request itemId:(NSString*)itemId quantity:(int)quantity;         //Unified


@end

/**
  The Tapjoy placement-Based Framework allows one to identify key placements within their application during development,
  and then reconfigure them on the dashboard as desired to help maximize monetization and engagement, without the need to update or resubmit the application.
  
  Use the TJPlacement class to define placement points in your application where ads and other content can be served.
  placements could include launching the application, completing an achievement, finishing a level, or any other moment conducive to communicating with your users.
 
  During your application development process, the key steps are to:
 
  1. Create and configure each placement as a TJPlacement
 
        TJPlacement *placement = [TJPlacement placementWithNAme: @"level_complete" delegate: self];
 
  2. Send the placement
 
        [placement send];
 
  3. Present any content that is returned by the placement callbacks defined in TJPlacementDelegate
 */
@interface TJPlacement : NSObject

/** The TJPlacementDelegate used to handle responses that are received upon sending this placement*/
@property (nonatomic, weak) id<TJPlacementDelegate> delegate;

/** The name of the placement */
@property (nonatomic, copy) NSString *placementName;                                        //Unified

/** UNIFIED **/
/** Whether content has been loaded and is ready to be presented */
@property (nonatomic, assign, readonly, getter=isContentReady) BOOL contentReady;           //Unified

/** When there is a fill to show - this value returns a true */
@property (nonatomic, assign, readonly, getter=isContentAvailable) BOOL contentAvailable;   //Unified

/** The UIViewController to show the content in */
@property (nonatomic, retain) UIViewController* presentationViewController;

/**
 * Creates a new instance of TJPlacement
 * @param placementName The name of the placement
 * @param delegate The class that implements the TJPlacementDelegate protocol
 */
+ (id)placementWithName:(NSString*)placementName delegate:(id<TJPlacementDelegate>)delegate;    //Unified

/**
 * Sends the placement to the server
 *
 * @return n/a
 */
- (void)requestContent;                                                                     //Unified

/**
 * Shows the content that was received from the server after sending this placement
 * @param viewController The ViewController to show the content in
 * @return n/a
 */
- (void)showContentWithViewController:(UIViewController*)viewController;                    //Unified


/**
 * Newer api's being added
 */


@end
