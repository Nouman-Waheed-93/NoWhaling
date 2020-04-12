using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    public class Bullet : LifeSpanObject
    {
        public float speed;
        
        protected void Update()
        {
            base.Update();
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        protected override void DeactivateObject()
        {
            base.DeactivateObject();
            GameObject hitFX = PoolManager.instance.bulletHitFXPool.GetPooledObject();
            hitFX.transform.position = transform.position;
            hitFX.transform.rotation = transform.rotation;
            hitFX.GetComponent<LifeSpanObject>().Activate();
        }
        
        public virtual float Fire(float distance)
        {
            lifeRemaining = distance/ speed;
            gameObject.SetActive(true);
            return lifeRemaining;
        }
    }
}