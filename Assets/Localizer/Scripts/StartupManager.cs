using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartupManager : MonoBehaviour
{
    public GameObject LangButtons;
    public string mainSceneName;
    public Animator anim;

    bool animationComplete;
    
    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.GetString("LanguageSet", "") == "")
        {
            LangButtons.SetActive(true);
        }
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene(mainSceneName);
    }

    public void AnimationComplete()
    {
        animationComplete = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if (LocalizationManager.instance.GetIsReady())
        {
            anim.SetTrigger("LangDone");
            if (animationComplete)
                UnityEngine.SceneManagement.SceneManager.LoadScene(mainSceneName);
        }
    }
}
