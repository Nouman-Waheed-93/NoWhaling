using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling {
    public class WhaleTargeter : MonoBehaviour {
        public static WhaleTargeter singleton;

        List<WhaleBehaviour> whales = new List<WhaleBehaviour>();
        int currTargetInd = -1;

        // Use this for initialization
        void Start() {
            singleton = this;
            whales.AddRange(FindObjectsOfType<WhaleBehaviour>());
        }

        public WhaleBehaviour getNextTarget()
        {
            if (whales.Count < 1)
                return null;
            currTargetInd++;
            if(currTargetInd >= whales.Count) {
                currTargetInd = 0;
            }
            return whales[currTargetInd];
        }

        public void WhaleDead(WhaleBehaviour whale)
        {
            whales.Remove(whale);
        }

    }
}
