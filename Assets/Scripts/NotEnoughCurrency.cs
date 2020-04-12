using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NotEnoughCurrency : MonoBehaviour {

    protected void Start()
    {
        gameObject.SetActive(false);
    }

    public void DisableGameObject()
    {
        gameObject.SetActive(false);
    }

    public void ShowUp()
    {
        gameObject.SetActive(true);
    }
}
