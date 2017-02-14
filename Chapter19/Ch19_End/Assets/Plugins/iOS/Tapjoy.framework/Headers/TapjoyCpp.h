//
// Copyright (c) 2015 Tapjoy, Inc.
// All rights reserved.
//

/**
 * @file TapjoyCpp.h
 * @brief C++ API header file of the Tapjoy SDK
 */

#ifndef TapjoyCpp_h
#define TapjoyCpp_h

#include <stdint.h>

#if defined(ANDROID)
#include <jni.h>
#endif

namespace tapjoy {

  class TJConnectListener;
  class TJOffersListener;
  class TJGetCurrencyBalanceListener;
  class TJSpendCurrencyListener;
  class TJAwardCurrencyListener;
  class TJEarnedCurrencyListener;
  class TJViewListener;
  class TJVideoListener;
  class TJPlacementListener;

  /**
   * @brief C++ API class of the Tapjoy SDK.
   */
  class Tapjoy {
  public:

#if defined(ANDROID) && defined(TAPJOY_STATIC)
    /**
     * @brief Sets the JavaVM instance to interoperate with Java for Android only.
     *        This method is equivalent to Tapjoy.initStaticLibrary() in Java.
     * @param vm
     *        a pointer to JavaVM instance
     */
    static jint setJavaVM(JavaVM* vm);
#endif

#if defined(ANDROID)
    /*
     * @brief Set common context for Android
     * @param context
     *        a context
     */
    static void setContext(jobject context);
#endif

    /**
     * @brief Returns the name of the library linked.
     */
    static const char* getLibraryName();

    /**
     * @brief Returns the version name of the SDK.
     * @return a string which represents the version name
     */
    static const char* getVersion();

    /**
     * @brief Enables the debug mode of the SDK.
     * @param enable
     *        true to enable
     */
    static void setDebugEnabled(bool enable);


#if defined(ANDROID)
    /**
     * @brief Connects to the Tapjoy Server
     * @param context
     *        a Java object of the application context.
     * @param sdkKey
     *        Your Tapjoy SDK Key.
     * @param listener
     *        listener for connect success/failure
     */
    static bool connect(jobject context, const char* sdkKey, TJConnectListener* listener = NULL);
#endif

    /**
     * @brief Connects to the Tapjoy Server
     * @param sdkKey
     *        Your Tapjoy SDK Key.
     * @param listener
     *        listener for connect success/failure
     */
    static bool connect(const char* sdkKey, TJConnectListener* listener = NULL);

    /**
     * @deprecated Deprecated since version 11.0.0. Tapjoy Offerwall should now be configured through {@link TJPlacement}
     * @brief Show available offers to the user. Data is returned to the callback
     *        {@link TJOffersListener}
     *
     * @param listener
     *        The class implementing the TapjoyOffersListener callback.
     */
    static void showOffers(TJOffersListener* listener);

    /**
     * @deprecated Deprecated since version 11.0.0. Tapjoy Offerwall should now be configured through {@link TJPlacement}
     * @brief Show available offers using a currencyID and currency selector flag. This
     *        should only be used if the application supports multiple currencies and
     *        is NON-MANAGED by Tapjoy. Data is returned to the callback
     *        {@link TJOffersListener}.
     *
     * @param currencyID
     *        ID of the currency to display.
     * @param enableCurrencySelector
     *        whether to display the currency selector to toggle currency.
     * @param listener
     *        the class implementing the TapjoyOffersListener callback.
     */
    static void showOffersWithCurrencyID(const char* currencyID, bool enableCurrencySelector, TJOffersListener* listener);

    /**
     * @brief Gets the virtual currency data from the server for this device. The data
     *        will be returned in a callback to onCurrencyBalanceResponse() to the
     *        class implementing the listener.
     *
     * @param listener
     *        the class implementing the TapjoyCurrencyBalanceListener
     *        callback
     */
    static void getCurrencyBalance(TJGetCurrencyBalanceListener* listener);

    /**
     * @brief Spends virtual currency. This can only be used for currency managed by
     *        Tapjoy. The data will be returned in a callback to
     *        onSpendCurrencyResponse() to the class implementing the listener.
     *
     * @param listener
     *        the class implementing the TapjoySpendCurrencyListener
     *        callback
     */
    static void spendCurrency(int amount, TJSpendCurrencyListener* listener);

    /**
     * @brief Awards virtual currency. This can only be used for currency managed by
     *        Tapjoy. The data will be returned in a callback to
     *        onAwardCurrencyResponse() to the class implementing the listener.
     *
     * @param listener
     *        the class implementing the TJAwardCurrencyListener
     *        callback
     */
    static void awardCurrency(int amount, TJAwardCurrencyListener* listener);

