using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalMessage : NotEnoughCurrency {

    public static BalMessage instance;

    public GameObject FBOffer;

    private new void Start()
    {
        instance = this;
        base.Start();
        FBOffer.SetActive(false);
    }

    private void OnEnable()
    {
        FBOffer.SetActive(true);
    }
}
