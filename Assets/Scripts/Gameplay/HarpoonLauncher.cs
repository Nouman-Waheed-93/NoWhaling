using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NoWhaling
{
    public class HarpoonLauncher : TargetBasedWeapons
    {
        public Transform[] whaleTyingPoints;
        public LayerMask harpoonMask;
        /// <summary>
        /// time required to tie a whale after harpooning
        /// </summary>
        public float tieWhaleSpeed;
        public float waitTImebfrPull;
        public float retargetSpeed;
        [HideInInspector]
        public UnityEvent GotEnoughFishes;
        public bool enoughFish;
       
        public WhaleBehaviour targetWhale;
        LineRenderer rope;
        int shipLayer;
        bool whaleInRange;

        void Start()
        {
            base.Start();
            projectilePool = PoolManager.instance.HarpoonPool;
            shipLayer = LayerMask.GetMask("Ship");
            rope = ProjectilePoint.GetComponent<LineRenderer>();
            rope.enabled = false;
        }

        protected override void FireProjectile()
        {
            GameObject projectile = projectilePool.GetPooledObject();
            projectile.transform.position = ProjectilePoint.position;
            projectile.transform.rotation = ProjectilePoint.rotation;
            float dist = Vector3.Distance(ProjectilePoint.position, targetWhale.transform.position);
            Invoke("WhaleHarpooned", projectile.GetComponent<Bullet>().Fire(dist));
        }

        void WhaleHarpooned()
        {
            if (targetWhale)
            {
                targetWhale.GetComponent<Health>().Damage(DamageAmt);
                targetWhale.GetComponent<Health>().onHealed.AddListener(HarpoonRopeCut);
                StartCoroutine(pullWhale());
            }
        }

        void HarpoonRopeCut()
        {
            StartCoroutine("RopeCutRetarget");
        }

        IEnumerator RopeCutRetarget()
        {
            whaleInRange = false;
            yield return new WaitForSeconds(retargetSpeed);
            whaleInRange = true;
        }

        public void SetTarget(WhaleBehaviour trgt)
        {
            targetWhale = trgt; 
        }

        protected override void OnAcquiredTarget(Transform target)
        {
            if (enoughFish)
                return;

            if (targetWhale == target.GetComponent<WhaleBehaviour>())
            {
                whaleInRange = true;
            }
            else
            {
                targeter.DeselectCurrTrgt();
            }
        }

        protected override void OnLostTarget()
        {
            if (!targetWhale)
                return;

            whaleInRange = false;
            targetWhale.GetComponent<Health>().onHealed.RemoveListener(HarpoonRopeCut);
       }

        public override bool hasClearShot()
        {
            if (!whaleInRange && targetWhale == null)
                return false;
            RaycastHit hit;
            bool hitbool;
            hitbool = Physics.Raycast(ProjectilePoint.position,
            ProjectilePoint.forward, out hit, 1000, harpoonMask);
            return targetWhale.isOnSurface && hitbool && hit.collider.gameObject == targetWhale.gameObject;
        }

        IEnumerator pullWhale()
        {
            float waitTime = waitTImebfrPull;
            rope.enabled = true;
            rope.SetPosition(1, ProjectilePoint.InverseTransformPoint(targetWhale.transform.position));

            while (waitTime > 0 && whaleInRange)
            {
                waitTime -= Time.deltaTime;
                yield return null;
            }
            if (whaleInRange)
            {
                float pullTime = Vector3.Distance(targetWhale.transform.position, ProjectilePoint.position) / tieWhaleSpeed;
                while (pullTime > 0 && whaleInRange)
                {
                    pullTime -= Time.deltaTime;
                    Vector3 newPos = Vector3.MoveTowards(targetWhale.transform.position, ProjectilePoint.position, Time.deltaTime * tieWhaleSpeed);
                    newPos.y = targetWhale.transform.position.y;
                    targetWhale.transform.position = newPos;
                    rope.SetPosition(1, ProjectilePoint.InverseTransformPoint(targetWhale.transform.position));
                    yield return null;
                }
                if (whaleInRange)
                    TieWhale();
            }
            rope.enabled = false;
        }

        void TieWhale()
        {
            targetWhale.GetComponent<WhaleBehaviour>().Tie(whaleTyingPoints[0]);
            targeter.DeselectCurrTrgt();
            GotEnoughFishes.Invoke();
            enoughFish = true;
        }
    }
}