using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMisc : MonoBehaviour {

    public GameObject PauseScreen;
    public GameObject ResultScreen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowResults()
    {
        ResultScreen.SetActive(true);
    }

    public void ShowPauseScreen()
    {
        PauseScreen.SetActive(true);
        PauseGame();
    }

    public void HidePauseScreen()
    {
        PauseScreen.SetActive(false);
        UnPauseGame();
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ToMainMenu()
    {
        UnPauseGame();
        SceneManager.LoadScene("ManagmentPanel");
    }
}
