using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverCamActivator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CameraToggle.instance.AddobserverCamObject(gameObject);
    }
	
}
