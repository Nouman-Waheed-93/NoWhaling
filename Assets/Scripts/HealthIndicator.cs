using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    public class HealthIndicator : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            WorldUI.instance.CreateHelthIndicator(transform);
        }

    }
}