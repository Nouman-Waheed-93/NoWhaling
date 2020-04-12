using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    public class AttackVehicleAI : MonoBehaviour
    {
        public Transform WayPointsParent;
        public AnimationCurve patrollingCurve, attackCurve;

        Transform[] waypoints;
        int currWP;
        protected WayPointVehicleAI ai;
        enum aiState { Patrol, Attack}
        aiState state;
        
    //    Weapon[] weapon;
        protected Transform target;

        protected void Start()
        {
            waypoints = WayPointsParent.GetComponentsInChildren<Transform>();
            //    weapon = GetComponentsInChildren<Weapon>();
            ai = GetComponent<WayPointVehicleAI>();
            Targeter radar = GetComponent<Targeter>();
            radar.onAcquiredTarget.AddListener(AcquiredTarget);
            radar.onLostTarget.AddListener(LoseTarget);
            ai.DScurve = patrollingCurve;
            ai.SetDestination(waypoints[0].position);
        }

        void LoseTarget()
        {
            state = aiState.Patrol;
            ai.DScurve = patrollingCurve;
            target = null;
        }

        void AcquiredTarget(Transform target)
        {
            this.target = target;
            state = aiState.Attack;
            ai.DScurve = attackCurve;
        }
        
        protected void Update()
        {
            if (state == aiState.Attack)
                Attack();
            else if (state == aiState.Patrol)
                Patrol();
        }

        protected virtual void Attack() {
            if (target == null)
            {
                StopAttack();
                return;
            }
            ai.SetDestination(target.position);
        }

        protected void StopAttack()
        {
            state = aiState.Patrol;
            ai.DScurve = patrollingCurve;
        }
        
        protected void Patrol()
        {
            if (ai.arrived)
            {
                currWP++;
                if (currWP >= waypoints.Length)
                    currWP = 0;
                ai.SetDestination(waypoints[currWP].position);
            }
        }
    }
}