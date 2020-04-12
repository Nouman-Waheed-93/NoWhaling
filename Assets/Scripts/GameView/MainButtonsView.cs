using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameView
{
    public class MainButtonsView : RegionDetailsView
    {
        public OperationProgressView progressView;
        // Use this for initialization
        void Start()
        {
            base.Start();
            RegionHandler.instance.regionSlctdCB.AddListener(RegionSelected);
            RegionHandler.instance.regionUnSlctdCB.AddListener(RegionUnSelected);
        }

        public void RegionSelected()
        {
            if (RegionHandler.instance.SelectedRegion == null)
                return;

            ShowButtons(RegionHandler.instance.SelectedRegion.GetOptions());
            if(RegionHandler.instance.SelectedRegion is Land)
            {
                Land land = (Land)RegionHandler.instance.SelectedRegion;
                if (land)
                    land.operationStarted.AddListener(OperationStarted);
            }
        }

        public void RegionUnSelected()
        {

        }

        void ShowButtons(RegionAction[] actions)
        {
            ClearDetails();
            if (actions != null)
            {
                gameObject.SetActive(true);
                progressView.gameObject.SetActive(false);
                for (int i = 0; i < actions.Length; i++)
                {
                    Button button = Instantiate(Resources.Load<Button>("ButtonTemplate"), transform);
                    button.transform.GetChild(0).GetComponent<Text>().text = actions[i].ActionName;
                    button.transform.GetChild(1).GetComponentInChildren<Text>().text = actions[i].Price.ToString();
                    button.onClick.AddListener(actions[i].Action);
                    button.onClick.AddListener(MainMenu.instance.PlayButtonSound);
                }
            }
            else
            {
                if (RegionHandler.instance.SelectedRegion is Land)
                {
                    progressView.ShowProgress();
                    gameObject.SetActive(false);
                    progressView.gameObject.SetActive(true);
                }
                else
                {

                }
            }
        }

        void OperationStarted()
        {
            gameObject.SetActive(false);
            progressView.gameObject.SetActive(true);
            progressView.ShowProgress();
        }
        
    }
}
