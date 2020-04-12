using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace NoWhaling
{
    public class NavMeshVehicle : WayPointVehicleAI
    {
        NavMeshAgent nav;

        public void Escape()
        {
            SetDestination(new Vector3(1000,0, 1000));
        }

        public override void SetDestination(Vector3 destination)
        {
            if (nav == null)
                nav = GetComponent<NavMeshAgent>();
            nav.SetDestination(destination);
        }

        protected override void Start()
        {
            base.Start();
            nav = GetComponent<NavMeshAgent>();
        }

        protected override void MoveToDestination()
        {
            Debug.Log("Trying to move " + gameObject.name);
            if (nav.path.corners.Length > 1)
            {
                destination = nav.path.corners[1];
            }
            arrived = nav.remainingDistance <= nav.stoppingDistance;
        }
    }
}