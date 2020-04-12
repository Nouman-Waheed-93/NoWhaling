using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour
{

    public static LocalizationManager instance;
    
    private Dictionary<string, string> localizedText;
    private bool isReady = false;
    private string missingTextString = "Localized text not found";
    private int smallSize, normalSize, BigSize;
    private Font font;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            if (PlayerPrefs.GetString("LanguageSet", "") != "")
                LoadLocalizedText(PlayerPrefs.GetString("LanguageSet"));
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void LoadLocalizedText(string fileName)
    {
        localizedText = new Dictionary<string, string>();
   //     string filePath;
        string dataAsJson = "";
      //  filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        dataAsJson = Resources.Load<TextAsset>(fileName).ToString();
        bool fileFound = dataAsJson != "";

        if (fileFound)
        {
            PlayerPrefs.SetString("LanguageSet", fileName);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }

            smallSize = loadedData.smallSize;
            normalSize = loadedData.normalSize;
            BigSize = loadedData.bigSize;
            font = Resources.Load<Font>(loadedData.font);

            Debug.Log("Data loaded, dictionary contains: " + localizedText.Count + " entries");
        }
        else
        {
            Debug.LogError("Cannot find file!");
        }

        isReady = true;
    }

    public string GetLocalizedValue(string key)
    {
        string result = missingTextString;
        if (localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }

        return result;

    }

    public bool GetIsReady()
    {
        return isReady;
    }
    

    public int GetSmallSize() {
        return smallSize;
    }

    public int GetNormalSize() {
        return normalSize;
    }

    public int GetBigSize() {
        return BigSize;
    }

    public Font GetFont() {
        return font;
    }

}