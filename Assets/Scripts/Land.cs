using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Land : Region {

    public int ambassadorSalary;
    public RegionStat consumers = new RegionStat(LocalizationManager.instance.GetLocalizedValue("Consumers"), 0);
    public UnityEvent operationStarted = new UnityEvent();
    public uint branchCost;
    public OperationVar fundRaising;
    public OperationVar awarenessCmpn;
    public OperationVar protest;
  
    public float remainingSeconds;

    public TassoOperation currOperation;

    static string fundsString = LocalizationManager.instance.GetLocalizedValue("Funds");
    static string awarenessString = LocalizationManager.instance.GetLocalizedValue("Awareness");
    static string protestString = LocalizationManager.instance.GetLocalizedValue("Protest");

    [SerializeField]
    private int startConsumerAmt, startAgnstWhling, startIgnrntPpl;
    //RegionStat populationAgainstWhaling = new RegionStat("People Against Whaling", 0);
    //RegionStat populationIgnorant = new RegionStat("Ignorant People", 0);
    //RegionStat pressureOnGovt = new RegionStat("Pressure On Govt.", 0);
    
    public override void SelectRegion()
    {
        base.SelectRegion();
    }

    public override void BuyAsset()
    {
        if (MngmntGameHandler.instance.Tasso.bankBalance >= branchCost)
        {
            base.BuyAsset();
            MngmntGameHandler.instance.Tasso.TakeLoss(branchCost);
        }
        else
            BalMessage.instance.ShowUp();
    }

    public override RegionAction[] GetOptions()
    {
        if (tassoPresence)
        {
            if (remainingSeconds > 0)
                return null;
            else
                return actions.ToArray();
        }
        else
            return new RegionAction[] { buyAssetAction };
    }

    void BanWhaleProducts() {

    }
    
    public override void CreateAssetAction()
    {
        buyAssetAction = new RegionAction(LocalizationManager.instance.GetLocalizedValue("Hire Ambassador"), (int)branchCost, BuyAsset);
    }

    void Start()
    {
        base.Start();
        
        if (JWHPresence)
            ((JWH)MngmntGameHandler.instance.jwh).RegisterMarket(this);
        
        consumers.StatValue = PlayerPrefs.GetInt("Consumers" + transform.GetSiblingIndex(), startConsumerAmt);
        stats = new RegionStat[4];
        stats[0] = consumers;
        stats[1] = new RegionStat(LocalizationManager.instance.GetLocalizedValue("People Against Whaling"), PlayerPrefs.GetInt("Against" + transform.GetSiblingIndex(), startAgnstWhling));
        stats[2] = new RegionStat(LocalizationManager.instance.GetLocalizedValue("Ignorant People"), PlayerPrefs.GetInt("Ignorant" + transform.GetSiblingIndex(), startIgnrntPpl));
        stats[3] = new RegionStat(LocalizationManager.instance.GetLocalizedValue("Pressure On Govt."), PlayerPrefs.GetInt("Pressure" + transform.GetSiblingIndex(), 0));
        
        actions.Add(new RegionAction(LocalizationManager.instance.GetLocalizedValue("Raise Funds"), fundRaising.price, RaiseFunds));
        actions.Add(new RegionAction(LocalizationManager.instance.GetLocalizedValue("Awareness Campaign"), awarenessCmpn.price, StartAwarenessCampaign));
        if (!JWHDemolished)
            actions.Add(new RegionAction(LocalizationManager.instance.GetLocalizedValue("Call Protest"), protest.price, CallProtest));
        else
            GlobalVals.totalLandsDemolished++;
        
        {
            string TlastOpName = GlobalFunctions.GetLastOperation(transform.GetSiblingIndex());

            if(TlastOpName == fundsString)
            {
                    currOperation.name = fundsString;
                    remainingSeconds = Mathf.Clamp(
                        (int)(fundRaising.time - GlobalFunctions.GetTassoOpTime(transform.GetSiblingIndex()))
                        , 1
                        , int.MaxValue);
                    currOperation.operationTime = fundRaising.time;
            }
            else if(TlastOpName == awarenessString)
            {
                    currOperation.name = awarenessString;
                    remainingSeconds = Mathf.Clamp(
                        (int)(awarenessCmpn.time - GlobalFunctions.GetTassoOpTime(transform.GetSiblingIndex()))
                        , 1
                        , int.MaxValue);
                    currOperation.operationTime = awarenessCmpn.time;
            }
            else if(TlastOpName == protestString)
            {
                    currOperation.name = protestString;
                    remainingSeconds = Mathf.Clamp(
                        (int)(protest.time - GlobalFunctions.GetTassoOpTime(transform.GetSiblingIndex()))
                        , 1
                        , int.MaxValue);
                    currOperation.operationTime = protest.time;
            }
            else
            {
                    currOperation.name = "";
                    remainingSeconds = 0;
            }
        }

        if (currOperation.name != "")
        {
            StartCoroutine(currOperation.name);
        }
    }

    public void CallProtest()
    {
        if (MngmntGameHandler.instance.Tasso.bankBalance >= protest.price)
        {
            remainingSeconds = protest.time;
            currOperation.operationTime = protest.time;
            StartOperation("Protest");
            MngmntGameHandler.instance.Tasso.TakeLoss((uint)protest.price);
        }
        else
            BalMessage.instance.ShowUp();
    }

    public void StartAwarenessCampaign()
    {
        if (MngmntGameHandler.instance.Tasso.bankBalance >= awarenessCmpn.price)
        {
            remainingSeconds = awarenessCmpn.time;
            currOperation.operationTime = awarenessCmpn.time;
            StartOperation("Awareness");
            MngmntGameHandler.instance.Tasso.TakeLoss((uint)awarenessCmpn.price);
        }
        else
            BalMessage.instance.ShowUp();
    }

    public void RaiseFunds()
    {
        if (MngmntGameHandler.instance.Tasso.bankBalance >= fundRaising.price)
        {
            remainingSeconds = fundRaising.time;
            currOperation.operationTime = fundRaising.time;
            StartOperation("Funds");
            MngmntGameHandler.instance.Tasso.TakeLoss((uint)fundRaising.price);
        }
        else
            BalMessage.instance.ShowUp();
    }
    
    void StartOperation(string name)
    {
        currOperation.name = LocalizationManager.instance.GetLocalizedValue(name);
        GlobalFunctions.SetLastOperation(name, transform.GetSiblingIndex());
        StartCoroutine(name);
        if (operationStarted != null)
            operationStarted.Invoke();
    }

    IEnumerator Funds() {
        while(remainingSeconds > 0)
        {
            yield return null;
            remainingSeconds -= Time.deltaTime;
        }
        //Add Funds to tasso balance
        uint profit = (uint)(10 * startAgnstWhling * branchCost * 0.003f);
        ScoreLayer.instance.CreateScore(transform.position, fundsString, "+" + profit, Color.green, Vector3.up*15, ScoreDeltaText.ScoreDirection.Up);
        MngmntGameHandler.instance.Tasso.TakeProfit(profit);
        GlobalFunctions.SetLastOperation("", transform.GetSiblingIndex());
    }

    IEnumerator Awareness()
    {
        while(remainingSeconds > 0)
        {
            yield return null;
            remainingSeconds -= Time.deltaTime;
        }
        //Add in people against whaling and lessen ignorant people
        uint Ignrntadv = (uint)(stats[2].StatValue * Random.Range(0.2f, 0.5f));
        uint consumeradv = (uint)(consumers.StatValue * Random.Range(0.1f, 0.2f));
        stats[1].StatValue += (int)Ignrntadv + (int)consumeradv;
        PlayerPrefs.SetInt("Against" + transform.GetSiblingIndex(), stats[1].StatValue);
        stats[2].StatValue -= (int)Ignrntadv;
        PlayerPrefs.SetInt("Ignorant" + transform.GetSiblingIndex(), stats[2].StatValue);
        stats[0].StatValue -= (int)consumeradv;
        PlayerPrefs.SetInt("Consumers" + transform.GetSiblingIndex(), stats[0].StatValue);
        ScoreLayer.instance.CreateScore(transform.position, awarenessString, "+" + Ignrntadv, Color.green, Vector3.up * 15, ScoreDeltaText.ScoreDirection.Up);
        RegionHandler.instance.regionSlctdCB.Invoke();
        GlobalFunctions.SetLastOperation("", transform.GetSiblingIndex());
    }

    IEnumerator Protest()
    {
        while(remainingSeconds > 0)
        {
            yield return null;
            remainingSeconds -= Time.deltaTime;
        }
        //Increase pressure on government with respect to people against whaling .... 
        //More than 50 percent people protests would be noticably effective
        int deltaPressure = (int)(stats[1].StatValue * Random.Range(0.5f, 0.8f));
        stats[3].StatValue += deltaPressure;
        if(stats[3].StatValue >= 100)
        {
            DemolishJWH();
            actions.RemoveAt(2);
            GlobalVals.totalLandsDemolished++;
            PlayerPrefs.SetInt("Gems", PlayerPrefs.GetInt("Gems") + protest.price);
            GlobalFunctions.AddTotalDJINBanScore(protest.price);
            LBManager.Instance.PostScoreToDJINBanned();
            GameView.MainMenu.instance.ShowBanned();
        }
        PlayerPrefs.SetInt("Pressure" + transform.GetSiblingIndex(), stats[3].StatValue);
        ScoreLayer.instance.CreateScore(transform.position, LocalizationManager.instance.GetLocalizedValue("Pressure"), "+" + deltaPressure, Color.green, Vector3.up * 15, ScoreDeltaText.ScoreDirection.Up);
        RegionHandler.instance.regionSlctdCB.Invoke();
        GlobalFunctions.SetLastOperation("", transform.GetSiblingIndex());
    }

    public void CancelOperations()
    {
        StopCoroutine(currOperation.name);
        GlobalFunctions.SetLastOperation("", transform.GetSiblingIndex());
        remainingSeconds = 0;
    }

    public void StartJWHMarketing()
    {
        uint Ignrntadv = (uint)(stats[2].StatValue * Random.Range(0.01f, 0.02f));
      //  uint againstadv = (uint)(stats[1].StatValue * Random.Range(0.01f, 0.05f));
        //stats[1].StatValue -= (int)againstadv;
       // PlayerPrefs.SetInt("Against" + transform.GetSiblingIndex(), stats[1].StatValue);
        stats[2].StatValue -= (int)Ignrntadv;
        stats[2].StatValue = Mathf.Clamp(stats[2].StatValue, 0, 100);
        PlayerPrefs.SetInt("Ignorant" + transform.GetSiblingIndex(), stats[2].StatValue);
        stats[0].StatValue += (int)Ignrntadv;
        stats[0].StatValue = Mathf.Clamp(stats[0].StatValue, 0, 100);
        PlayerPrefs.SetInt("Consumers" + transform.GetSiblingIndex(), stats[0].StatValue);
        ScoreLayer.instance.CreateScore(transform.position, LocalizationManager.instance.GetLocalizedValue(awarenessString), "-" + Ignrntadv, Color.red, -Vector3.up * 15, ScoreDeltaText.ScoreDirection.Down);
    }
    
}

public struct TassoOperation
{

    public string name;
    public int operationTime;

}
