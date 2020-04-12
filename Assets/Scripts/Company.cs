using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Company : MonoBehaviour {

    [HideInInspector]
    public UnityEvent BalanceUpdated = new UnityEvent();
    public string companyName;
    public int startingBankBal;

    public int bankBalance { get; private set; }

    protected List<Land> branches = new List<Land>();

    protected void Awake()
    {
        bankBalance = PlayerPrefs.GetInt(companyName, startingBankBal);
     //   TakeProfit(0);
        TakeLoss((uint)InFieldProfitLoss.GetLoss(companyName));
        InFieldProfitLoss.SetLoss(companyName, 0);
        TakeProfit((uint)InFieldProfitLoss.GetProfit(companyName));
        InFieldProfitLoss.SetProfit(companyName, 0);
    }

    public void TakeLoss(uint loss) {
        Debug.Log(companyName + " takes " + loss + " loss");
        bankBalance -= (int)loss;
        BalanceUpdated.Invoke();
        PlayerPrefs.SetInt(companyName, bankBalance);
    }

    public void TakeProfit(uint profit) {
        Debug.Log(companyName + " takes " + profit + " profit");
        bankBalance += (int)profit;
        BalanceUpdated.Invoke();
        PlayerPrefs.SetInt(companyName, bankBalance);
    }

}
