using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ocean : Region {
    public int shipPrice;

    [SerializeField]
    int WhalePopulation;

    GameObject whalingIndicator;

    public override void BuyAsset()
    {
        if (MngmntGameHandler.instance.Tasso.bankBalance >= shipPrice)
        {
            base.BuyAsset();
            MngmntGameHandler.instance.Tasso.TakeLoss((uint)shipPrice);
        }
        else
            BalMessage.instance.ShowUp();
    }
    
    public void RehabWhales()
    {

    }

    public override void SelectRegion()
    {
        base.SelectRegion();
    }

    public override void CreateAssetAction()
    {
        buyAssetAction = new RegionAction(LocalizationManager.instance.GetLocalizedValue("Buy Ship"), shipPrice, BuyAsset);
    }

    public override RegionAction[] GetOptions()
    {
        if (tassoPresence)
        {
            if (whalingIndicator)
                return actions.ToArray();
            else
                return null;
        }
        else
            return new RegionAction[] { buyAssetAction };
    }


    public void TakeControl() {
        if (whalingIndicator)
        {
            if (MngmntGameHandler.instance.Tasso.bankBalance >= 50)
            {
                MngmntGameHandler.instance.Tasso.TakeLoss(50);
                int lvl = GlobalFunctions.GetProgress();
                if (lvl > SceneManager.sceneCountInBuildSettings - GlobalVals.totalNonOceanScenes)
                {
                    lvl = Random.Range(1, SceneManager.sceneCountInBuildSettings - GlobalVals.totalNonOceanScenes + 1);
                }
                GlobalVals.LastWhaledOcean = transform.GetSiblingIndex();
                SceneManager.LoadScene("Ocean " + lvl);
            }
            else
                BalMessage.instance.ShowUp();
        }
        else
            RegionHandler.instance.regionSlctdCB.Invoke();
    }

    public bool StartWhaling()
    {
        if (!JWHDemolished && WhalePopulation > 1 && whalingIndicator == null)
        {
            whalingIndicator = ScoreLayer.instance.CreateWhalingIndctor(transform.position);
            StartCoroutine("Whaling");
            return true;
        }
        else
            return false;
    }

    IEnumerator Whaling()
    {
        float timeElapsed = 0;
        while(timeElapsed < 30)
        {
            yield return null;
            timeElapsed += Time.deltaTime;
        }
        int HuntedAmnt = (int)(Mathf.Clamp(Random.Range(1, 3f), 0, WhalePopulation));
        WhalePopulation -= HuntedAmnt;
        GlobalFunctions.ChangeWhalePopulation(transform.GetSiblingIndex(), WhalePopulation);
        MngmntGameHandler.instance.jwh.IncreaseProductReserves(HuntedAmnt);
        ScoreLayer.instance.CreateScore(transform.position, LocalizationManager.instance.GetLocalizedValue("whales"), "-" + HuntedAmnt, Color.red, Vector3.down * 15, ScoreDeltaText.ScoreDirection.Down);
        Destroy(whalingIndicator);
    }

    private void Start()
    {
        base.Start();

        if (JWHPresence)
            ((JWH)MngmntGameHandler.instance.jwh).RegisterWhalingSpot(this);

        int tempWhalePopulation = GlobalFunctions.GetWhalePopulation(transform.GetSiblingIndex());
        if (tempWhalePopulation >= 0)
        {
            WhalePopulation = tempWhalePopulation;
        }
        stats = new RegionStat[1];
        stats[0] = new RegionStat(LocalizationManager.instance.GetLocalizedValue("Whale Population"), WhalePopulation);
        
        actions.Add(new RegionAction(LocalizationManager.instance.GetLocalizedValue("Protect Whales"), 50, TakeControl));
        
    }
}
