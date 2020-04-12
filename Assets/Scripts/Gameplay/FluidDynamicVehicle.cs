using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling {
    public class FluidDynamicVehicle : Vehicle {

        public float minDrag, maxDrag;
 
        private void FixedUpdate()
        {
            rb.drag = Mathf.Lerp(minDrag, maxDrag, Vector3.Dot(rb.velocity.normalized, transform.right));
            rb.AddRelativeForce(0, 0, throttle * speed, ForceMode.Acceleration);
            rb.AddRelativeTorque(0, Mathf.Clamp(rb.velocity.magnitude * turnSpeed * turn, -turnSpeed, turnSpeed), 0, ForceMode.Acceleration);
        }
    }
}