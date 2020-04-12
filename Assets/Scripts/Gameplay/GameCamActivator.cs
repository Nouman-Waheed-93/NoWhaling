using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamActivator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CameraToggle.instance.AddgameCamObject(gameObject);
    }
}
