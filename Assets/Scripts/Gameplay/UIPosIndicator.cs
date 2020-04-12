using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NoWhaling
{
    public class UIPosIndicator : MonoBehaviour
    {
        public float screenMargin;
        public bool dstryOnTrgtLost;
        public bool disapprInVisRng;
        public float minSize, maxSize;
        public float referenceDistance;
        protected Transform target;
        
        bool IndicateOffScreen;
        Vector3 offset;
        

        public void Init(Transform target)
        {
            this.target = target;
        }

        public void Init(Transform target, bool IndicateOffScreen)
        {
            Init(target);
            this.IndicateOffScreen = IndicateOffScreen;
        }

        public void Init(Transform target, bool IndicateOffScreen, Vector3 offset)
        {
            Init(target, IndicateOffScreen);
            this.offset = offset;
        }
        
        void LateUpdate()
        {
            if (!target)
            {
                if (dstryOnTrgtLost)
                    Destroy(gameObject);
                else
                    gameObject.SetActive(false);
                return;
            }

            transform.position = target.TransformPoint(offset);

            if (IndicateOffScreen) { 
                Vector3 position = transform.position;
                Vector3 point = Camera.main.WorldToScreenPoint(position);
                
                bool isOffscreen = false;
                if (point.x > Screen.width - screenMargin)
                {
                    isOffscreen = true;
                    point.x = Screen.width - screenMargin;
                }
                if (point.x < 0 + screenMargin)
                {
                    isOffscreen = true;
                    point.x = 0 + screenMargin;
                }
                if (point.y > Screen.height - screenMargin)
                {
                    isOffscreen = true;
                    point.y = Screen.height - screenMargin;
                }
                if (point.y < 0 + screenMargin)
                {
                    isOffscreen = true;
                    point.y = 0 + screenMargin;
                }
                
                point.z = 10;
                point = Camera.main.ScreenToWorldPoint(point);
                transform.position = point;
                point -= Camera.main.transform.position;

                transform.localScale = Vector3.Lerp(Vector3.one * minSize, Vector3.one * maxSize,
                    referenceDistance / Vector3.Distance(transform.position, target.position));

                //transform.eulerAngles = new Vector3(-90, Mathf.Atan2(point.z, point.x) * Mathf.Rad2Deg - 90, 0);

                //   transform.GetChild(2).position = transform.position + new Vector3(0, -0.8f, 0);
                //  transform.GetChild(2).eulerAngles = new Vector3(0, 0, 0);
                if (disapprInVisRng)
                {
                    if (isOffscreen)
                    {
                        GetComponent<Image>().CrossFadeAlpha(1, 0.1f, false);
                    }
                    else
                    {
                        GetComponent<Image>().CrossFadeAlpha(0, 0.1f, false);
                    }
                }
            }
            transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
        }
    }
}