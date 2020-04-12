using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {
    [System.Serializable]
    public struct TimedSound
    {
        public AudioClip audio;
        public float playTime;
    }

    public TimedSound[] sounds;
    public AudioSource mySrc;
    public string nextSceneName;

    public void PlaySound(int ind)
    {
        mySrc.clip = sounds[ind].audio;
        mySrc.pitch = mySrc.clip.length / sounds[ind].playTime;
        mySrc.Play();
    }
    
    public void LoadScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
