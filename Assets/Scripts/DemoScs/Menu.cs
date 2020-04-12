using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    public GameObject MainMenuGO, ingameGO;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);   
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
        ingameGO.SetActive(true);
        MainMenuGO.SetActive(false);
    }

    public void BackToMenu()
    {
        ingameGO.SetActive(false);
        MainMenuGO.SetActive(true);
    }
}
