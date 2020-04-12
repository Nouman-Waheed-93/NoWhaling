using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    public class WhalerAI : AttackVehicleAI
    {
        public float whaleTakeDistance;
        public float whaleTakePlyrDstnc;
        HarpoonLauncher harpoon;

        WhaleBehaviour Trgtwhale;
        Vector3 startingPos;
        bool returnToBase;

        private void Start()
        {
            base.Start();
            harpoon = GetComponent<HarpoonLauncher>();
            harpoon.GotEnoughFishes.AddListener(EnoughFish);
            GameManager.instance.RegisterWhaler();
            Health mHlth = GetComponent<Health>();
            mHlth.onDie.AddListener(GameManager.instance.WhalerDestroyed);
            WorldUI.instance.CreateShipPointer(transform);
            mHlth.onDie.AddListener(ReleaseFish);
            Trgtwhale = WhaleTargeter.singleton.getNextTarget();
            if (!Trgtwhale)
                Destroy(gameObject);
            harpoon.SetTarget(Trgtwhale);
        }

        private void Update()
        {
            if (!returnToBase)
            {
                if (Trgtwhale == null)
                {
                    Trgtwhale = WhaleTargeter.singleton.getNextTarget();
                }
                if(Trgtwhale)
                    ai.SetDestination(Trgtwhale.transform.position);
            }
            else
                ReturnToBase();
        }
        
        void ReturnToBase()
        {
            if (Vector3.Distance(startingPos, transform.position) > whaleTakeDistance
                && Vector3.Distance(transform.position, GameManager.instance.playerObject.transform.position) > whaleTakePlyrDstnc)
            {
                WhaleBehaviour[] whales = GetComponentsInChildren<WhaleBehaviour>();
                int countWhales = 0;
                for(int i =0; i < whales.Length; i++)
                {
                    whales[i].Die();
                    if (!whales[i].GetComponent<Health>().isDead)
                        countWhales++;
                }
                GameManager.instance.FishTakenAway(countWhales);
                Destroy(gameObject);
            }
        }

        void ReleaseFish()
        {
            WhaleBehaviour[] whales = GetComponentsInChildren<WhaleBehaviour>();
            foreach(WhaleBehaviour w in whales)
            {
                w.UnTie();
            }
        }

        void EnoughFish()
        {
            if (GameManager.instance.playerObject == null)
                return;

            ((NavMeshVehicle)ai).Escape();
            startingPos = transform.position;
            returnToBase = true;
            Debug.Log("return bhago");
        }
    }
}
