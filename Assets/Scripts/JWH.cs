using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JWH : Company {
    [System.Serializable]
    public struct OperationVar{
        public float Interval;
        public float remainingTime;
    }

    public OperationVar newBranchCreation;
    public OperationVar newWhlingSpot;
    public OperationVar fishing;
    public OperationVar marketing;
    public OperationVar monthTick;
    public int ProductsPerWhale;

    int whaleReserves = 500;
  
    int currWhalingZoneInd;
    
    List<Ocean> whalingZones = new List<Ocean>();

    private void Awake()
    {
        base.Awake();
        fishing.remainingTime = Mathf.Clamp(fishing.Interval - GlobalFunctions.GetRemainingFishingTime(), 1, int.MaxValue);
        marketing.remainingTime = marketing.Interval - GlobalFunctions.GetRemainingMarketingTime();
        newBranchCreation.remainingTime = Mathf.Clamp(newBranchCreation.Interval - GlobalFunctions.GetRemainingBranchTime(), 1, int.MaxValue );
        newWhlingSpot.remainingTime = Mathf.Clamp(newWhlingSpot.Interval - GlobalFunctions.GetRemainingNewSpotTime(), 1, int.MaxValue);
        monthTick.remainingTime = monthTick.Interval - GlobalFunctions.GetRemainingMnthTime();
        whaleReserves = GlobalFunctions.GetWhaleReserves();
        IncreaseProductReserves(InFieldProfitLoss.GetWhalesHunted());
    }

    public void IncreaseProductReserves(int increment)
    {
        Debug.Log("Reserves before addition " + whaleReserves);
        increment *= ProductsPerWhale;
        whaleReserves += increment;
        Debug.Log("Reserves after addition " + whaleReserves);
        GlobalFunctions.SetWhaleReserves(whaleReserves);
    }

    public void RegisterMarket(Land land)
    {
        branches.Add(land);
    }

    public void RegisterWhalingSpot(Ocean spot)
    {
        whalingZones.Add(spot);
    }

    void CalculateMonthlyProfit()
    {
        for(int i = 0; i < branches.Count; i++)
        {
            if (whaleReserves >= branches[i].consumers.StatValue)
            {
                if (!branches[i].JWHDemolished)
                {
                    Debug.Log("Balance before " + bankBalance);
                    whaleReserves -= branches[i].consumers.StatValue;
                    TakeProfit((uint)(branches[i].consumers.StatValue * branches[i].branchCost * 0.005f));
                }
            }
            else
            {
                Debug.Log("Balance before " + bankBalance);
                TakeLoss((uint)(branches[i].branchCost * 0.1f));
                Debug.Log("Loss hoya");
                Debug.Log("Balance after " + bankBalance);
            }
        }
        GlobalFunctions.SetLastMnthTime();
    }

    void MakeNewBranch() {
        Land[] allCountries = FindObjectsOfType<Land>();

        int i = Random.Range(0, allCountries.Length);
        
        Debug.Log("bank bal " + bankBalance);
        if (!branches.Contains(allCountries[i]) && allCountries[i].consumers.StatValue <= whaleReserves 
                && allCountries[i].branchCost <= bankBalance/3)
        {
            Debug.Log("Branch ceaddd");
            allCountries[i].EstablishJWH();
            branches.Add(allCountries[i]);
            TakeLoss(allCountries[i].branchCost);
            GlobalFunctions.SetLastBrnchCreationTime();
            newBranchCreation.remainingTime = newBranchCreation.Interval;
        }
    }

    void FindNewWhalingSpot()
    {
        Ocean[] allCountries = FindObjectsOfType<Ocean>();
        int i = PlayerPrefs.HasKey("Tutorial1")? Random.Range(0, allCountries.Length) : 0;

        if (!whalingZones.Contains(allCountries[i]) && allCountries[i].shipPrice <= bankBalance / 3)
        {
            allCountries[i].EstablishJWH();
            whalingZones.Add(allCountries[i]);
            TakeLoss((uint)allCountries[i].shipPrice);
            GlobalFunctions.SetLastNewSpotTime();
            newWhlingSpot.remainingTime = newWhlingSpot.Interval;
        }
    }

    private void Update()
    {
        fishing.remainingTime -= Time.deltaTime;
        newBranchCreation.remainingTime -= Time.deltaTime;
        marketing.remainingTime -= Time.deltaTime;
        newWhlingSpot.remainingTime -= Time.deltaTime;
        monthTick.remainingTime -= Time.deltaTime;
        Debug.Log("Whaling ind zone " + currWhalingZoneInd);
        if(fishing.remainingTime < 0 && currWhalingZoneInd < whalingZones.Count)
        {
            if (whalingZones[currWhalingZoneInd].StartWhaling())
            {
                fishing.remainingTime = fishing.Interval;
                GlobalFunctions.SetLastFishingTime();
                for (int i = 0; i < whalingZones.Count; i++)
                {
                    if (!whalingZones[i].JWHDemolished)
                        IncreaseProductReserves(10);
                }
            }
            currWhalingZoneInd++;
            if (currWhalingZoneInd >= whalingZones.Count)
            {
                currWhalingZoneInd = 0;
            }
        }
        if(newBranchCreation.remainingTime < 0)
        {
            MakeNewBranch();
        }
        if(newWhlingSpot.remainingTime < 0)
        {
            FindNewWhalingSpot();
        }
        if(marketing.remainingTime < 0)
        {
            for (int i = 0; i < branches.Count; i++)
            {
                if (branches[i].JWHDemolished)
                    continue;
           //     branches[i].StartJWHMarketing();
                GlobalFunctions.SetLastMarketingTime();
                marketing.remainingTime = marketing.Interval;
            }
        }
        if(monthTick.remainingTime < 0)
        {
            CalculateMonthlyProfit();
            monthTick.remainingTime = monthTick.Interval;
        }
    }
}
