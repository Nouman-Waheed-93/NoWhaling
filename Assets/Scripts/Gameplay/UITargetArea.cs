using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NoWhaling
{
    public class UITargetArea : UIPosIndicator
    {
        Targeter targeter;
        Image image;
       
        public void Init(Transform target, bool IndicateOffScreen, Vector3 offset, Vector2 size, Targeter targeter)
        {
            Init(target, IndicateOffScreen, offset);
            this.targeter = targeter;
            GetComponent<Image>().rectTransform.sizeDelta = size;
            targeter.onAcquiredTarget.AddListener(FoundTarget);
            targeter.onLostTarget.AddListener(LostTarget);
        }

        // Use this for initialization
        void Start()
        {
            image = GetComponent<Image>();
        }

        void FoundTarget(Transform target)
        {
            image.CrossFadeColor(Color.red, targeter.TrgtAcquireTime, false, false);
        }

        void LostTarget()
        {
            image.CrossFadeColor(Color.white, 0.5f, false, false);
        }

    }
}