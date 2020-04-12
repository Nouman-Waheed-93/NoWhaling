using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling {
    public class DeathScore : MonoBehaviour {

        public int score;

        // Use this for initialization
        void Start() {
            GetComponent<Health>().onDie.AddListener(AddScore);
        }

        void AddScore()
        {
            ResultScreen.instance.NumJWHDmg += score;
        }
    }
}
