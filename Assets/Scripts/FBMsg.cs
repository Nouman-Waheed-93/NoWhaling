using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBMsg : NotEnoughCurrency {

    public static FBMsg instance;

    private new void Start()
    {
        instance = this;
        base.Start();
    }
}
