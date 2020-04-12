using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    public class DeathEffect : MonoBehaviour
    {
        public GameObject deathEffect;

        Health health;

        private void Start()
        {
            health = GetComponent<Health>();
            health.onDie.AddListener(Die);
        }

        void Die()
        {
            (Instantiate(deathEffect, transform.position, transform.rotation)).GetComponent<LifeSpanObject>().Activate();
            Destroy(gameObject);
        }
    }
}
