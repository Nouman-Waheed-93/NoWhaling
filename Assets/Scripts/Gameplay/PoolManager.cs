using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager instance;
        
        public PoolHandler MGBulletPool;
        public PoolHandler HarpoonPool;
        public PoolHandler RocketPool;
        public PoolHandler MissilePool;
        public PoolHandler bulletHitFXPool;
        public PoolHandler rocketHitFXPool;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
                Destroy(gameObject);
        }

    }
}
