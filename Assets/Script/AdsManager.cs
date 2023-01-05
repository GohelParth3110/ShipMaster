using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
	public static AdsManager instance;

	public bool isFirstTime = true;

	private BannerView bannerView;	
	public InterstitialAd interstitial;
	private RewardedAd reward;

	private bool shouldBeRewarded = false;
	private bool hasBeenRewarded = false;

	public string str_BannerID;
	public string str_InterstitialID;
	public string str_RewardID;
	public bool isTestMode;
	

	private void Awake()
	{
		DontDestroyOnLoad(this);

		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);
		}

		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
		instance = this;

		Application.targetFrameRate = 60;
	}

	private void Start()
	{
		RequestRewardVideo();
	}

	public void CallDefaultAds()
    {
		RequestBanner();
		RequestInterstitial();
	}

	public void RequestBanner()
	{
		// These ad units are configured to always serve test ads.

		string adUnitId = "";

		if (!isTestMode)
		{
			adUnitId = str_BannerID;
		}
		else
		{
			adUnitId = "ca-app-pub-3940256099942544/6300978111";
		}

        AdSize adaptiveSize =
               AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);


        this.bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);

		// Register for ad events.
		this.bannerView.OnAdLoaded += this.HandleAdLoaded;
		this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
		this.bannerView.OnAdOpening += this.HandleAdOpened;
		this.bannerView.OnAdClosed += this.HandleAdClosed;

		// Load a banner ad.
		this.bannerView.LoadAd(this.CreateAdRequest());
	}

	public void HandleAdLoaded(object sender, EventArgs args)
	{
		
	}

	public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{

	}

	public void HandleAdOpened(object sender, EventArgs args)
	{
		
	}

	public void HandleAdClosed(object sender, EventArgs args)
	{
		
	}



	// Returns an ad request with custom ad targeting.
	

	public void PurchasedNoAds()
	{
		PlayerPrefs.SetInt("NoAds", 1);
		//GameObject.Find("Btn-RemoveAds").SetActive(false);

		if (bannerView != null)
		{
			bannerView.Hide();
			bannerView.Destroy();
		}
	}

	private void RequestInterstitial()
	{
		// These ad units are configured to always serve test ads.

		string adUnitId = "";

		if (!isTestMode)
		{
			adUnitId = str_InterstitialID;
		}
		else
		{
			adUnitId = "ca-app-pub-3940256099942544/1033173712";
		}


		// Create an interstitial.
		this.interstitial = new InterstitialAd(adUnitId);

		// Register for ad events.
		this.interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
		this.interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
		this.interstitial.OnAdOpening += this.HandleInterstitialOpened;
		this.interstitial.OnAdClosed += this.HandleInterstitialClosed;
		

		// Load an interstitial ad.
		this.interstitial.LoadAd(this.CreateAdRequest());
	}

	public void HandleInterstitialLoaded(object sender, EventArgs args)
	{

	}

	public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
	
		
	}

	public void HandleInterstitialOpened(object sender, EventArgs args)
	{
		
	}

	public void HandleInterstitialClosed(object sender, EventArgs args)
	{
		RequestInterstitial();
	}

	public void HandleInterstitialLeftApplication(object sender, EventArgs args)
	{
		print("HandleInterstitialLeftApplication event received");
	}

	// Returns an ad request with custom ad targeting.
	private AdRequest CreateAdRequest()
	{
		return new AdRequest.Builder()
				.AddKeyword("game")
				.AddExtra("color_bg", "9B30FF")
				.Build();
	}

	public void ShowInterstitialAd()
	{
		Debug.Log("Show ShowInterstitialAd ad");

		if (DataManager.Instance.hasPurchasedNoAds)
        {
			return;
        }

		if (interstitial != null && interstitial.IsLoaded())
		{
			interstitial.Show();

		}
		else
		{
			RequestInterstitial();
		}
	}

	private void RequestRewardVideo()
	{
		string adUnitId = "";

		if (!isTestMode)
		{
			adUnitId = str_RewardID;
		}
		else
		{
			adUnitId = "ca-app-pub-3940256099942544/5224354917";
		}

		reward = new RewardedAd(adUnitId);
		
		// Called when an ad request has successfully loaded.
		reward.OnAdLoaded += HandleRewardBasedVideoLoaded;
		// Called when an ad request failed to load.
		reward.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
		// Called when an ad is shown.
		reward.OnAdOpening += HandleRewardBasedVideoOpened;
		// Called when the ad starts to play.
	
		// Called when the user should be rewarded for watching a video.
		reward.OnUserEarnedReward += HandleRewardBasedVideoRewarded;
		// Called when the ad is closed.
		reward.OnAdClosed += HandleRewardBasedVideoClosed;

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the rewarded video ad with the request.
		this.reward.LoadAd(request);
	}

	public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
	{
		
	}

	public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		
		
	}

	public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
	{
		
	}

	public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
	{
		
	}

	public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
	{


		if (shouldBeRewarded)
        {

	
			

			if (!hasBeenRewarded)
            {
				hasBeenRewarded = true;


             
                if (GameManager.InstanceOfGameManager.panel_GameOver.activeSelf)
                {
                   
					//GameManager.InstanceOfGameManager.CoinCollected(GameManager.InstanceOfGameManager.
					//                coinCollectedInThisRound);
					GameManager.InstanceOfGameManager.CoinCollected(100);
					GameManager.InstanceOfGameManager.panel_GameOver.GetComponent<UiGameOver>().CallingCourtine();
                    
                }
                else
                {

					GameManager.InstanceOfGameManager.ui_Gameplay.PlayCurrentGame();

					//uiGamePlay.GetComponent<UiGamePlay>().PlayCurrentGame();
				}
            }
        }
        else
        {
			if (!GameManager.InstanceOfGameManager.panel_GameOver.activeSelf)
            {
				GameManager.InstanceOfGameManager.PlayerGameOver();
			}
				
		}


        RequestRewardVideo();
	}



	public void HandleRewardBasedVideoRewarded(object sender, Reward args)
	{
		
		shouldBeRewarded = true;
		
	}


	public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
	{
	
	}

	public void ShowRewardAd()
	{

		
		shouldBeRewarded = false;
		hasBeenRewarded = false;
	
		reward.Show();
	}

	public bool IsRewardLoaded()
    {
        if (reward.IsLoaded())
        {
			return true;
        }

		return false;
    }


	public void NoAdPurchasedCompleted()
    {
		if(bannerView != null)
        {
			bannerView.Hide();
			bannerView.Destroy();
        }
    }
	
}
