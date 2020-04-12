using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NoWhaling {
    public class ResultScreen : MonoBehaviour {

        public static ResultScreen instance;

        public Text JWHDmgTxt, WhalesTrtdTxt, WhalesLostTxt, ShipDmgTxt, RsltTxt;

        public string RsltStrng;
        public int NumJWHDmg, NumTreatedWhales, NumWhalesLost;

        private void Start()
        {
            instance = this;
            gameObject.SetActive(false);
        }

        public void ShowResults()
        {
            gameObject.SetActive(true);
            JWHDmgTxt.text = NumJWHDmg.ToString();
            WhalesTrtdTxt.text = NumTreatedWhales.ToString();
            WhalesLostTxt.text = NumWhalesLost.ToString();
            GlobalFunctions.AddDJINDamage(NumJWHDmg);
            GlobalFunctions.AddWhalesLost(NumWhalesLost);
            GlobalFunctions.AddWhalesSaved(NumTreatedWhales);
            LBManager.Instance.ReportAllProgress();
            RsltTxt.text = RsltStrng;
            ShipDmgTxt.text = ((1 - GameManager.instance.playerObject.GetComponent<Health>().GetHlthinPerOne()) *100).ToString();
            InFieldProfitLoss.SetLoss("JWH", NumJWHDmg);
            InFieldProfitLoss.SetProfit("Tasso", NumTreatedWhales + (int)(NumJWHDmg * 0.2f));
            InFieldProfitLoss.SetLoss("Tasso", NumWhalesLost * 50);
        }

    }
}