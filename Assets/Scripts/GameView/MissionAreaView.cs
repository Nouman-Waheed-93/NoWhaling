using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NoWhaling;
using UnityEngine.UI;

namespace GameView
{
    public class MissionAreaView : MonoBehaviour
    {
        public GameObject returnMsg;
        public Text timeLeft;

        MissionArea area;
        bool outOfArea;
        
        // Use this for initialization
        void Start()
        {
            area = FindObjectOfType<MissionArea>();
            area.playerLeftArea.AddListener(AreaLeft);
            area.playerEnteredArea.AddListener(AreaReentered);
        }

        void AreaLeft()
        {
            outOfArea = true;
            returnMsg.SetActive(true);
        }

        void AreaReentered()
        {
            outOfArea = false;
            returnMsg.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (outOfArea)
            {
                timeLeft.text = Mathf.CeilToInt(area.timeCntr).ToString();
            }
        }
    }
}