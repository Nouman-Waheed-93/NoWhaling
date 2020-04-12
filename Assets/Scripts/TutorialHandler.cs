using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour {

    public Tutorial[] tutorials;
    public Land tutorialLand;

	// Use this for initialization
	void Start () {
        
        if (!PlayerPrefs.HasKey(tutorials[0].tutorialName))
        {
            tutorials[0].NextStep();
        }
        else if (!PlayerPrefs.HasKey(tutorials[1].tutorialName))
        {
            if(tutorialLand.remainingSeconds <= 0)
                tutorials[1].NextStep();
        }
	}
	
}
