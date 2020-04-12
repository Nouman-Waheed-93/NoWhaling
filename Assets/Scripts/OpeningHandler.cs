using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class OpeningHandler : MonoBehaviour {

    public VideoPlayer player1;
    public GameObject video1GO;
    public GameObject[] Statements;
    public float timePerStatement;
    Animator anim;
    GameObject nextGO;
    int currStInd = -1;
    int currOpInd;
    float cumTime = -int.MaxValue;
    delegate void Operation();
    List<Operation> listOfOps = new List<Operation>();
	// Use this for initialization
	void Start () {
        for(int i = 0; i < Statements.Length; i++)
            listOfOps.Add(NextStatement);
        anim = GetComponent<Animator>();
        player1.Prepare();
        player1.prepareCompleted += VideoReady;
        Handheld.StartActivityIndicator();
    }

    void VideoReady(VideoPlayer vp)
    {
        Debug.Log("o pya j");
        Handheld.StopActivityIndicator();
        anim.SetTrigger("FadeIn");
        video1GO.SetActive(true);
        player1.Play();
        player1.loopPointReached+= Video1Complete;
    }

    void Video1Complete(VideoPlayer vp)
    {
        video1GO.SetActive(false);
        anim.SetTrigger("FadeOut");
        anim.SetTrigger("FadeIn");
    }

    private void Update()
    {
        cumTime += Time.deltaTime;
        if(cumTime > timePerStatement)
        {
            anim.SetTrigger("FadeOut");
            anim.SetTrigger("FadeIn");
            cumTime = -10000;
        }
    }
    
    void NextStatement()
    {
        if (currStInd > -1)
            Statements[currStInd].SetActive(false);
        currStInd++;
        Statements[currStInd].SetActive(true);
    }

    public void FadedOut()
    {
        cumTime = 0;
        if (currOpInd < listOfOps.Count)
        {
            listOfOps[currOpInd]();
            currOpInd++;
        }
        else
            LoadNextScene();
    }

    public void FadedIn()
    {

    }

    public void LoadNextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ManagmentPanel");
    }

}
