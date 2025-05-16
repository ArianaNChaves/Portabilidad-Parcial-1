using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener
{
    public BannerManager    banner;
    public InterstitialManager interstitial;
    public RewardedAdManager   rewardedAd;

    private string _gameId;

    void Awake()
    {
#if UNITY_IOS
        _gameId = "5855867";
#elif UNITY_ANDROID
        _gameId = "5855866";
#endif

#if (UNITY_ANDROID || UNITY_IOS)
        if (!Advertisement.isInitialized && Advertisement.isSupported)
            Advertisement.Initialize(_gameId, true, this);
#endif
    }

    public void OnInitializationComplete()
    {
#if (UNITY_ANDROID || UNITY_IOS)
        Debug.Log("Unity Ads initialization complete.");
        banner.Show();
        interstitial.Initialize();
        rewardedAd.Initialize();
#endif
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
#if (UNITY_ANDROID || UNITY_IOS)
        Debug.Log($"Unity Ads Initialization Failed: {error} - {message}");
#endif
    }
}