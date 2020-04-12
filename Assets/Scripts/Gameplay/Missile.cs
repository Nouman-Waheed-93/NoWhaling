using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    public class Missile : Projectile
    {
        public float turningSpeed;
        public float minDrag, maxDrag;
        Transform target;
     
        public void Fire(Transform target, int damage, Collider collIgnore)
        {
            this.target = target;
            base.Fire(damage, collIgnore);
        }
        
        private void FixedUpdate()
        {
            base.FixedUpdate();
            if (target == null)
                return;
            rb.drag = Mathf.Lerp(minDrag, maxDrag, Vector3.Dot(rb.velocity.normalized, transform.right));
            rb.AddTorque(Vector3.Cross(transform.forward, (target.position - transform.position).normalized) * turningSpeed);
        }

    }
}