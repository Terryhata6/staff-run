using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronSourceSdkINIT : MonoBehaviour
{
    private static IronSourceSdkINIT current;
    public IronSourceSdkINIT Current => current;
    private string _appKey;


    public void Awake()
    {
#if UNITY_ANDROID
        _appKey = "f046e829";
#elif UNITY_IPHONE
        _appKey = "f046e829";
#else
        _appKey = "unexpected_platform";
#endif
        current = this;
        DontDestroyOnLoad(this.gameObject);
        IronSource.Agent.validateIntegration();
        // banner suda
        IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;
        IronSourceEvents.onBannerAdLoadFailedEvent += BannerAdLoadFailedEvent;
        IronSourceEvents.onBannerAdClickedEvent += BannerAdClickedEvent;
        IronSourceEvents.onBannerAdScreenPresentedEvent += BannerAdScreenPresentedEvent;
        IronSourceEvents.onBannerAdScreenDismissedEvent += BannerAdScreenDismissedEvent;
        IronSourceEvents.onBannerAdLeftApplicationEvent += BannerAdLeftApplicationEvent;
        //

        IronSource.Agent.init(_appKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.OFFERWALL, IronSourceAdUnits.BANNER);
        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
    }

    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }

	void BannerAdLoadedEvent()
	{
		//Debug.Log("unity-script: I got BannerAdLoadedEvent");
	}

	void BannerAdLoadFailedEvent(IronSourceError error)
	{
		//Debug.Log("unity-script: I got BannerAdLoadFailedEvent, code: " + error.getCode() + ", description : " + error.getDescription());
	}

	void BannerAdClickedEvent()
	{
		//Debug.Log("unity-script: I got BannerAdClickedEvent");
	}

	void BannerAdScreenPresentedEvent()
	{
		//Debug.Log("unity-script: I got BannerAdScreenPresentedEvent");
	}

	void BannerAdScreenDismissedEvent()
	{
		//Debug.Log("unity-script: I got BannerAdScreenDismissedEvent");
	}

	void BannerAdLeftApplicationEvent()
	{
		//Debug.Log("unity-script: I got BannerAdLeftApplicationEvent");
	}
}
