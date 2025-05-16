using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAdManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener

{
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    [SerializeField] private GameController gameController;
    string _adUnitId = null;
    bool _adLoaded = false;

    void Start()
    {
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif           
    }

    internal void Initialize()
    {
        Advertisement.Load(_adUnitId, this);
    }

    public void ShowRewardedAd()
    {
        Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Rewarded Ad: Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string _adUnitId)
    {
        Debug.Log("mostrando interstitial");
    }

    public void OnUnityAdsShowClick(string _adUnitId)
    {
        Debug.Log("clickearon el ad");
    }

    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (_adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            gameController.AddTime(2f);
            Advertisement.Load(_adUnitId, this);
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
    }
    public void OnUnityAdsAdLoaded(string placementId)
    {
        _adLoaded = true;
    }

}
