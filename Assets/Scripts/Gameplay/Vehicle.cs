using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    public abstract class Vehicle : MonoBehaviour
    {
        public float speed, turnSpeed;
        protected Rigidbody rb;
        protected float throttle, turn;

        protected void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void SetThrottle(float throttle) {
            this.throttle = throttle;
        }

        //-1 for left and 1 for right
        public void Steer(float dir) {
            turn = dir;
        }
    }
}