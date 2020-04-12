using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace NoWhaling
{
    public class PlayerMissileLauncher : MonoBehaviour
    {
        MissileLauncher missile;
        // Use this for initialization
        void Start()
        {
            missile = GetComponent<MissileLauncher>();
            int MissileLvl = PlayerPrefs.GetInt("Missile");
            gameObject.SetActive(MissileLvl > 0);
            missile.FireRate = PlayerPrefs.GetInt("Missile") * 0.1f;
        }

        // Update is called once per frame
        void Update()
        {
            missile.Fire(CrossPlatformInputManager.GetButtonDown("Missile"));
        }
    }
}