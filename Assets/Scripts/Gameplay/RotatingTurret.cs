using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    public class RotatingTurret : AIWeaponController
    {
        public float rotateAngle;
        public float dirResetInterval;
        public float firingPause;

        Vector3 targetPosition;
        int currSign = 1;
        float dirTimer;
        bool fire = true;

        private void Start()
        {
            base.Start();
        }

        private void Update()
        {
            weapon.Fire(fire && target != null && weapon.hasClearShot());
            if (fire && target != null)
            {
                SetTargetRotation();
                AttackTarget();
            }
        }
    
        void AttackTarget()
        {
            weapon.TurnInDirection(targetPosition);
        }

        void SetTargetRotation()
        {
            dirTimer += Time.deltaTime;
            if (dirTimer >= dirResetInterval)
            {
                targetPosition = target.position - transform.position;
                targetPosition = Quaternion.Euler(0, currSign * rotateAngle, 0) * targetPosition;
                currSign *= -1;
                dirTimer = 0;
                StartCoroutine(RestartFiringAfterPause());
            }
        }

        IEnumerator RestartFiringAfterPause()
        {
            float pauseTimer = firingPause;
            fire = false;
            while(pauseTimer > 0)
            {
                pauseTimer -= Time.deltaTime;
                yield return null;
            }
            fire = true;
        }

    }
}
