using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    public class GameManager : MonoBehaviour
    {
        public GameObject playerObject;
        public EnemySpawner whalerSpawner;
        public AudioSource ASWhalerKilled, ASWhaleDead, ASWhaleTaken, ASWhaleSaved;
        public static GameManager instance;
        int remainingFishes;
        int remainingWhalers;

        // Use this for initialization
        void Awake()
        {
            playerObject = GameObject.FindGameObjectWithTag("Player");
            playerObject.GetComponent<Health>().onDie.AddListener(FailMission);
            instance = this;
        }

        public void RegisterFish(WhaleBehaviour whale)
        {
            Health hlth = whale.GetComponent<Health>();
            hlth.onDie.AddListener(FishTakenAway);
            hlth.onHealed.AddListener(FishSaved);
            remainingFishes++;
            Debug.Log("Fishes total " + remainingFishes);
        }

        public void RegisterWhaler()
        {
            remainingWhalers++;
        }

        public void WhalerDestroyed()
        {
            remainingWhalers--;
            Debug.Log("Ae j baqiya " + remainingWhalers);
            if(remainingWhalers < 1)
            {
                SucceedMission();
            }
            ASWhalerKilled.Play();
        }

        public void FishSaved()
        {
            ASWhaleSaved.Play();
        }

        public void FishTakenAway()
        {
            ResultScreen.instance.NumWhalesLost++;
            remainingFishes--;
            Debug.Log("Remaining fishing " + remainingFishes);
            if (remainingFishes <= 0)
            {
                FailMission();
            }
            ASWhaleDead.Play();
        }

        public void FishTakenAway(int number)
        {
            ResultScreen.instance.NumWhalesLost+=number;
            remainingFishes -= number;
            Debug.Log("Remaining fish  " + remainingFishes);
            if (remainingFishes <= 0)
            {
                FailMission();
            }
            else
            {
                whalerSpawner.NeedToSpwnAnthrOne();
                remainingWhalers--;
            }
            if(number > 0)
                ASWhaleTaken.Play();
        }

        public void FailMission()
        {
            whalerSpawner.StopSpawningEnemies();
            Debug.Log("Failure reason ");
            ResultScreen.instance.RsltStrng = LocalizationManager.instance.GetLocalizedValue("failed");
            ResultScreen.instance.ShowResults();
        }

        public void SucceedMission()
        {
            ResultScreen.instance.RsltStrng = LocalizationManager.instance.GetLocalizedValue("successful");
            GlobalFunctions.IncreaseProgressLevel();
            GlobalFunctions.DemolishJWH(GlobalVals.LastWhaledOcean);
            ResultScreen.instance.ShowResults();
        }
        
    }
}