using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionDetailsView : MonoBehaviour {

	// Use this for initialization
	protected void Start () {
        RegionHandler.instance.regionUnSlctdCB.AddListener(ClearDetails);
	}
	
    protected void ClearDetails()
    {
        for (int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);
    }

}
