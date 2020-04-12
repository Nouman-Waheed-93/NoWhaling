using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    public class RocketLauncher : Weapon
    {
        protected void Start()
        {
            projectilePool = PoolManager.instance.RocketPool;
        }

        protected override void FireProjectile()
        {
            GameObject projectile = projectilePool.GetPooledObject();
            projectile.transform.position = ProjectilePoint.position;
            projectile.transform.rotation = ProjectilePoint.rotation;
            projectile.GetComponent<Projectile>().Fire(DamageAmt, collIgnore);
            firingEffect.Play();
        }

        public override bool hasClearShot()
        {
            return true;
        }
    }
}
