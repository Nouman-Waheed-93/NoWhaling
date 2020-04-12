using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NoWhaling
{
    public class Targeter : MonoBehaviour
    {
        public Transform target { get; private set; }
        public string targetTag;
        public float TrgtAcquireTime;
    
        [HideInInspector]
        public TransformEvent onAcquiredTarget = new TransformEvent();
        [HideInInspector]
        public UnityEvent onLostTarget;
        List<Transform> targetsInRange = new List<Transform>();
        public Vector2 XAngleRange = new Vector2(0, 0);
        public Vector2 YAngleRange = new Vector2(0, 0);

        float acquireTimer;
        bool hasTarget;

        private void Start()
        {
            acquireTimer = TrgtAcquireTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.isTrigger && other.CompareTag(targetTag))
            {
                targetsInRange.Add(other.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.isTrigger && other.CompareTag(targetTag))
            {
                targetsInRange.Remove(other.transform);
                if (target == other.transform)
                {
                    acquireTimer = TrgtAcquireTime;
                    onLostTarget.Invoke();
                    acquireTimer = TrgtAcquireTime;
                    target = null;
                }
            }
        }
        
        public void DeselectCurrTrgt()
        {
            acquireTimer = TrgtAcquireTime;
            targetsInRange.Remove(target);
            target = null;
            onLostTarget.Invoke();
        }

        protected virtual void Update()
        {

            if (target == null && targetsInRange.Count > 0)
            {
                acquireTimer -= Time.deltaTime;
                if (acquireTimer <= 0.0f)
                {
                    target = GetNearestTarget();
                    if (target != null)
                    {
                        onAcquiredTarget.Invoke(target);
                        hasTarget = true;
                    }
                    else if (hasTarget)
                    {
                        hasTarget = false;
                        onLostTarget.Invoke();
                    }
                }
            }
            if(target != null)
            {
                Vector3 dir = target.position - transform.position;
                dir = transform.InverseTransformDirection(dir);
                float XAngle = Mathf.Rad2Deg * Mathf.Atan2(dir.x, dir.z);
                float YAngle = Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.z);
                if (XAngle < XAngleRange.x && XAngle > XAngleRange.y)
                    DeselectCurrTrgt();
                if (YAngle < YAngleRange.x && YAngle > YAngleRange.y)
                    DeselectCurrTrgt();
            }
        }

        protected virtual Transform GetNearestTarget()
        {
            int length = targetsInRange.Count;

            if (length == 0)
            {
                return null;
            }

            Transform nearest = null;
            float distance = float.MaxValue;
            for (int i = length - 1; i >= 0; i--)
            {
                Transform targetable = targetsInRange[i];
                if (targetable == null)
                {
                    targetsInRange.RemoveAt(i);
                    continue;
                }
                float currentDistance = Vector3.Distance(transform.position, targetable.position);
                if (currentDistance < distance)
                {
                    Vector3 dir = targetable.position - transform.position;
                    dir = transform.InverseTransformDirection(dir);
                    float XAngle = Mathf.Rad2Deg * Mathf.Atan2(dir.x, dir.z);
                    float YAngle = Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.z);

                    //if ((XAngle > XAngleRange.x && XAngle < XAngleRange.y) &&
                    //    (YAngle > YAngleRange.x && YAngle < YAngleRange.y)
                    //)
                    {
                        distance = currentDistance;
                        nearest = targetable;
                    }
                }
            }

            return nearest;
        }

        public float getTargetLockStrength()
        {
            return acquireTimer / TrgtAcquireTime;
        }
    }
}
