using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NoWhaling
{
    public class UIWhale : UIPosIndicator
    {
        public Color healthyColor, hurtColor;
        public float pulseSpeed;
      
        Image image;
        
        bool hurt;

        private void Start()
        {
            image = GetComponent<Image>();
        }

        public void Init(Transform target, bool IndicateOffScreen, Health hlth)
        {
            Init(target, IndicateOffScreen);
            hlth.onDamage.AddListener(WhaleHurt);
            hlth.onHealed.AddListener(WhaleHealed);
            hlth.onDie.AddListener(WhaleDead);
        }

        public void WhaleHurt(int damage)
        {
            hurt = true;
            GetComponent<AudioSource>().Play();
        }

        public void WhaleDead()
        {
            Destroy(gameObject);
        }

        public void WhaleHealed()
        {
            hurt = false;
            image.CrossFadeColor(healthyColor, 0.2f, false, false);
        }

        private void Update()
        {
            if (hurt)
                BlinkColor();
        }

        void BlinkColor()
        {
            image.color = Color.Lerp(healthyColor, hurtColor, Mathf.PingPong(Time.time, pulseSpeed));
        }

    }
}