using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValuesInitializer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (!PlayerPrefs.HasKey("Gems"))
        {
            PlayerPrefs.SetInt("Gems", 1000);
            PlayerPrefs.SetInt("Main Cannon", 1);
            PlayerPrefs.SetInt("Side Cannons", 1);
            PlayerPrefs.SetInt("Rear Cannons", 1);
        }
    }
	
}
