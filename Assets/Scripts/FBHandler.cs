using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;
using System.IO;
using System;

public class FBHandler : MonoBehaviour {

    private static readonly List<string> readPermissions = new List<string> { "public_profile", "user_friends" };
    private static readonly List<string> publishPermissions = new List<string> { "publish_actions" };

    public static FBHandler instance;

    public int sharingReward = 200;
    
    public Button ShareButton;
    
    // Use this for initialization
    long nextFBShareTimeAllow; //time after which the player would be allowed to share the game on fb

    void Awake () {
        if (instance)
            Destroy(gameObject);
        else
            instance = this;
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
        nextFBShareTimeAllow = long.Parse(PlayerPrefs.GetString("nextFBShareTime", "0"));
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }
    
    bool DayCheck()
    {
        if (!PlayerPrefs.HasKey("PlayDate"))
            return true;
        string stringDate = PlayerPrefs.GetString("PlayDate");
        DateTime oldDate = Convert.ToDateTime(stringDate);
        DateTime newDate = DateTime.Now;

        TimeSpan difference = newDate.Subtract(oldDate);
        return difference.Days >= 1;
    }
    
    public void OnShareClicked() {
        if (DayCheck())
        {
            FB.FeedShare("", new Uri("https://play.google.com/store/apps/details?id=com.tasso.NoWhaling"), "Save the Whales", "Whales are being brutally hunted, Stop it!",
                "Became an Anti-Whaling activist, Save the Whales.", null, "", SharingDone);
        }
        else {
            FBMsg.instance.ShowUp();
        }
    }
    
    void SharingDone(IShareResult result) {
        if (result.Cancelled || result.Error != null)
            return;
        MngmntGameHandler.instance.Tasso.TakeProfit((uint)sharingReward);
        DateTime newDate = DateTime.Now;
        string newStringDate = Convert.ToString(newDate);
        PlayerPrefs.SetString("PlayDate", newStringDate);
 
    }

}
