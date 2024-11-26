using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : Singleton<AdManager>, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private string GAME_ID = "5737548"; 
    private const string GAME_ID_IOS = "5737549";

    private string BANNER_PLACEMENT = "Banner_Android";
    private const string BANNER_PLACEMENT_IOS = "Banner_iOS";

    private string VIDEO_PLACEMENT = "Interstitial_Android";
    private const string VIDEO_PLACEMENT_IOS = "Interstitial_iOS";
    
    [SerializeField] private BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;
    [SerializeField] private GameState gameSound;

    private bool testMode = true;
    
    public delegate void DebugEvent(string msg);
    public static event DebugEvent OnDebugLog;

    protected override void Awake()
    {
        base.Awake();

        Initialize();
    }

    public void Initialize()
    {
        if (Advertisement.isSupported)
        {
            DebugLog(Application.platform + " supported by Advertisement");
            if (Application.platform != RuntimePlatform.Android)
            {
                BANNER_PLACEMENT = BANNER_PLACEMENT_IOS;
                VIDEO_PLACEMENT = VIDEO_PLACEMENT_IOS;               
                GAME_ID = GAME_ID_IOS;
            }
            Advertisement.Initialize(GAME_ID, testMode, this);
        }
    }        

    public void ShowBannerAD()
    {
        Advertisement.Banner.SetPosition(bannerPosition);
        Advertisement.Banner.Show(BANNER_PLACEMENT);
    }

    public void HideBannerAD()
    {
        Advertisement.Banner.Hide(false);
    }

    public void LoadNonRewardedAd()
    {
        Advertisement.Load(VIDEO_PLACEMENT, this);
    }

    public void ShowNonRewardedAd()
    {
        Advertisement.Show(VIDEO_PLACEMENT, this);
    }

    #region Interface Implementations
    public void OnInitializationComplete()
    {
        DebugLog("Init Success");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        DebugLog($"Init Failed: [{error}]: {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        DebugLog($"Load Success: {placementId}");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        DebugLog($"Load Failed: [{error}:{placementId}] {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        DebugLog($"OnUnityAdsShowFailure: [{error}]: {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        gameSound.audioSource.Stop();
        GameManager.Instance.audioSource.Stop();
        AudioClipManager.Instance.StopSound();
        Time.timeScale = 0.0f;
        DebugLog($"OnUnityAdsShowStart: {placementId}");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        DebugLog($"OnUnityAdsShowClick: {placementId}");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        gameSound.audioSource.Play();
        GameManager.Instance.audioSource.Play();
        Time.timeScale = 1.0f;
        DebugLog($"OnUnityAdsShowComplete: [{showCompletionState}]: {placementId}");
    }
    #endregion

    //public void OnGameIDFieldChanged(string newInput)
    //{
    //    GAME_ID = newInput;
    //}

    public void ToggleTestMode(bool isOn)
    {
        testMode = isOn;
    }

    //wrapper around debug.log to allow broadcasting log strings to the UI
    void DebugLog(string msg)
    {
        OnDebugLog?.Invoke(msg);
        Debug.Log(msg);
    }
}
