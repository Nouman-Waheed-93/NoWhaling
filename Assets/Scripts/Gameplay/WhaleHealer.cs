using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    public class WhaleHealer : MonoBehaviour
    {
        public Health targetHealth;
        public float healingTime;

        float healTimer;

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Whale"))
            {
               if (targetHealth == null || targetHealth.isHealthy || targetHealth.isDead )
                {
                    Health hlth = other.GetComponent<Health>();
                    if (hlth != null && !hlth.isHealthy && !hlth.isDead)
                    {
                        targetHealth = hlth;
                    }
                }
               else if(targetHealth == other.GetComponent<Health>())
                {
                    healTimer += Time.deltaTime;
                    if (healTimer > healingTime)
                    {
                        Debug.Log("healing");
                        targetHealth.Heal(1);
                        healTimer = 0;
                    }
                }
            }   
        }

        private void OnTriggerExit(Collider other)
        {
            targetHealth = null;
        }
    }
}