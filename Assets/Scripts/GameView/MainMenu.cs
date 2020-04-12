using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameView
{

    public class MainMenu : MonoBehaviour
    {
        public static MainMenu instance;

        public GameObject OptionsBar, VictoryMsg, DefeatMsg, BannedMsg, UpgradeMnu, LowBalMsg, OneClickCover, TutorialScreen, RangerRegistration;

        public Tutorial ManagementTutorial, GameplayTutorial;

        GameObject menuToTransitionTo;

        public AudioClip banClip;

        Animator anim;

        AudioSource asorce;
        private void Awake()
        {
            anim = GetComponent<Animator>();
            asorce = GetComponent<AudioSource>();
            instance = this;
            LBManager.Instance.Authenticate();
            Input.backButtonLeavesApp = true;
        }

        private void Start()
        {
            Debug.Log("Progress ho gayi " + GlobalFunctions.GetProgress());
            if (GlobalFunctions.GetProgress() > SceneManager.sceneCountInBuildSettings - GlobalVals.totalNonOceanScenes)
                Victory();
            else if (GlobalFunctions.GetProgress() > 3 && !GlobalFunctions.RangerRegistered()) {
                ShowRangerReg();
            }
            else if (MngmntGameHandler.instance.Tasso.bankBalance <= 0)
            {
                ShowLowBal();
            }

        }

        public void Victory()
        {
            PlayerPrefs.DeleteAll();
            menuToTransitionTo = VictoryMsg;
            anim.SetTrigger("Close");
            anim.SetTrigger("Open");
            OneClickCover.SetActive(true);
        }

        public void Defeat()
        {
            PlayerPrefs.DeleteAll();
            menuToTransitionTo = DefeatMsg;
            anim.SetTrigger("Close");
            anim.SetTrigger("Open");
            OneClickCover.SetActive(true);
        }

        public void AboutUs()
        {
            Application.OpenURL("https://www.tasso.net/Ueber-uns");
        }

        public void HelpInReal()
        {
            Application.OpenURL("https://www.tasso.net/Ihre-Spende/Online-spenden?sc=hpsp002");
        }

        public void ShowBanned()
        {
            AudioSource.PlayClipAtPoint(banClip, transform.position);
            GemView.updateView.Invoke();
            menuToTransitionTo = BannedMsg;
            anim.SetTrigger("Close");
            anim.SetTrigger("Open");
            OneClickCover.SetActive(true);
        }

        public void ShowRangerReg()
        {
            AudioSource.PlayClipAtPoint(banClip, transform.position);
            menuToTransitionTo = RangerRegistration;
            anim.SetTrigger("Close");
            anim.SetTrigger("Open");
            OneClickCover.SetActive(true);
        }

        public void RegisterRanger()
        {
            HideBanned();
            GlobalFunctions.RegisterRanger();
            Application.OpenURL("http://tassogames.com/?key=2");
        }

        public void HideBanned()
        {
            anim.SetTrigger("Close");
            anim.SetTrigger("OpenFull");
            OneClickCover.SetActive(true);
        }

        public void ShowLowBal()
        {
            menuToTransitionTo = LowBalMsg;
            anim.SetTrigger("Close");
            anim.SetTrigger("Open");
        }

        public void ToLanguage()
        {
            Destroy(LocalizationManager.instance.gameObject);
            PlayerPrefs.SetString("LanguageSet", "");
            anim.SetTrigger("Close");
            SceneManager.LoadSceneAsync("LanguageSelection");
        }

        public void ToUpgrade()
        {
            menuToTransitionTo = UpgradeMnu;
            anim.SetTrigger("Close");
            anim.SetTrigger("OpenFull");
            OneClickCover.SetActive(true);
        }
      
        public void ToTutorialScreen()
        {
            GameplayTutorial.gameObject.SetActive(false);
            ManagementTutorial.gameObject.SetActive(false);
            menuToTransitionTo = TutorialScreen;
            anim.SetTrigger("Close");
            anim.SetTrigger("Open");
            OneClickCover.SetActive(true);
        }

        public void ToManagmentTutorial()
        {
            menuToTransitionTo = ManagementTutorial.gameObject;
            anim.SetTrigger("Close");
            anim.SetTrigger("OpenFull");
            OneClickCover.SetActive(true);
        }

        public void ToGameplayTutorial()
        {
            menuToTransitionTo = GameplayTutorial.gameObject;
            anim.SetTrigger("Close");
            anim.SetTrigger("OpenFull");
            OneClickCover.SetActive(true);
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void Restart()
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void PlayButtonSound()
        {
            asorce.Play();
        }

        public void ToggleOptionsBar()
        {
            PlayButtonSound();
            OptionsBar.SetActive(!OptionsBar.activeSelf);
        }

        public void ShowLeaderboard()
        {
            LBManager.Instance.ShowLeaderboardUI();
        }

        public void DoorsClosed()
        {
            OneClickCover.SetActive(false);
            if (menuToTransitionTo)
                menuToTransitionTo.SetActive(!menuToTransitionTo.activeSelf);
            if (menuToTransitionTo.gameObject.activeInHierarchy && menuToTransitionTo == ManagementTutorial.gameObject)
            {
                ManagementTutorial.NextStep();
                TutorialScreen.SetActive(false);
            }
            else if (menuToTransitionTo.gameObject.activeInHierarchy && menuToTransitionTo == GameplayTutorial.gameObject)
            {
                GameplayTutorial.NextStep();
                TutorialScreen.SetActive(false);
            }

        }

        public void DoorsOpened()
        {

        }

    }
}