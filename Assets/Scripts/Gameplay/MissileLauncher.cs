using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    public class MissileLauncher : TargetBasedWeapons
    {
        public Transform target;
        public float missileTurnSpeed;
        public float minDrag, maxDrag;

        protected void Start()
        {
            base.Start();
            projectilePool = PoolManager.instance.MissilePool;
        }

        public override bool hasClearShot()
        {
            return true;
        }

        protected override void FireProjectile()
        {
            GameObject projectile = projectilePool.GetPooledObject();
            projectile.transform.position = ProjectilePoint.position;
            projectile.transform.rotation = ProjectilePoint.rotation;
            Missile msl = projectile.GetComponent<Missile>();
            msl.turningSpeed = missileTurnSpeed;
            msl.minDrag = minDrag;
            msl.maxDrag = maxDrag;
            msl.Fire(target, DamageAmt, collIgnore);
            firingEffect.Play();
          }

        protected override void OnAcquiredTarget(Transform target)
        {
            this.target = target;
        }

        protected override void OnLostTarget()
        {
            target = null;
        }
    }
}
