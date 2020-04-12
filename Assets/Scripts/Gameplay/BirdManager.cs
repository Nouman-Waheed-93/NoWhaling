using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour {

    public GameObject BirdTemplate;
    public int minBirds, maxBirds;
    public float minDistance, maxDistance;
    public int minHieght, maxHeight;
    public float startDelay;
    public int birdLayer;
    public AudioClip seagullSound;
    public float spatialBlend;
    public float maxSoundDistance;
    public SphereCollider collider;
    
    GameObject[] birds;
    AudioSource asorce;
    float delayTimer;
    float disableTimer;

    private void Reset()
    {
        collider = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        SpawnBirds();
        EndBirds();
        delayTimer = startDelay;
    }

    private void OnTriggerExit(Collider other)
    {
        EndBirds();
        delayTimer = startDelay;
    }

    void Update () {
		//if(birds[0].activeSelf)
  //      {
  //          if (!collider.bounds.Contains(birds[0].transform.position) && disableTimer < 0)
  //          {
  //              EndBirds();
  //              delayTimer = startDelay;
  //          }
  //      }
        if(!birds[0].activeSelf)
        {
            if (delayTimer < 0)
            {
                StartBirds();
                disableTimer = 1;
            }
        }
        disableTimer -= Time.deltaTime;
        delayTimer -= Time.deltaTime;
	}

    void SpawnBirds()
    {
        birds = new GameObject[maxBirds];
        for(int i = 0; i < maxBirds; i++)
        {
            birds[i] = Instantiate(BirdTemplate);
        }
        birds[0].AddComponent<BoxCollider>();
        birds[0].AddComponent<Rigidbody>().useGravity = false;
        birds[0].layer = birdLayer;
        asorce = birds[0].AddComponent<AudioSource>();
        asorce.clip = seagullSound;
        asorce.spatialBlend = spatialBlend;
        asorce.maxDistance = maxSoundDistance;
    }

    void StartBirds()
    {
        transform.position = new Vector3(transform.position.x, minHieght, transform.position.z);
        int num = Random.Range(minBirds, maxBirds);
        float height = Random.Range(minHieght, maxHeight);
        Vector3 startingPos = transform.position + Random.onUnitSphere * collider.radius;
        startingPos.y = height;
        Vector3 endingPos = transform.position + Random.onUnitSphere * collider.radius;
        endingPos.y = height;
        for(int i = 0; i < num; i++)
        {
            Vector3 distance = i * Random.onUnitSphere * Random.Range(minDistance, maxDistance);
            birds[i].transform.position = startingPos + distance;
            birds[i].transform.LookAt(endingPos + distance);
            birds[i].SetActive(true);
        }
        asorce.Play();
    }
    
    void EndBirds()
    {
        for(int i = 0; i < maxBirds; i++)
        {
            birds[i].SetActive(false);
        }
    }

}
