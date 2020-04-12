using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayTutorial : MonoBehaviour {
    public Tutorial tutorial;

    private void Start()
    {
        if (!PlayerPrefs.HasKey(tutorial.tutorialName))
        {
            Time.timeScale = 0;
            tutorial.NextStep();
            tutorial.onTutorialEnd.AddListener(TutorialEnded);
        }
    }

    void TutorialEnded()
    {
        Time.timeScale = 1;
    }
}
