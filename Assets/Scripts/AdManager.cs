using UnityEngine;
using System;

#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

/// <summary>
/// Reklam yönetim sistemi - Unity Ads entegrasyonu
/// Mobil ve PC platformlar için reklam gösterimi
/// </summary>
public class AdManager : MonoBehaviour
#if UNITY_ADS
    , IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
#endif
{
    [Header("Unity Ads Settings")]
    public string androidGameId = "YOUR_ANDROID_GAME_ID";
    public string iosGameId = "YOUR_IOS_GAME_ID";
    public bool testMode = true;

    [Header("Ad Unit IDs")]
    public string bannerAdUnitId = "Banner_Android";
    public string interstitialAdUnitId = "Interstitial_Android";
    public string rewardedAdUnitId = "Rewarded_Android";

    [Header("Ad Display Settings")]
    public bool showBannerOnStart = false;
    public int showInterstitialEveryNDeaths = 3;
    public float rewardedVideoRewardMultiplier = 2f;

    [Header("Statistics")]
    public int totalDeaths = 0;
    public int adsWatched = 0;
    public int rewardsEarned = 0;

    private static AdManager instance;
    private bool isInitialized = false;
    private bool rewardedAdReady = false;
    private bool interstitialAdReady = false;
    private Action onRewardedAdSuccess;
    private Action onRewardedAdFailed;

    public static AdManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AdManager>();
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAds();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void InitializeAds()
    {
#if UNITY_ADS
        string gameId = "";

        #if UNITY_ANDROID
            gameId = androidGameId;
            bannerAdUnitId = "Banner_Android";
            interstitialAdUnitId = "Interstitial_Android";
            rewardedAdUnitId = "Rewarded_Android";
        #elif UNITY_IOS
            gameId = iosGameId;
            bannerAdUnitId = "Banner_iOS";
            interstitialAdUnitId = "Interstitial_iOS";
            rewardedAdUnitId = "Rewarded_iOS";
        #else
            // PC/Other platforms - use Android IDs for testing
            gameId = androidGameId;
        #endif

        if (!string.IsNullOrEmpty(gameId) && gameId != "YOUR_ANDROID_GAME_ID")
        {
            Advertisement.Initialize(gameId, testMode, this);
            Debug.Log("[AdManager] Initializing Unity Ads with Game ID: " + gameId);
        }
        else
        {
            Debug.LogWarning("[AdManager] Unity Ads Game ID not set! Please configure in AdManager.");
            isInitialized = false;
        }
#else
        Debug.LogWarning("[AdManager] Unity Ads package not installed. Please install via Package Manager.");
#endif
    }

#if UNITY_ADS
    // IUnityAdsInitializationListener implementation
    public void OnInitializationComplete()
    {
        isInitialized = true;
        Debug.Log("[AdManager] Unity Ads initialization complete.");

        // Load ads
        LoadInterstitialAd();
        LoadRewardedAd();

        if (showBannerOnStart)
        {
            ShowBannerAd();
        }
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        isInitialized = false;
        Debug.LogError($"[AdManager] Unity Ads initialization failed: {error} - {message}");
    }

    // Banner Ads
    public void ShowBannerAd()
    {
        if (!isInitialized) return;

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Load(bannerAdUnitId);
        Advertisement.Banner.Show(bannerAdUnitId);
        Debug.Log("[AdManager] Showing banner ad");
    }

    public void HideBannerAd()
    {
        if (!isInitialized) return;
        Advertisement.Banner.Hide();
    }

    // Interstitial Ads (full screen ads between games)
    public void LoadInterstitialAd()
    {
        if (!isInitialized) return;
        Advertisement.Load(interstitialAdUnitId, this);
    }

    public void ShowInterstitialAd()
    {
        if (!isInitialized)
        {
            Debug.LogWarning("[AdManager] Ads not initialized yet.");
            return;
        }

        totalDeaths++;

        // Show interstitial ad every N deaths
        if (totalDeaths % showInterstitialEveryNDeaths == 0)
        {
            if (interstitialAdReady)
            {
                Advertisement.Show(interstitialAdUnitId, this);
                adsWatched++;
                Debug.Log("[AdManager] Showing interstitial ad");
            }
            else
            {
                Debug.LogWarning("[AdManager] Interstitial ad not ready. Loading...");
                LoadInterstitialAd();
            }
        }
    }

    // Rewarded Video Ads (watch ad for rewards)
    public void LoadRewardedAd()
    {
        if (!isInitialized) return;
        Advertisement.Load(rewardedAdUnitId, this);
    }

    public void ShowRewardedAd(Action onSuccess, Action onFailed = null)
    {
        if (!isInitialized)
        {
            Debug.LogWarning("[AdManager] Ads not initialized.");
            onFailed?.Invoke();
            return;
        }

        if (rewardedAdReady)
        {
            onRewardedAdSuccess = onSuccess;
            onRewardedAdFailed = onFailed;
            Advertisement.Show(rewardedAdUnitId, this);
            adsWatched++;
            Debug.Log("[AdManager] Showing rewarded ad");
        }
        else
        {
            Debug.LogWarning("[AdManager] Rewarded ad not ready.");
            onFailed?.Invoke();
            LoadRewardedAd();
        }
    }

    public bool IsRewardedAdReady()
    {
        return isInitialized && rewardedAdReady;
    }

    // IUnityAdsLoadListener implementation
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log($"[AdManager] Ad loaded: {placementId}");

        if (placementId == interstitialAdUnitId)
        {
            interstitialAdReady = true;
        }
        else if (placementId == rewardedAdUnitId)
        {
            rewardedAdReady = true;
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"[AdManager] Failed to load ad {placementId}: {error} - {message}");

        if (placementId == interstitialAdUnitId)
        {
            interstitialAdReady = false;
        }
        else if (placementId == rewardedAdUnitId)
        {
            rewardedAdReady = false;
        }
    }

    // IUnityAdsShowListener implementation
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"[AdManager] Ad show complete: {placementId} - {showCompletionState}");

        if (placementId == rewardedAdUnitId)
        {
            if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
            {
                Debug.Log("[AdManager] Rewarded ad completed! Giving reward.");
                rewardsEarned++;
                onRewardedAdSuccess?.Invoke();
            }
            else
            {
                Debug.Log("[AdManager] Rewarded ad not completed.");
                onRewardedAdFailed?.Invoke();
            }

            onRewardedAdSuccess = null;
            onRewardedAdFailed = null;

            // Load next rewarded ad
            LoadRewardedAd();
        }
        else if (placementId == interstitialAdUnitId)
        {
            // Load next interstitial ad
            LoadInterstitialAd();
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"[AdManager] Ad show failed: {placementId} - {error} - {message}");

        if (placementId == rewardedAdUnitId)
        {
            onRewardedAdFailed?.Invoke();
            onRewardedAdSuccess = null;
            onRewardedAdFailed = null;
            LoadRewardedAd();
        }
        else if (placementId == interstitialAdUnitId)
        {
            LoadInterstitialAd();
        }
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log($"[AdManager] Ad show started: {placementId}");
        // Pause game if needed
        Time.timeScale = 0f;
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log($"[AdManager] Ad clicked: {placementId}");
    }
#endif

    // Helper methods for non-Unity Ads builds
    public void ShowBannerAdFallback()
    {
        Debug.Log("[AdManager] Banner ad would show here (Unity Ads not available)");
    }

    public void ShowInterstitialAdFallback()
    {
        Debug.Log("[AdManager] Interstitial ad would show here (Unity Ads not available)");
    }

    public void ShowRewardedAdFallback(Action onSuccess, Action onFailed)
    {
        Debug.Log("[AdManager] Rewarded ad would show here (Unity Ads not available)");
        // For testing without ads, just give reward
        if (testMode)
        {
            onSuccess?.Invoke();
        }
        else
        {
            onFailed?.Invoke();
        }
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
