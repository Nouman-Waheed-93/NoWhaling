using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    [RequireComponent (typeof (Targeter))]
    public class AIWeaponController : WeaponController
    {
        Targeter targeter;
        protected Transform target;
        protected void Start()
        {
            base.Start();
            targeter = GetComponent<Targeter>();
            targeter.onAcquiredTarget.AddListener(AcquiredTarget);
            targeter.onLostTarget.AddListener(LostTarget);
        }

        private void Update()
        {
            weapon.Fire(target!= null && weapon.hasClearShot());
            if (target != null)
            {
                PointWeaponAtTarget();
            }
        }
        
        void PointWeaponAtTarget()
        {
            weapon.TurnTowardsPoint(target.position);
        }

        void AcquiredTarget(Transform target) {
            this.target = target;   
        }

        void LostTarget() {
            target = null;
        }
    }
}