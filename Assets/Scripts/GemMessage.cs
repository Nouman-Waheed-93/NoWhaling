using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemMessage : NotEnoughCurrency {

    public static GemMessage instance;

    private new void Start()
    {
        instance = this;
        base.Start();
    }
}
