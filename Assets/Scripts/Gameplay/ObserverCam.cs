using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ObserverCam : MonoBehaviour {

    public float rotationSpeed;
    Vector3 turnVector;

	// Use this for initialization
	void Start () {
        CameraToggle.instance.AddobserverCamObject(gameObject);
	}

    private void Update()
    {
        Rotate(CrossPlatformInputManager.GetAxis("CamH"));
    }

    public void Rotate(float dir)
    {
        turnVector = transform.rotation.eulerAngles;
        turnVector.y += dir * rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(turnVector);
    }

}
