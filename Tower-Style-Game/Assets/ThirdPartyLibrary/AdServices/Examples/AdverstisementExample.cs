using Library.Advertisement.UnityAd;
using UnityEngine;

// Works for both on iOS and Android platforms.
public class AdverstisementExample : MonoBehaviour
{

    private UnityRewardedVidedAd _skiplevelRewardedAds, _doublegoldRewardedAds;

    private UnityVideoAd _interstialVideoAds;

    private static string SKIP_LEVEL = "SKIP_LEVEL";
    private static string DOUBLE_GOLD = "DOUBLE_GOLD";

    void Awake()
    {
        _skiplevelRewardedAds = new UnityRewardedVidedAd(SKIP_LEVEL, 1);

        _doublegoldRewardedAds = new UnityRewardedVidedAd(DOUBLE_GOLD, 2);

        _interstialVideoAds = new UnityVideoAd();
    }

    private void Start()
    {
        _skiplevelRewardedAds.OnAdLoaded += OnRewardAdLoaded;
        _skiplevelRewardedAds.OnAdClosed += OnRewardedAdClosed;
        _skiplevelRewardedAds.OnAdOpened += OnRewardAdOpened;
        _skiplevelRewardedAds.OnAdFailedToLoad += OnRewardedAdFailedToLoad;
        _skiplevelRewardedAds.OnAdFailedToShow += OnRewardedAdFailedToShow;
        _skiplevelRewardedAds.OnUserEarnedReward += OnRewardedAdEarnedReward;
    }

    // Give reward to player.
    private void OnRewardedAdEarnedReward(string rewardType, int rewardAmount)
    {
        Debug.Log("OnRewardedAdEarnedReward! " + rewardType + " " + rewardAmount);
    }

    private void OnRewardedAdFailedToShow()
    {
        Debug.Log("OnRewardedAdFailedToShow!");
    }

    private void OnRewardedAdFailedToLoad()
    {
        Debug.Log("OnRewardedAdFailedToLoad!");
    }

    private void OnRewardAdOpened()
    {
        Debug.Log("OnRewardAdOpened!");
    }

    // Do not give reward to player.
    private void OnRewardedAdClosed()
    {
        Debug.Log("OnRewardedAdClosed!");
    }

    private void OnRewardAdLoaded()
    {
        Debug.Log("OnRewardAdLoaded!");
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("s"))
        {
            _skiplevelRewardedAds.LoadAndShowRewardedVideoAd();
        }
        if (Input.GetKey("a"))
        {
            _interstialVideoAds.LoadAndShowVideoAD();
        }

    }

}
