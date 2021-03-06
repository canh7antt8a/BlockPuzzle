﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardedVideoButton : MonoBehaviour
{
    private const string ACTION_NAME = "rewarded_video";

    private void Start()
    {
        Timer.Schedule(this, 0.1f, AddEvents);
    }

    private void AddEvents()
    {
    }

    public void OnClick()
    {
        if (IsAvailableToShow())
        {
            //TODO: ADD UNITY ADS CALL HERE
            //AdmobController.instance.ShowRewardBasedVideo();
        }
        else if (!IsActionAvailable())
        {
            int remainTime = (int)(ConfigController.Config.rewardedVideoPeriod - CUtils.GetActionDeltaTime(ACTION_NAME));

            int minute = remainTime / 60;
            int second = remainTime % 60;

            string m = minute == 1 ? "minute" : "minutes";
            string s = second == 1 ? "second" : "seconds";

            string m2 = minute == 1 ? "min" : "mins";
            string s2 = second == 1 ? "sec" : "secs";

            if (minute == 0)
                Toast.instance.ShowMessage("Please wait " + second + " " + s + " for the next ad");
            else if (second == 0)
                Toast.instance.ShowMessage("Please wait " + minute + " " + m + " for the next ad");
            else
                Toast.instance.ShowMessage("Please wait " + minute + " " + m2 + " " + second + " " + s2 + " for the next ad");
        }
        else
        {
            Toast.instance.ShowMessage("Ad is not available at the moment, please wait..");
        }
        
        Sound.instance.PlayButton();
    }
    
    public bool IsAvailableToShow()
    {
        return IsActionAvailable() && IsAdAvailable();
    }

    private bool IsActionAvailable()
    {
        return CUtils.IsActionAvailable(ACTION_NAME, ConfigController.Config.rewardedVideoPeriod);
    }

    private bool IsAdAvailable()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
            return false;
        return true;
        /*if (AdmobController.instance.rewardBasedVideo == null) return false;
        bool isLoaded = AdmobController.instance.rewardBasedVideo.IsLoaded();
        return isLoaded;*/
    }

    private void OnDestroy()
    {
    }
}
