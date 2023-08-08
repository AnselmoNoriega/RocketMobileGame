using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class Ads : MonoBehaviour, IUnityAdsShowListener, IUnityAdsInitializationListener, IUnityAdsLoadListener
{
    [SerializeField]
    private SpaceShip charcter;
    [SerializeField]
    private ScoreManager manager;
    [SerializeField]
    private Slider pos;
    [SerializeField]
    private GameObject adButton;

    private string _androidID = "5370182";
    private string _banner = "Banner_Android";
    private string _reward = "Rewarded_Android";
    private string _interstitial = "Interstitial_Android";

    private bool isBannerOn;
    private bool hasAdBeenShown;

    private float bannerTimer;
    private int tries;

    private void Awake()
    {
        Advertisement.Initialize(_androidID, false, this);
        isBannerOn = true;
        hasAdBeenShown = false;
        bannerTimer = 0;
        tries = 2;
    }

    private void Update()
    {
        ShowIntertitialAD();
        TimerForBanner();
    }

    private void ShowIntertitialAD()
    {
        if (Time.timeScale == 0 && !hasAdBeenShown)
        {
            Advertisement.Show(_interstitial, this);
            hasAdBeenShown = true;
        }
    }

    private void ContinueBannerAd()
    {
        if (isBannerOn)
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
        tries -= 1;

        if(tries <= 0)
        {
            adButton.SetActive(false);
        }
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
        if (placementId == _reward && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            for (int i = 0; i < charcter.asteroids.Length; i++)
            {
                charcter.asteroids[i].position = new Vector2(Random.Range(3.3f, 7.0f), Random.Range(-3.4f, 2.1f));
            }

            charcter.health = 3;
            Time.timeScale = 1;
            charcter.deathPanel.SetActive(false);
            pos.value = 50;
            charcter.dangerZoneTimer = 0;
            if (manager.sliderReductor > 25)
            {
                manager.sliderReductor -= 20;
            }
        }

        if (placementId == _reward)
        {
            Advertisement.Load(_reward, this);
        }
        else if (placementId == _interstitial)
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

        if (bannerTimer <= 0)
        {
            ContinueBannerAd();
            bannerTimer = 30;
        }
    }
}
