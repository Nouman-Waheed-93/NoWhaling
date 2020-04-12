using UnityEngine;

namespace NoWhaling
{
    public abstract class ShipController : MonoBehaviour
    {

        protected FluidDynamicVehicle ship;

        protected void Start()
        {
            ship = GetComponent<FluidDynamicVehicle>();
        }

    }
}