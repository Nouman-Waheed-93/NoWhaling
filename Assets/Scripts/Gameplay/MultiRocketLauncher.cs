using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    public class MultiRocketLauncher : RocketLauncher
    {

        Transform[] projectilePoints;
        
        // Use this for initialization
        void Start()
        {
            base.Start();
            projectilePoints = ProjectilePoint.GetComponentsInChildren<Transform>();
        }

        protected override void FireProjectile()
        {
            for (int i = 0; i < projectilePoints.Length; i++)
            {
                GameObject projectile = projectilePool.GetPooledObject();
                projectile.transform.position = projectilePoints[i].position;
                projectile.transform.rotation = projectilePoints[0].rotation;
                projectile.GetComponent<Projectile>().Fire(DamageAmt, collIgnore);
            }
            firingEffect.Play();
        }
    }
}
