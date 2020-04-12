using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    public class ColliisionDamager : MonoBehaviour
    {
        public float damageAmt;

        protected void OnCollisionEnter(Collision collision)
        {
            Health hlth = collision.collider.GetComponent<Health>();
            if(hlth)
                hlth.Damage( (int)(collision.impulse.magnitude * damageAmt));
        }

    }
}