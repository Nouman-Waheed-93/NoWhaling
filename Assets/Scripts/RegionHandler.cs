using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameView;

public class RegionHandler : MonoBehaviour {

    public static RegionHandler instance;
    
    public Color UnselectedColor;
    public Color selectedColor;
    public Color TassoColor;
    public Color JWHColor;
    public UnityEvent regionSlctdCB = new UnityEvent();
    public UnityEvent regionUnSlctdCB = new UnityEvent();
    public UnityEvent operationStarted = new UnityEvent();

    Region selectedRegion;

    public Region SelectedRegion
    {
        get
        {
            return selectedRegion;
        }
    }

    public void RegionSelected(Region selectedRegion) {
        if (this.selectedRegion != null)
            this.selectedRegion.UnSelectRegion();
        if (this.selectedRegion == selectedRegion)
        {
            this.selectedRegion = null;
            regionUnSlctdCB.Invoke();
        }
        else
        {
            regionUnSlctdCB.Invoke();
            this.selectedRegion = selectedRegion;
            regionSlctdCB.Invoke();
        }
        MainMenu.instance.PlayButtonSound();
    }
    
    // Use this for initialization
    void Awake () {
        instance = this;
	}

}

public class RegionStat
{
    public string StatName
    {
        get
        {
            return statName;
        }
    }

    public int StatValue
    {
        get
        {
            return statValue;
        }
        set
        {
            statValue = value;
        }
    }
    
    int statValue;
    string statName;

    public RegionStat(string statName, int statValue)
    {
        this.statName = statName;
        this.statValue = statValue;
    }

}

public class RegionAction
{

    public UnityAction Action {
        get {
            return action;
        }
    }

    public string ActionName {
        get
        {
            return actionName;
        }
    }

    public int Price {
        get {
            return price;
        }
    }

    UnityAction action;
    string actionName;
    int price;

    public RegionAction(string name, int price, UnityAction action) {
        this.actionName = name;
        this.action = action;
        this.price = price;
    }

}