using System.Collections;
using UnityEngine;

public class Interstitial : MonoBehaviour
{
    public static Interstitial current;

    public void Awake()
    {
        current = this;
        IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;
    }

	public void LoadInterstitial()
	{		
		IronSource.Agent.loadInterstitial();
	}

	public void ShowInterstitial()
	{		
		if (IronSource.Agent.isInterstitialReady())
		{
			IronSource.Agent.showInterstitial();
		}
		else
		{
			LoadInterstitial();
		}
	}

	void InterstitialAdReadyEvent()
	{
		Debug.Log("unity-script: I got InterstitialAdReadyEvent");		
	}

	void InterstitialAdLoadFailedEvent(IronSourceError error)
	{
		Debug.Log("unity-script: I got InterstitialAdLoadFailedEvent, code: " + error.getCode() + ", description : " + error.getDescription());
	}

	void InterstitialAdShowSucceededEvent()
	{
		Debug.Log("unity-script: I got InterstitialAdShowSucceededEvent");		
	}

	void InterstitialAdShowFailedEvent(IronSourceError error)
	{
		Debug.Log("unity-script: I got InterstitialAdShowFailedEvent, code :  " + error.getCode() + ", description : " + error.getDescription());		
	}

	void InterstitialAdClickedEvent()
	{
		Debug.Log("unity-script: I got InterstitialAdClickedEvent");
	}

	void InterstitialAdOpenedEvent()
	{
		Debug.Log("unity-script: I got InterstitialAdOpenedEvent");
	}

	void InterstitialAdClosedEvent()
	{
		Debug.Log("unity-script: I got InterstitialAdClosedEvent");
	}
}