using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour, IUnityAdsShowListener, IUnityAdsInitializationListener, IUnityAdsLoadListener
{
    private string _androidID = "5370182";
    private string _banner = "Banner_Android";
    private string _reward = "Rewarded_Android";
    private string _interstitial = "Interstitial_Android";

    private bool isBannerOn;
    private bool hasAdBeenShown;

    private float bannerTimer;

    private void Awake()
    {
        Advertisement.Initialize(_androidID, false, this);
        isBannerOn = true;
        hasAdBeenShown = false;
        bannerTimer = 0;
    }

    private void Update()
    {
        ShowIntertitialAD();
        TimerForBanner();
    }

    private void ShowIntertitialAD()
    {
        if(Time.timeScale == 0 && !hasAdBeenShown)
        {
            Advertisement.Show(_interstitial, this);
            hasAdBeenShown = true;
        }
    }

    private void ContinueBannerAd()
    {
        if(isBannerOn)
        {
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            Advertisement.Banner.Show(_banner);
            isBannerOn = false;
        }
        else
        {
            Advertisement.Banner.Hide();
            isBannerOn = true;
        }
    }

    public void ShowRewardAd()
    {
        Advertisement.Show(_reward, this);
    }

    public void OnInitializationComplete()
    {
        Advertisement.Load(_banner, this);
        Advertisement.Load(_reward, this);
        Advertisement.Load(_interstitial, this);
        Debug.Log("ALL init");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Failed");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log(placementId + "Correctly loaded");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log(placementId + "failed for " + error + " " + message);
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("skipped");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if(placementId == _reward && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            //reward
        }

        if(placementId == _reward)
        {
            Advertisement.Load(_reward, this);
        }
        else if(placementId == _interstitial)
        {
            Advertisement.Load(_interstitial, this);
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log(placementId + "failed for " + error + " " + message);
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Showing " + placementId);
    }

    private void TimerForBanner()
    {
        bannerTimer -= Time.deltaTime;
        Debug.Log(bannerTimer);
        if(bannerTimer <= 0)
        {
            ContinueBannerAd();
            bannerTimer = 60;
        }
    }
}
