using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedAudioLoop : MonoBehaviour {

    AudioSource asorce;
    public float minDelay, maxDelay;
	// Use this for initialization
	void Start () {
        asorce = GetComponent<AudioSource>();
        Invoke("PlayAudio", Random.Range(minDelay, maxDelay));
	}

    void PlayAudio() {
        asorce.Play();
        Invoke("PlayAudio", Random.Range(minDelay, maxDelay));
    }
}
