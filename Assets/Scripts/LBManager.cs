/*
 * Copyright (C) 2014 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
 
    using UnityEngine;
    using System.Collections.Generic;
    using GooglePlayGames;
    using GooglePlayGames.BasicApi.SavedGame;
    using System;
    using GooglePlayGames.BasicApi;

    public class LBManager
    {
        private static LBManager sInstance = new LBManager();
        private int mLevel = 0;

    
        private bool mAuthenticating = false;
        
        // list of achievements we know we have unlocked (to avoid making repeated calls to the API)
      //  private Dictionary<string, bool> mUnlockedAchievements = new Dictionary<string, bool>();

        // achievement increments we are accumulating locally, waiting to send to the games API
     //   private Dictionary<string, int> mPendingIncrements = new Dictionary<string, int>();

        // what is the highest score we have posted to the leaderboard?
     //   private int mHighestPostedScore = 0;
     
        public static LBManager Instance
        {
            get
            {
                return sInstance;
            }
        }
        
        public void ReportAllProgress()
        {
            //     PostToLeaderboard();
            PostScoreToDJINDamaged();
            PostScoreToWhalesLost();
            PostScoreToWhalesSaved();
    }
        
        public void Authenticate()
        {
            if (Authenticated || mAuthenticating)
            {
                Debug.LogWarning("Ignoring repeated call to Authenticate().");
                return;
            }

            // Enable/disable logs on the PlayGamesPlatform
        //    PlayGamesPlatform.DebugLogEnabled = GameConsts.PlayGamesDebugLogsEnabled;

            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
                .Build();
            PlayGamesPlatform.InitializeInstance(config);

            // Activate the Play Games platform. This will make it the default
            // implementation of Social.Active
            PlayGamesPlatform.Activate();

            // Set the default leaderboard for the leaderboards UI
           // ((PlayGamesPlatform)Social.Active).SetDefaultLeaderboardForUI(GPGSIds.leaderboard_animals_saved);

            // Sign in to Google Play Games
            mAuthenticating = true;
            Social.localUser.Authenticate((bool success) =>
                {
                    mAuthenticating = false;
                    if (success)
                    {
                        // if we signed in successfully, load data from cloud
                        Debug.Log("Login successful!");
                    }
                    else
                    {
                        // no need to show error message (error messages are shown automatically
                        // by plugin)
                        Debug.LogWarning("Failed to sign in with Google Play Games.");
                    }
                });
        }
        
        public bool Authenticating
        {
            get
            {
                return mAuthenticating;
            }
        }

        public bool Authenticated
        {
            get
            {
                return Social.Active.localUser.authenticated;
            }
        }

        public void SignOut()
        {
            ((PlayGamesPlatform)Social.Active).SignOut();
        }

        //public string AuthProgressMessage
        //{
        //    get
        //    {
        //        return mAuthProgressMessage;
        //    }
        //}

        public void ShowLeaderboardUI()
        {
            if (Authenticated)
            {
                Social.ShowLeaderboardUI();
            }
        }
        
        public void PostScoreToWhalesSaved() {
            if (Authenticated)
            {
                // post score to the leaderboard
                Social.ReportScore(GlobalFunctions.GetWhalesSaved(), GPGSIds.leaderboard_whales_saved, (bool success) =>
                {
                });
            }
            else
            {
                Debug.LogWarning("Not reporting score, auth not");
            }
        }

        public void PostScoreToDJINDamaged() {
            if (Authenticated)
            {
                // post score to the leaderboard
                Social.ReportScore(GlobalFunctions.GetDJINDamage(), GPGSIds.leaderboard_damage_done_to_djin, (bool success) =>
                {
                });
            }
            else
            {
                Debug.LogWarning("Not reporting score, auth not");
            }
        }

        public void PostScoreToWhalesLost() {
            if (Authenticated)
            {
                // post score to the leaderboard
                Social.ReportScore(GlobalFunctions.GetWhalesLost(), GPGSIds.leaderboard_whales_lost, (bool success) =>
                {
                });
            }
            else
            {
                Debug.LogWarning("Not reporting score, auth not");
            }
        }

    public void PostScoreToDJINBanned()
    {
        if (Authenticated)
        {
            // post score to the leaderboard
            Social.ReportScore(GlobalFunctions.GetDJINBanTimeScore() , GPGSIds.leaderboard_djin_banned, (bool success) =>
            {
            });
        }
        else
        {
            Debug.LogWarning("Not reporting score, auth not");
        }
    }

}
