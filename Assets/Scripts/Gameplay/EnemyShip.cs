using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling {
    public class EnemyShip : ShipController {
        public float attackingDistance;
        public float offensiveTime;
        public float idlingTime;
        /// <summary>
        /// larger value slows speed earlier
        /// </summary>
        public float distanceSpeedMultiplier;

        private float offensiveTimeCount;
        private float idlingTimeCount;
        private float desiredShipSpeed;
        private float desiredTurnSpeed;

        protected Transform target;
        Vector3 destination;
        enum BehaviourStates { offensive, idling }
        BehaviourStates currState = BehaviourStates.offensive;

        private void Update()
        {
            if (currState == BehaviourStates.offensive)
            {
                OffensiveBehaviour();
            }
            else if (currState == BehaviourStates.idling)
            {
                Idle();
            }
        }

        void Idle()
        {

        }
        
        void OffensiveBehaviour()
        {
            if (offensiveTimeCount >= offensiveTime)
            {
                currState = BehaviourStates.idling;
                idlingTimeCount = 0;
            }
            else
            {
                offensiveTimeCount += Time.deltaTime;
                if (target)
                {
                    desiredShipSpeed = Mathf.Clamp01((GlobalFunctions.DistanceOnHorizontalPlane(transform.position, target.position)
                        - attackingDistance) * distanceSpeedMultiplier);
                    Vector3 localTarget = transform.InverseTransformPoint(target.position);
                    float targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
                    desiredTurnSpeed = Mathf.Clamp(targetAngle, -1, 1);
                }
                else
                    SelectTarget();
             }
        }
        
        protected virtual void SelectTarget()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void FixedUpdate()
        {
            ship.SetThrottle(desiredShipSpeed);
            ship.Steer(desiredTurnSpeed);
        }

    }
}