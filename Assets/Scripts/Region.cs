using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Region : MonoBehaviour {

    public Color regionColor;
    public Image image;
    public GameObject TassoPresenceInd, JWHPresenceInd;
    
    [HideInInspector]
    public bool JWHDemolished;

    protected RegionStat[] stats;
    protected bool tassoPresence, JWHPresence;

    protected List<RegionAction> actions = new List<RegionAction>();

    static Color BannedColor = Color.black;

    [System.Serializable]
    public struct OperationVar
    {
        public int price;
        public int time;
    }

    public virtual void BuyAsset() {
        GlobalFunctions.EstablishTaso(transform.GetSiblingIndex());
        InitTassoPresence();
        RegionHandler.instance.regionSlctdCB.Invoke();
    }

    public void EstablishJWH()
    {
        GlobalFunctions.EstablishJWH(transform.GetSiblingIndex());
        InitJWHPresence();
    }

    public void DemolishJWH()
    {
        GlobalFunctions.DemolishJWH(transform.GetSiblingIndex());
        BanJWH();
    }

    public abstract void CreateAssetAction();
    protected RegionAction buyAssetAction;

    public abstract RegionAction[] GetOptions();

    public RegionStat[] GetStats()
    {
        return stats;
    }
    
    public virtual void SelectRegion() {
        image.color = Color.Lerp(regionColor, RegionHandler.instance.selectedColor, 0.4f);
        RegionHandler.instance.RegionSelected(this);
        Debug.Log("Have done the stuff");
    }

    public virtual void UnSelectRegion() {
        image.color = regionColor;
        Debug.Log("REegon unselected");
    }

    protected void Start()
    {
        GetComponent<Button>().onClick.AddListener(SelectRegion);
        image.color = regionColor;
        CreateAssetAction();
        if (GlobalFunctions.IsJWHPresent(transform.GetSiblingIndex()))
            InitJWHPresence();
        if (GlobalFunctions.IsJWHDemolished(transform.GetSiblingIndex()))
            BanJWH();
        if (GlobalFunctions.IsTassoPresent(transform.GetSiblingIndex()))
            InitTassoPresence();
    }

    void InitTassoPresence()
    {
        tassoPresence = true;
        TassoPresenceInd.SetActive(true);
    }

    void InitJWHPresence()
    {
        JWHPresence = true;
        JWHPresenceInd.SetActive(true);
    }

    void BanJWH()
    {
        JWHDemolished = true;
        JWHPresenceInd.transform.GetChild(0).GetComponent<Image>().color = BannedColor;
        Debug.Log("Ban ban ban");
    }

}
