using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameView
{
    public class OperationProgressView : MonoBehaviour
    {
        public MainButtonsView OperationButtons;

        public Text operationName;
        public Slider slider;
        Land slctdLand;

        private void Start()
        {
            RegionHandler.instance.regionUnSlctdCB.AddListener(HideProgress);
        }

        public void HideProgress()
        {
            gameObject.SetActive(false);
        }

        public void ShowProgress()
        {
            slctdLand =  (Land)RegionHandler.instance.SelectedRegion;
            
            operationName.text = slctdLand.currOperation.name;
            slider.maxValue = slctdLand.currOperation.operationTime;
        }

        public void CancelOperation()
        {
            slctdLand.CancelOperations();
            gameObject.SetActive(false);
            OperationButtons.gameObject.SetActive(true);
            OperationButtons.RegionSelected();
        }

        private void Update()
        {
            if (!slctdLand)
                return;
            slider.value = slider.maxValue - slctdLand.remainingSeconds;
            if (slctdLand.remainingSeconds < 0)
            {
                gameObject.SetActive(false);
                OperationButtons.gameObject.SetActive(true);
                OperationButtons.RegionSelected();
            }
        }

    }
}