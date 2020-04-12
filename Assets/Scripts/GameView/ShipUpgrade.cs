using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameView
{
    public class ShipUpgrade : MonoBehaviour
    {
        public string UpgradeName;
        public int maxValue = 6;
        public int price;
        public int currLevel;
        public Button BuyBtn;
        public Slider slider;
        public GameObject PriceTag;

        private void Start()
        {
            currLevel = PlayerPrefs.GetInt(UpgradeName, 0);
            BuyBtn.interactable = (currLevel < maxValue);
            PriceTag.SetActive(currLevel < maxValue);
            PriceTag.GetComponentInChildren<Text>().text = price.ToString();
            slider.value = currLevel;
        }

        public void Buy()
        {
            int gemsAmt = PlayerPrefs.GetInt("Gems", 0);
            if (gemsAmt >= price)
            {
                PlayerPrefs.SetInt("Gems", gemsAmt - price);
                currLevel = Mathf.Clamp(++currLevel, 0, maxValue);
                PlayerPrefs.SetInt(UpgradeName, currLevel);
                slider.value = currLevel;
                BuyBtn.interactable = (currLevel < maxValue);
                PriceTag.SetActive(currLevel < maxValue);
                GemView.updateView.Invoke();
            }
            else
                GemMessage.instance.ShowUp();
        }
    }
}