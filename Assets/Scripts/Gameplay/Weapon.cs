using ActionGameFramework.Audio;
using UnityEngine;

namespace NoWhaling
{
    public abstract class Weapon : MonoBehaviour
    {
        public Transform ProjectilePoint;
        public float FireRate;
        public int DamageAmt;
        public Collider collIgnore;
        public RandomAudioSource randomAudioSource;
        public Transform turret;
        public float turningSpeed;
        public Vector2 turretXRotationRange = new Vector2(-45, 45);
        public Vector2 turretYRotationRange = new Vector2(-45, 45);
        public bool OnlyYRotation;
        public ParticleSystem firingEffect;
     
        protected PoolHandler projectilePool;
        protected float m_FireTimer;
        protected bool firing;
        
        protected virtual void Update()
        {
            m_FireTimer -= Time.deltaTime;
            if (firing && m_FireTimer <= 0.0f)
            {
                FireProjectile();
                if (randomAudioSource != null)
                {
                    randomAudioSource.PlayRandomClip();
                }
                m_FireTimer = 1 / FireRate;
            }
        }
        
        public bool TurnTowardsPoint(Vector3 point)
        {
            //  Vector3 localDir = transform.InverseTransformDirection(point - turret.position);
            Vector3 localDir = point - turret.position;
            return Turn(localDir);
        }

        public bool TurnInDirection(Vector3 dir)
        {
           // Vector3 localDir = transform.InverseTransformDirection(dir);
            return Turn(dir);
        }

        bool Turn(Vector3 localDir)
        {
           if (OnlyYRotation)
            {
                localDir.y = 0;
            }
            Quaternion look = Quaternion.LookRotation(localDir, Vector3.up);
            look = Quaternion.Inverse(turret.parent.rotation) * look;
            Vector3 lookEuler = look.eulerAngles;
            // We need to convert the rotation to a -180/180 wrap so that we can clamp the angle with a min/max
            float x = GlobalFunctions.Wrap180(lookEuler.x);
            lookEuler.x = Mathf.Clamp(x, turretXRotationRange.x, turretXRotationRange.y);
            float y = GlobalFunctions.Wrap180(lookEuler.y);
            lookEuler.y = Mathf.Clamp(y, turretYRotationRange.x, turretYRotationRange.y);
            look.eulerAngles = lookEuler;
            turret.localRotation = Quaternion.RotateTowards(turret.localRotation, look, Time.deltaTime * turningSpeed);
            return y == lookEuler.y;
        }

        public abstract bool hasClearShot();

        public void Fire(bool fire)
        {
            firing = fire;
        }

        public float GetFireTimerPerOne()
        {
            float fireSpeed = 1 / FireRate;
            return (fireSpeed - m_FireTimer) / fireSpeed;
        }

        protected virtual void FireProjectile() {
            GameObject projectile = projectilePool.GetPooledObject();
            projectile.transform.position = ProjectilePoint.position;
            projectile.transform.rotation = ProjectilePoint.rotation;
        }
        
    }
}