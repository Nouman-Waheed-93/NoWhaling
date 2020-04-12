using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    public class LifeSpanObject : MonoBehaviour
    {
        public float maxLifeSpan;

        protected float lifeRemaining;
        

        public void Activate() {
            lifeRemaining = maxLifeSpan;
            gameObject.SetActive(true);
        }
        
        protected void Update()
        {
            lifeRemaining -= Time.deltaTime;
            if (lifeRemaining <= 0)
            {
                DeactivateObject();
            }
        }

        protected virtual void DeactivateObject()
        {
            gameObject.SetActive(false);
        }
    }
}
