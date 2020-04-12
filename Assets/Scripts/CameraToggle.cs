using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToggle : MonoBehaviour {

    public static CameraToggle instance;

    List<GameObject> gameCamObjects = new List<GameObject>();
    List<GameObject> observerCamObjects = new List<GameObject>();
    
    bool isIngameCam;

    private void Awake()
    {
        instance = this;
        isIngameCam = false;
        ToggleGameCam();
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(ToggleGameCam);
    }
    
    public void AddgameCamObject(GameObject obj)
    {
        gameCamObjects.Add(obj);
        obj.SetActive(isIngameCam);
    }

    public void AddobserverCamObject(GameObject obj)
    {
        observerCamObjects.Add(obj);
        obj.SetActive(!isIngameCam);
    }

    void ToggleGameCam()
    {
        isIngameCam = !isIngameCam;
        for(int i = 0; i < observerCamObjects.Count; i++)
        {
            observerCamObjects[i].SetActive(!isIngameCam);
        }
        for(int i = 0; i < gameCamObjects.Count; i++)
        {
            gameCamObjects[i].SetActive(isIngameCam);
        }
    }

}
