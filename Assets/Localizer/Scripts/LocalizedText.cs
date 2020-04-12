using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
    [SerializeField]
    string key;

    public string Key { set {
            key = value;
            UpdateText();
        } }

    enum TextSize { Small, Normal, Big};
    [SerializeField]
    TextSize size;

    // Use this for initialization
    void Start()
    {
        UpdateText();
    }

    void UpdateText() {
        Text text = GetComponent<Text>();
        text.text = LocalizationManager.instance.GetLocalizedValue(key);
        text.font = LocalizationManager.instance.GetFont();
        if (size == TextSize.Big)
            text.fontSize = LocalizationManager.instance.GetBigSize();
        else if (size == TextSize.Normal)
            text.fontSize = LocalizationManager.instance.GetNormalSize();
        else
            text.fontSize = LocalizationManager.instance.GetSmallSize();
    }

}