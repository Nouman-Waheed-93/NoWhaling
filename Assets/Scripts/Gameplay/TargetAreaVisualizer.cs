using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    public class TargetAreaVisualizer : MonoBehaviour
    {
        public Vector3 offset;
        public Color noTargetColor, gotTargetColor;
        public int circleDetail;

        Transform areaTransform;
        Targeter targeter;
        LineRenderer line;

        void Start()
        {
            targeter = GetComponent<Targeter>();
            CapsuleCollider capsule = GetComponent<CapsuleCollider>();
            areaTransform = Instantiate(Resources.Load<GameObject>("TargetArea")).transform;
            line = areaTransform.GetComponentInChildren<LineRenderer>();
            offset.x = capsule.center.x;
            offset.z = capsule.center.z;
            // areaTransform.localScale = Vector3.one * capsule.radius * 2;
            line.positionCount = circleDetail * (int)capsule.radius;
            for(int i = 0; i < line.positionCount; i++)
            {
                var rad = Mathf.Deg2Rad * (i * 360f / line.positionCount);
                line.SetPosition(i, new Vector3(Mathf.Sin(rad) * capsule.radius, 0, Mathf.Cos(rad) * capsule.radius));
            }
        }
        
        private void Update()
        {
            areaTransform.position = transform.TransformPoint(offset);
            line.startColor = line.endColor = Color.Lerp(gotTargetColor, noTargetColor, targeter.getTargetLockStrength());
        }

        private void OnDestroy()
        {
            if(areaTransform)
                Destroy(areaTransform.gameObject);
        }
    }
}