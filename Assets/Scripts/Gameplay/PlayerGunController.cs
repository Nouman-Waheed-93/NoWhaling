using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace NoWhaling {
    public class PlayerGunController : WeaponController {

        public string gunName;

        private void Start()
        {
            base.Start();
            weapon.FireRate = PlayerPrefs.GetInt(gunName) * 2;
            if (weapon.FireRate < 1)
            {
                weapon.turret.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }

        void Update() {
            //        Turning();
#if UNITY_EDITOR
            MousePosTurn();
#elif UNITY_ANDROID
            MobileTurn();
#endif
        }

        void MobileTurn()
        {
            Vector3 aimV = new Vector3(CrossPlatformInputManager.GetAxis("AimX"), 0, CrossPlatformInputManager.GetAxis("AimY"));
            weapon.Fire(weapon.TurnInDirection(aimV) &&
            aimV.magnitude > 0);
        }

        void MousePosTurn()
        {
            weapon.Fire(Input.GetMouseButton(0) && weapon.TurnTowardsPoint(FloorMousePos.MousePosition()));
        }
    }
}