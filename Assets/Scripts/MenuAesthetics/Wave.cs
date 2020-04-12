using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MenuAesthetics
{
    public class Wave : MonoBehaviour
    {
        public float speed;
        public Transform destinationPos;
        public Color Color1, Color2;
        public AnimationCurve colorCurve;
        Image image;
      
        public void StartWave()
        {
            gameObject.SetActive(true);
        }
        
        // Use this for initialization
        void Start()
        {
            image = GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {
            image.color = Color.Lerp(Color2, Color1, colorCurve.Evaluate(Time.time));
            if (Vector3.Distance(transform.position, destinationPos.position) > 0.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, destinationPos.position, speed);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
