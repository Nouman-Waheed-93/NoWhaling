using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAudio : MonoBehaviour {

    public AudioSource asorce;
    public Rigidbody rb;
    public float minPitch, maxPitch, pitchMultiplier;
	// Use this for initialization
	void Reset () {
        rb = GetComponent<Rigidbody>();
        asorce = GetComponent<AudioSource>();
	}

    private void Update()
    {
        asorce.pitch = Mathf.Lerp(minPitch, maxPitch, rb.velocity.magnitude * pitchMultiplier);
    }

}
