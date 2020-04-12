using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RngrSubscriptionHndlr : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine("GetLink");
	}
	
    IEnumerator GetLink()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://tassogames.com/get_key.php?getKey=1");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
    }
}
