using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tutorial : MonoBehaviour
{

    int currStep = -1;
    public string tutorialName;
    public TutorialStep[] steps;

    [HideInInspector]
    public UnityEvent onTutorialEnd = new UnityEvent();

    public void NextStep()
    {
        StartCoroutine(WaitNDNextStep());
    }

    IEnumerator WaitNDNextStep()
    {
        if (currStep > -1)
        {
            steps[currStep].gameObject.SetActive(false);
            if(steps[currStep].okBtn)
                steps[currStep].okBtn.onClick.RemoveListener(NextStep);
        }
        currStep++;
        if (currStep < steps.Length)
        {
            steps[currStep].gameObject.SetActive(true);
            while (!steps[currStep].okBtn)
                yield return null;
            steps[currStep].okBtn.onClick.AddListener(NextStep);
        }
        else
        {
            currStep = -1;
            PlayerPrefs.SetInt(tutorialName, 1);
            onTutorialEnd.Invoke();
        }
    }

}