    /**
     * @brief Sets the listener which gets informed whenever virtual currency is
     *        earned.
     *
     * @param listener
     *        class implementing TJEarnedCurrencyListener
     */
    static void setEarnedCurrencyListener(TJEarnedCurrencyListener* listener);

    /**
     * @brief ONLY USE FOR NON-MANAGED (by TAPJOY) CURRENCY.<br>
     *        Sets the multiplier for the virtual currency displayed in Offers, Banner
     *        Ads, etc. The default is 1.0
     *
     * @param multiplier
     */
    static void setCurrencyMultiplier(float multiplier);

    /**
     * @brief Gets the multiplier for the virtual currency display.
     *
     * @return Currency multiplier.
     */
    static float getCurrencyMultiplier();

    /**
     * @brief Tracks a purchase
     *
     * @param productId
     *        the product identifier
     * @param currencyCode
     *        the currency code of price as an alphabetic currency code
     *        specified in ISO 4217, i.e. "USD", "KRW"
     * @param price
     *        the price of product
     * @param campaignId
     *        the campaign id of the Purchase Action Request if it
     *        initiated this purchase, can be null
     */
    static void trackPurchase(const char* productId, const char* currencyCode, double price, const char* campaignId);

    /**
     * @brief Tracks an event of the given name without category
     */
    static void trackEvent(const char* name);

    /**
     * @brief Tracks an event of the given name without category, with a value.
     *
     * @param name
     *        the name of event
     * @param value
     *        the value of event
     */
    static void trackEvent(const char* name, int64_t value);

    /**
     * @brief Tracks an event of the given category and name, with a value.
     */
    static void trackEvent(const char* category, const char* name, int64_t value);

    /**
     * @brief Tracks an event of the given category and name, with two parameters.
     */
    static void trackEvent(const char* category, const char* name, const char* parameter1, const char* parameter2);

    /**
     * @brief Tracks an event of the given category and name, with two parameters and a
     *        value.
     */
    static void trackEvent(const char* category, const char* name, const char* parameter1, const char* parameter2, int64_t value);

    /**
     * @brief Tracks an event of the given category and name, with two parameters and a
     *        named values.
     */
    static void trackEvent(const char* category, const char* name, const char* parameter1, const char* parameter2, const char* valueName, int64_t value);

    /**
     * @brief Tracks an event of the given category and name, with two parameters and
     *        two named values.
     */
    static void trackEvent(const char* category, const char* name, const char* parameter1, const char* parameter2, const char* value1Name, int64_t value1, const char* value2Name, int64_t value2);


    static void trackEvent(const char* category, const char* name, const char* parameter1, const char* parameter2, const char* value1Name, int64_t value1, const char* value2Name, int64_t value2, const char* value3Name, int64_t value3);

    /**
     * @brief Manual session tracking. Notifies the SDK that new session of your
     *        application has been started.
     */
    static void startSession();

    /**
     * @brief Manual session tracking. Notifies the SDK that the session of your
     *        application has been ended.
     */
    static void endSession();

    /**
     * @brief Assigns a user ID for this user/device. This is used to identify the user
     *        in your application.
     *
     *        This is REQUIRED for NON-MANAGED currency apps.
     *
     * @param userID
     *        user ID you wish to assign to this device
     */
    static void setUserID(const char* userID);

    /**
     * @brief Sets the level of the user.
     *
     * @param userLevel
     *        the level of the user
     */
    static void setUserLevel(int userLevel);

    /**
     * @brief Sets the friends count of the user.
     *
     * @param friendCount
     *        the number of friends
     */
    static void setUserFriendCount(int friendCount);

    /**
     * @brief Sets the data version of the App or Game.
     *
     * @param dataVersion
     *        the data version
     */
    static void setAppDataVersion(const char* dataVersion);

    /**
     * @brief Sets a variable of user for the cohort analysis.
     *
     * @param variableIndex
     *        the index of the variable to set, must be in the range 1 to 5
     * @param value
     *        the value of the variable to set, or null to unset
     */
    static void setUserCohortVariable(int variableIndex, const char* value);

    /**
     * @brief Set to receive callbacks when Tapjoy views open and close
     *
     * @param listener
     *        onOffersResponse to receive open/close callbacks
     */
    static void setTapjoyViewListener(TJViewListener* listener);

    /**
     * @brief Sets the video listener. Use this to receive callbacks for on video
     *        start, complete and error.
     *
     * @param listener
     *        video to receive start/complete/error callbacks
     */
    static void setVideoListener(TJVideoListener* listener);

