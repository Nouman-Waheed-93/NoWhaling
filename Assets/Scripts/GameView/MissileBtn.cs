using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NoWhaling;
using UnityEngine.UI;

namespace GameView
{
    public class MissileBtn : MonoBehaviour
    {
        Image img;
        public Animator anim;
        public MissileLauncher msl;
        int targetID;
        // Use this for initialization
        void Start()
        {
            img = GetComponent<Image>();
            msl = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<MissileLauncher>();
            if (!msl)
                return;
            msl.targeter.onAcquiredTarget.AddListener(FoundTarget);
            msl.targeter.onLostTarget.AddListener(LostTarget);
            targetID = Animator.StringToHash("target");
        }

        private void OnEnable()
        {
            if (img && img.color == Color.white)
                anim.SetBool(targetID, true);
        }

        public void FoundTarget(Transform target)
        {
            img.color = Color.white;
            anim.SetBool(targetID, true);
        }

        public void LostTarget()
        {
            img.color = Color.red;
            anim.SetBool(targetID, false);
        }

        // Update is called once per frame
        void Update()
        {
            if(msl)
                img.fillAmount = msl.GetFireTimerPerOne();
        }
    }
}