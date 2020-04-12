using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling {
    public class EnemySpawner : MonoBehaviour {

        public Transform[] SpawnPoints;
        public GameObject EnemyObj;
        public float spawnTime;
        public Transform[] WPSet;
        int currWPInd = -1;
        /// <summary>
        /// the must distance that spawn point should have with player
        /// </summary>
        public float SpawnDistWithPlayer;
        /// <summary>
        /// max amount of enemies that can exist at a time
        /// </summary>
        public int maxEnemies;
        /// <summary>
        /// total number of enemies that have to be spawned
        /// </summary>
        public int totalEnemiesToSpawn;

        float SpawnTimeLeft;
        int currSPInd;
        int enemiesActive;
        bool StopSpawning;

        public void StopSpawningEnemies()
        {
            StopSpawning = true;
        }

        // Update is called once per frame
        void Update() {
            if (StopSpawning)
                return;
            SpawnTimeLeft -= Time.deltaTime;
            if (SpawnTimeLeft < 0 && totalEnemiesToSpawn > 0 && enemiesActive < maxEnemies && GameManager.instance.playerObject)
            {
                if (Vector3.Distance(SpawnPoints[currSPInd].position, 
                    GameManager.instance.playerObject.transform.position) > SpawnDistWithPlayer)
                {
                    GameObject obj = Instantiate(EnemyObj, SpawnPoints[currSPInd].position, SpawnPoints[currSPInd].rotation);
                    obj.GetComponent<Health>().onDie.AddListener(EnemyDestroyed);
                    obj.GetComponent<AttackVehicleAI>().WayPointsParent = GetNextWPSet();
                    SpawnTimeLeft = spawnTime;
                    enemiesActive++;
                    totalEnemiesToSpawn--;
                }
                currSPInd++;
                if(currSPInd >= SpawnPoints.Length)
                {
                    currSPInd = 0;
                }
            }
        }

        Transform GetNextWPSet()
        {
            currWPInd++;
            if(currWPInd >= WPSet.Length)
            {
                currWPInd = 0;
            }
            return WPSet[currWPInd];
        }

        public void EnemyDestroyed()
        {
            enemiesActive--;
        }

        public void NeedToSpwnAnthrOne()
        {
            if (!StopSpawning)
            {
                totalEnemiesToSpawn++;
                EnemyDestroyed();
            }
        }
    }
}