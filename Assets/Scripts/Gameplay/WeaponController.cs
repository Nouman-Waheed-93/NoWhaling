using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    [RequireComponent (typeof(Weapon))]
    public class WeaponController : MonoBehaviour
    {
        protected Weapon weapon;

        protected void Start()
        {
            weapon = GetComponent<Weapon>();
        }

    }
}
