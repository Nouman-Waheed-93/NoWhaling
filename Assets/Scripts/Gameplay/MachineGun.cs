using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    public class MachineGun : Weapon
    {
        public LayerMask bulletHitLayer;
        public float maxDistance;

        void Start() {
            projectilePool = PoolManager.instance.MGBulletPool;
        }
        
        void PlayParticles(Vector3 origin, Quaternion lookRotation)
        {
            if (firingEffect == null)
            {
                return;
            }
            firingEffect.transform.position = origin;
            firingEffect.transform.rotation = lookRotation;
            firingEffect.Play();
        }

        public override bool hasClearShot()
        {
            return true;
        }

        protected override void FireProjectile()
        {
            RaycastHit hit;
            GameObject projectile = projectilePool.GetPooledObject();
            projectile.transform.position = ProjectilePoint.position;
            projectile.transform.rotation = ProjectilePoint.rotation;
            if (Physics.Raycast(ProjectilePoint.position, ProjectilePoint.forward, out hit, maxDistance, bulletHitLayer))
            {
                projectile.GetComponent<Bullet>().Fire(Vector3.Distance(ProjectilePoint.position, hit.point));
                Health hitHlth = hit.collider.GetComponent<Health>();
                if(hitHlth)
                    hitHlth.Damage(DamageAmt);
            }
            else
                projectile.GetComponent<Bullet>().Fire(maxDistance);
            PlayParticles(ProjectilePoint.position, ProjectilePoint.rotation);
        }
    }
}
