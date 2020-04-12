using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    public class Plane : FluidDynamicVehicle
    {

        public Transform model;
        public Transform propeller;
        public float rotorSpeedMultiplier;
        public float maxBankAngle;
        public float bankSpeed;

        private void Update()
        {
            propeller.Rotate(0, speed * throttle * rotorSpeedMultiplier, 0);
            float tilt = -turn * maxBankAngle;
            model.localRotation = Quaternion.Lerp(model.localRotation, Quaternion.Euler(0, 0, tilt), bankSpeed * Time.deltaTime);
        }
        
    }
}