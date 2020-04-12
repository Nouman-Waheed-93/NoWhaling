using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace NoWhaling
{
    public class PlayerShipController : ShipController
    {

        int dir = 1;

        private void Start()
        {
            base.Start();
            ship.speed = Mathf.Lerp(30, 100, PlayerPrefs.GetInt("Speed")/6);
            ship.turnSpeed = Mathf.Lerp(0.5f, 1, PlayerPrefs.GetInt("Handling") / 6);
            Health hlth = GetComponent<Health>();
            hlth.SetMaxHealth((int)Mathf.Lerp(100, 200, PlayerPrefs.GetInt("Armor") / 6));
            hlth.Heal(200);
        }

        private void Update()
        {
            if (CrossPlatformInputManager.GetButtonUp("Dir"))
                dir *= -1;
        }

        void FixedUpdate()
        {
#if UNITY_EDITOR
            ship.SetThrottle(Input.GetAxis("Vertical") * dir);
            ship.Steer(Input.GetAxis("Horizontal"));
#elif UNITY_ANDROID
            Vector3 inputV = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0, CrossPlatformInputManager.GetAxis("Vertical"));
            ship.SetThrottle(inputV.magnitude * Mathf.Sign(dir));
            inputV = transform.InverseTransformDirection(inputV);
            ship.Steer(Mathf.Clamp(Mathf.Atan2(inputV.x, inputV.z), -1, 1));
#endif
        }
    }
}
