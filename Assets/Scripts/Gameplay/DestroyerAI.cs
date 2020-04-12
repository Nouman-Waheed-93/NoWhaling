using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    public class DestroyerAI : AttackVehicleAI
    {
        public float attackingDistance;

        protected override void Attack()
        {
            if (target == null)
            {
                StopAttack();
                return;
            }
            ai.SetDestination(target.position + target.right * attackingDistance);

        }
    }
}