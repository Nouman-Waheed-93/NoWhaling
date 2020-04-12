using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalanceText : MonoBehaviour {
    public Text txt;
    public Company company;

    private void Start()
    {
        company.BalanceUpdated.AddListener(UpdateBalance);
        UpdateBalance();
    }

    void UpdateBalance()
    {
        txt.text = company.bankBalance.ToString();
    }
}
