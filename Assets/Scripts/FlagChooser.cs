using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagChooser : MonoBehaviour {

    [SerializeField]
    Material flagMat;
    [SerializeField]
    Texture[] flagTexs;
    
    // Use this for initialization
	void Start () {
        flagMat.mainTexture = flagTexs[Random.Range(0, flagTexs.Length)];
	}
	
}
