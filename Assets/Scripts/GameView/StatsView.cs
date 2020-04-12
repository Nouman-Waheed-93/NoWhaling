using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameView
{
    public class StatsView : RegionDetailsView
    {
        void Start()
        {
            base.Start();
            RegionHandler.instance.regionSlctdCB.AddListener(RegionSelected);
        }

        void RegionSelected()
        {
            if (RegionHandler.instance.SelectedRegion == null)
                return;
            ClearDetails();
            ShowStats(RegionHandler.instance.SelectedRegion.GetStats());
        }

        void ShowStats(RegionStat[] stats)
        {
            for (int i = 0; i < stats.Length; i++)
            {
                Transform stat = Instantiate(Resources.Load<Transform>("StatTemplate"), transform);
                stat.GetComponentInChildren<Text>().text = stats[i].StatName;
                stat.GetComponentInChildren<Slider>().value = stats[i].StatValue;
            }
        }
    }
}