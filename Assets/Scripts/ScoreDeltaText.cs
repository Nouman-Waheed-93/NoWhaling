using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDeltaText : MonoBehaviour {

    public enum ScoreDirection { Up, Down};

    public float fadeTime;
    public float stayTime;
    public AudioClip ScoreUp, ScoreDown;

    public Text Scoretext, operationTxt;
    Vector3 speed;
    bool stay = true;
    
    public void AppearAtPosition(Vector3 position, string operation, string score, Color color, Vector3 speed, ScoreDirection scrDir)
    {
        AudioSource asorce = GetComponent<AudioSource>();
        if (scrDir == ScoreDirection.Down)
        {
            asorce.clip = ScoreDown;
        }
        else if (scrDir == ScoreDirection.Up)
        {
            asorce.clip = ScoreUp;
        }
        asorce.Play();

        transform.position = position;
        this.speed = speed;
        Scoretext.color = color;
        operationTxt.color = color;
        Scoretext.text = score;
        operationTxt.text = operation;
        Invoke("StartFading", stayTime);
    }

    void StartFading()
    {
        Scoretext.CrossFadeAlpha(0, fadeTime, false);
        operationTxt.CrossFadeAlpha(0, fadeTime, false);
        stay = false;
        Destroy(gameObject, fadeTime);
    }

	// Update is called once per frame
	void Update () {
        if(!stay)
            transform.position += speed * Time.deltaTime;
	}
}
