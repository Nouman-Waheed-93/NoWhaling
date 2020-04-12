using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoWhaling
{
    public class TargetHUD : MonoBehaviour
    {
        Transform target;
        UIPosIndicator hudOB;
        // Use this for initialization
        void Start()
        {
            Targeter targeter = GetComponent<Targeter>();
            targeter.onAcquiredTarget.AddListener(FoundTarget);
            hudOB = WorldUI.instance.CreateTargetHUD(transform);
            hudOB.gameObject.SetActive(false);
        }

        void FoundTarget(Transform target)
        {
            hudOB.Init(target);
            hudOB.gameObject.SetActive(true);
        }

        void LostTarget()
        {
            hudOB.gameObject.SetActive(false);
        }

    }
}