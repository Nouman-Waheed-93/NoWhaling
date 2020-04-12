using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling {
    public class WhaleGroup : MonoBehaviour {

        public List<WhaleBehaviour> whales;
        public Transform WayPointParent;

        int currLeader;
        int currWP;
        Transform[] waypoints;

        // Use this for initialization
        void Start() {
            waypoints = WayPointParent.GetComponentsInChildren<Transform>();
            for(int i =0; i < whales.Count; i++)
            {
                whales[i].WPNotFound.AddListener(ChooseNewLeader);
            }
            ChooseNewLeader();
        }
        
        void MoveToNextWP()
        {
            currWP++;
            if (currWP >= waypoints.Length)
                currWP = 0;
            whales[currLeader].waypointParent = waypoints[currWP];
            Debug.Log("Destingatio set");
        }

        void ChooseNewLeaderAdapter(int damgAmt) {
            ChooseNewLeader();
        }

        void ChooseNewLeader()
        {
            if(whales.Count > currLeader)
                whales[currLeader].OnDestinationReached.RemoveListener(MoveToNextWP);

            for (int i = 0; i < whales.Count; i++)
            {
                if (!whales[i])
                    whales.RemoveAt(i);
                else if(whales[i].GetComponent<Health>().isHealthy)
                {
                    currLeader = i;
                }
            }

            if (whales.Count <= currLeader)
                return;

            whales[currLeader].waypointParent = waypoints[currWP];
            whales[currLeader].GetComponent<Health>().onDamage.AddListener(ChooseNewLeaderAdapter);
            whales[currLeader].OnDestinationReached.AddListener(MoveToNextWP);
            for(int i = 0; i < whales.Count; i++)
            {
                if(i != currLeader)
                    whales[i].waypointParent = whales[currLeader].transform;
            }
            Debug.Log("Sada nwew lader " + whales[currLeader].gameObject.name);
        }

    }
}