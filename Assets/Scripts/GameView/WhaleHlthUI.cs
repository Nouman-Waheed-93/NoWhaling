using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NoWhaling
{
    public class WhaleHlthUI : UIPosIndicator
    {
        public Color HealthyColor, HurtColor;
        public float changeSpeed;

        Image hlthIndicatr;
        protected Health hlth;

        protected void Start()
        {
            hlthIndicatr = GetComponent<Image>();
        }

        public void Init(Transform target, bool IndicateOffScreen, Health hlth)
        {
            Init(target, IndicateOffScreen);
            this.hlth = hlth;
        }
        
        protected void Update()
        {
            hlthIndicatr.fillAmount = Mathf.MoveTowards(hlthIndicatr.fillAmount, hlth.GetHlthinPerOne(), changeSpeed * Time.deltaTime);
            hlthIndicatr.color = Color.Lerp(HurtColor, HealthyColor, hlthIndicatr.fillAmount);
        }
    }
}