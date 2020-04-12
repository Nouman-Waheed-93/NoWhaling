using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    public class Helicopter : Vehicle
    {
        public Transform model;
        public Transform rotor;
        public float rotorSpeedMultiplier;
        public float minTilt, maxTilt;

        private void Start()
        {
            base.Start();
        }

        private void Update()
        {
            rotor.Rotate(0, speed * throttle * rotorSpeedMultiplier, 0);
            float tilt = Mathf.Lerp(minTilt, maxTilt, throttle);
            model.localRotation = Quaternion.Lerp(model.localRotation, Quaternion.Euler(tilt, 0, 0), Time.deltaTime);
        }

        private void FixedUpdate()
        {
            rb.AddRelativeForce(0, 0, throttle * speed);
            rb.AddRelativeTorque(0, turnSpeed * turn, 0);
        }
    }
}
