using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameView
{
    public class LowBalanceGuide : MonoBehaviour
    {
        public Text remainingTimeTxt;
        public int FreeCoinsInterval;

        float RemainingTime;
        private void Start()
        {
            RemainingTime = FreeCoinsInterval - GlobalFunctions.GetRemainingTime("FreeCoins");
        }

        private void Update()
        {
            if (RemainingTime <= 0)
            {
                MainMenu.instance.HideBanned();
                MngmntGameHandler.instance.Tasso.TakeProfit(1000);
                GlobalFunctions.SetLastTime("FreeCoins");
                RemainingTime = FreeCoinsInterval - GlobalFunctions.GetRemainingTime("FreeCoins");
            }
            else
            {
                RemainingTime -= Time.deltaTime;
                System.TimeSpan remainingTimeSpan = System.TimeSpan.FromSeconds(RemainingTime);
                remainingTimeTxt.text = remainingTimeSpan.Hours + ":" + remainingTimeSpan.Minutes + ":" + remainingTimeSpan.Seconds;
            }
        }
    }
}