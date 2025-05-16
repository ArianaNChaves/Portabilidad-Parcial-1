using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener
{
    public BannerManager banner;
    public InterstitialManager interstitial;
    public RewardedAdManager rewardedAd;

    private string _gameId;

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        banner.Show();
        interstitial.Initialize();
        rewardedAd.Initialize();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    void Awake()
    {
#if UNITY_IOS
        _gameId = "5855867";
#elif UNITY_ANDROID
        _gameId = "5855866";
#elif UNITY_EDITOR
        _gameId = "5629780";
#endif
    
        if(!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, true, this);
        }

    }
}
