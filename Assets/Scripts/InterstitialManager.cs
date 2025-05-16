using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
    string _adUnitId;
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

    public void OnUnityAdsAdLoaded(string placementId)
    {
        _adLoaded = true;
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Interstitial: Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
    }

    public void ShowInterstitial()
    {
        if(_adLoaded)
            Advertisement.Show(_adUnitId, this);
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
        Debug.Log("termin� de ver el ad");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
    }
}
