using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    [RequireComponent(typeof (Targeter))]
    public abstract class TargetBasedWeapons : Weapon
    {
        [SerializeField]
        public Targeter targeter;

        protected void Start()
        {
            targeter = GetComponent<Targeter>();
            targeter.onAcquiredTarget.AddListener(OnAcquiredTarget);
            targeter.onLostTarget.AddListener(OnLostTarget);
        }

        void GotTarget(Transform target)
        {
            OnAcquiredTarget(target);
        }

        void LostTarget()
        {
            OnLostTarget();
        }

        protected abstract void OnAcquiredTarget(Transform target);

        protected abstract void OnLostTarget();
    }
}
