using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{

    public class DamageHealthBar : WhaleHlthUI
    {

        public float showTimeInterval;

        float showCumTime;

        new void Start()
        {
            base.Start();
            hlth.onDamage.AddListener(Appear);
        }

        new void Update()
        {
            base.Update();
            if (showCumTime < showTimeInterval)
            {
                showCumTime += Time.deltaTime;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        void Appear(int damage)
        {
            gameObject.SetActive(true);
            showCumTime = 0;
        }

    }
}