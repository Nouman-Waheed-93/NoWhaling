using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NoWhaling
{
    public class MissionArea : MonoBehaviour
    {
        Collider coll;

        public float timeAllowedOutSide;
        [HideInInspector]
        public UnityEvent playerLeftArea = new UnityEvent();
        [HideInInspector]
        public UnityEvent playerEnteredArea = new UnityEvent();

        public float timeCntr { get; private set; }
        bool playerOutside;

        private void Start()
        {
            coll = GetComponent<Collider>();
            timeCntr = timeAllowedOutSide;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.isTrigger)
            {
                if (other.gameObject == GameManager.instance.playerObject)
                {
                    playerEnteredArea.Invoke();
                    timeCntr = timeAllowedOutSide;
                    playerOutside = false;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.isTrigger)
            {
                if(other.gameObject == GameManager.instance.playerObject)
                {
                    playerLeftArea.Invoke();
                    playerOutside = true;
                }
            }
        }

        private void Update()
        {
            if (playerOutside)
            {
                timeCntr -= Time.deltaTime;
                if(timeCntr <= 0)
                {
                    GameManager.instance.FailMission();
                }
            }
        }
    }
}