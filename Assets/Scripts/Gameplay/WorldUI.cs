using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    public class WorldUI : MonoBehaviour
    {
        public static WorldUI instance;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            CameraToggle.instance.AddgameCamObject(gameObject);
        }

        public void CreateWhalePointer(Transform targetTransform)
        {
            Instantiate(Resources.Load<GameObject>("WhaleIndicator"), transform).GetComponent<UIWhale>().Init(targetTransform,
                true, targetTransform.GetComponent<Health>());
        }

        public void CreateShipPointer(Transform targetTransform)
        {
            Instantiate(Resources.Load<GameObject>("ShipIndicator"), transform).GetComponent<UIPosIndicator>().Init(targetTransform,
                true);
        }

        public GameObject CreateHelthIndicator(Transform targetTransform)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("HlthInd"), transform);
            go.GetComponent<WhaleHlthUI>().Init(targetTransform,
              false, targetTransform.GetComponent<Health>());
            return go;
        }

        public GameObject CreateWhaleHealth(Transform targetTransform)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("WhaleHlth"), transform);
            go.GetComponent<WhaleHlthUI>().Init(targetTransform,
              false, targetTransform.GetComponent<Health>());
            return go;
        }

        public UIPosIndicator CreateTargetHUD(Transform targetTransform)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("TargetBox"), transform);
            UIPosIndicator hudOB = go.GetComponent<UIPosIndicator>();
            hudOB.Init(targetTransform, true);
            return hudOB;
        }

    }
}
