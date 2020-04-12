using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace GameView
{
    public class GemView : MonoBehaviour
    {
        public static UnityEvent updateView = new UnityEvent();

        Text text;

        // Use this for initialization
        void Start()
        {
            text = GetComponent<Text>();
            UpdateText();
            updateView.AddListener(UpdateText);
        }

        public void UpdateText()
        {
            text.text = PlayerPrefs.GetInt("Gems", 0).ToString();
        }

        private void OnDestroy()
        {
            updateView.RemoveListener(UpdateText);
        }
    }
}
