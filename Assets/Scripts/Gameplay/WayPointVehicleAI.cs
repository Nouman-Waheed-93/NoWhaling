using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    public class WayPointVehicleAI : MonoBehaviour
    {
        public float stoppingDistance;
        [HideInInspector]
        public bool arrived;
        [HideInInspector]
        protected Vector3 destination;

        Vehicle vehicle;
        /// <summary>
        /// the distance speed curve
        /// </summary>
        public AnimationCurve DScurve;
        
        public virtual void SetDestination(Vector3 destination)
        {
            this.destination = destination;
        }
        
        protected virtual void Start()
        {
            vehicle = GetComponent<Vehicle>();
        }

        // Update is called once per frame
        protected void Update()
        {
            MoveToDestination();
        }
        
        protected virtual void MoveToDestination()
        {
            float remainingDistance = GlobalFunctions.DistanceOnHorizontalPlane
                (transform.position, destination);
            float requiredSpeed = Mathf.Clamp(DScurve.Evaluate(remainingDistance), 0, 1);
            vehicle.SetThrottle(requiredSpeed);

            Vector3 localTarget = transform.InverseTransformPoint(destination);
            float targetAngle = Mathf.Atan2(localTarget.x, localTarget.z);
            vehicle.Steer(Mathf.Clamp(targetAngle, -1, 1));
            arrived = remainingDistance <= stoppingDistance;
        }
    }
}