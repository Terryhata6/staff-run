using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronSourceSdkINIT : MonoBehaviour
{
    private static IronSourceSdkINIT current;
    public IronSourceSdkINIT Current => current;

    public void Awake()
    {
        current = this;
        DontDestroyOnLoad(this.gameObject);
        IronSource.Agent.init("f046e829", IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.OFFERWALL, IronSourceAdUnits.BANNER);
        IronSource.Agent.validateIntegration();
    }

    void Update()
    {
        
    }


    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }
}