    /**
     * @brief ONLY USE FOR PAID APP INSTALLS.<br>
     *        This method should be called in the onCreate() method of your first
     *        activity after calling connect.<br>
     *        Must enable a paid app Pay-Per-Action on the Tapjoy dashboard. Starts a
     *        15 minute timer. After which, will send an actionComplete call with the
     *        paid app PPA to inform the Tapjoy server that the paid install PPA has
     *        been completed.
     *
     * @param paidAppPayPerActionID
     *        The Pay-Per-Action ID for this paid app download action.
     */
    static void enablePaidAppWithActionID(const char* paidAppPayPerActionID);

    /**
     * @brief Informs the Tapjoy server that the specified Pay-Per-Action was
     *        completed. Should be called whenever a user completes an in-game action.
     *
     * @param actionID
     *        The action ID of the completed action.
     */
    static void actionComplete(const char* actionID);

    /**
     * @brief Helper function to check if SDK is initialized
     */
    static bool isConnected();


  };

#if defined(ANDROID)
  typedef jobject TJActionRequestHandle;
  typedef jobject TJPlacementHandle;
#else
  typedef void* TJActionRequestHandle;
  typedef void* TJPlacementHandle;
#endif

  class TJConnectListener {
  public:
    virtual ~TJConnectListener() {}

    virtual void onConnectSuccess() {}
    virtual void onConnectFailure() {}
  };

  class TJViewListener {
  public:
    virtual ~TJViewListener() {}

    virtual void onViewWillClose(int viewType) {}
    virtual void onViewDidClose(int viewType) {}
    virtual void onViewWillOpen(int viewType) {}
    virtual void onViewDidOpen(int viewType) {}
  };

  class TJAwardCurrencyListener {
  public:
    virtual ~TJAwardCurrencyListener() {}

    virtual void onAwardCurrencyResponse(const char* currencyName, int balance) {}
    virtual void onAwardCurrencyResponseFailure(const char* error) {}
  };

  class TJEarnedCurrencyListener {
  public:
    virtual ~TJEarnedCurrencyListener() {}

    virtual void onEarnedCurrency(const char* currencyName, int amount) {}
  };

  class TJGetCurrencyBalanceListener {
  public:
    virtual ~TJGetCurrencyBalanceListener() {}

    virtual void onGetCurrencyBalanceResponse(const char* currencyName, int balance) {}
    virtual void onGetCurrencyBalanceResponseFailure(const char* error) {}
  };

  class TJOffersListener {
  public:
    virtual ~TJOffersListener() {}

    virtual void onOffersResponse() {}
    virtual void onOffersResponseFailure(const char* error) {}
  };

  class TJSpendCurrencyListener {
  public:
    virtual ~TJSpendCurrencyListener() {}

    virtual void onSpendCurrencyResponse(const char* currencyName, int balance) {}
    virtual void onSpendCurrencyResponseFailure(const char* error) {}
  };

  class TJVideoListener {
  public:
    virtual ~TJVideoListener() {}

    virtual void onVideoStart() {}
    virtual void onVideoClose() {}
    virtual void onVideoError(int statusCode) {}
    virtual void onVideoComplete() {}
  };

  class TJPlacementListener {
  public:
    virtual ~TJPlacementListener() {}

    virtual void onRequestSuccess(TJPlacementHandle placementHandle, const char* placementName) {}
    virtual void onRequestFailure(TJPlacementHandle placementHandle, const char* placementName, int errorCode, const char* errorMessage) {}
    virtual void onContentReady(TJPlacementHandle placementHandle, const char* placementName) {}
    virtual void onContentShow(TJPlacementHandle placementHandle, const char* placementName) {}
    virtual void onContentDismiss(TJPlacementHandle placementHandle, const char* placementName) {}
    virtual void onPurchaseRequest(TJPlacementHandle placementHandle, const char* placementName, TJActionRequestHandle requestHandle, const char* requestId, const char* requestToken, const char* productId) {}
    virtual void onRewardRequest(TJPlacementHandle placementHandle, const char* placementName, TJActionRequestHandle requestHandle, const char* requestId, const char* requestToken, const char* itemId, int quantity) {}
  };

  class TJActionRequest {
  public:
    static void completed(TJActionRequestHandle actionRequestHandle);
    static void cancelled(TJActionRequestHandle actionRequestHandle);
  };

  class TJPlacement {
  public:
#if defined(ANDROID)
    static TJPlacementHandle create(jobject activityContext, const char* placementName, TJPlacementListener* listener);
#endif
    static TJPlacementHandle create(const char* placementName, TJPlacementListener* listener);
    static void release(TJPlacementHandle placementHandle);
    static bool isContentReady(TJPlacementHandle placementHandle);
    static bool isContentAvailable(TJPlacementHandle placementHandle);
    static void requestContent(TJPlacementHandle placementHandle);
    static void showContent(TJPlacementHandle placementHandle);
  };

}

#endif // TapjoyCpp_h
