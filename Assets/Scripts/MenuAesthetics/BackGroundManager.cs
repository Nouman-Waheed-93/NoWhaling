using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MenuAesthetics
{
    public class BackGroundManager : MonoBehaviour
    {

        public Color[] waterColorShades;
        public Wave[] waves;
        public Transform waveStart, waveEnd;
        public float waveAngle;
        public float minSpawnDelay, maxSpawnDelay;
        public int minWaveWidth, maxWaveWidth;

        float spawnDelay;
        int currInd;
        // Use this for initialization
        void Start()
        {
            SetWaveRotations();
        }

        private void Update()
        {
            if(spawnDelay > 0)
            {
                spawnDelay -= Time.deltaTime;
            }
            else
            {
                SpawnWave();
                spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            }
        }
        
        void SetWaveRotations()
        {
            foreach(Wave wave in waves)
            {
                wave.transform.Rotate(0, 0, waveAngle);
            }
        }
        
        void SpawnWave()
        {
            if (waves[currInd].gameObject.activeInHierarchy)
                return;

            waves[currInd].GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
                RectTransform.Axis.Horizontal, Random.Range(minWaveWidth, maxWaveWidth));
            waves[currInd].StartWave();
            waves[currInd].transform.position = waveStart.position;
            waves[currInd].gameObject.SetActive(true);
            currInd++;
            if (currInd >= waves.Length)
                currInd = 0;
        }
        
    }
}