using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    public class Projectile : LifeSpanObject
    {
        public float propulsiveForce;
        
        protected Rigidbody rb;

        protected int damage;

        Collider launcherColl, myCollider;
        
        protected void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
        
        public void Fire(int damage, Collider launcherColl)
        {
            this.damage = damage;
            if(rb)
                rb.velocity = Vector3.zero;
            if (this.launcherColl != null)
                Physics.IgnoreCollision(this.launcherColl, myCollider, false);
            this.launcherColl = launcherColl;
            if (myCollider == null)
                myCollider = GetComponent<Collider>();
            Physics.IgnoreCollision(this.launcherColl, myCollider, true);
     
            Activate();
        }

        protected void FixedUpdate()
        {
            rb.AddRelativeForce(0, 0, propulsiveForce,ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            gameObject.SetActive(false);
            Health hlth = collision.gameObject.GetComponent<Health>();
            if (hlth)
                hlth.Damage(damage);
            PlayDestroyParticle();
        }

        protected override void DeactivateObject()
        {
            base.DeactivateObject();
            PlayDestroyParticle();
        }

        void PlayDestroyParticle()
        {
            GameObject hitFX = PoolManager.instance.rocketHitFXPool.GetPooledObject();
            hitFX.transform.position = transform.position;
            hitFX.transform.rotation = transform.rotation;
            hitFX.GetComponent<LifeSpanObject>().Activate();
        }
    }
}