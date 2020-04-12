using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NoWhaling {
    public class WhaleBehaviour : MonoBehaviour {

        public Transform waypointParent;
        public Vector3 offset;
        public float stoppingDistance;
        public float movementSpeed;
        public float depthMoveSpeed;
        public float turnSpeed;
        public float turnDampTime;
        public float swimmingDepth;// y position of normal swim
        public float breathingDepth;// y position of breathing swim
        public float breathTime;// the amount of time whale can hold its breath underwater
        public float breathingTime;// the amount of time the whale will breath at the surface
        public float bleedOffSpeed;
        public Transform tyingPoint;
        public bool isOnSurface;
        public LayerMask obstacleMask;
        public float obstacleAvoidanceDistance;
        public float obstcleDtctionFrqncy;
        public float mass;
        public float drag;
        public float angularDrag;
        public RigidbodyConstraints constraints;
        [HideInInspector]
        public UnityEvent OnDestinationReached = new UnityEvent();
        public UnityEvent WPNotFound = new UnityEvent();
  
        Vector3 currDestination;
        Animator anim;
        Health health;
        float cumBreathTime, cumBreathingTime, cumSurfaceTime;
        int speedID = Animator.StringToHash("ForwardSpeed");
        int turnID = Animator.StringToHash("TurnSpeed");
        int hurtID = Animator.StringToHash("hurt");
        int currWP;
        float targetHeight;
        enum BehaviourStates { Swimming, Breathing, Hurt, MovingToSurface}
        BehaviourStates currState;
        float bleedOffAmt;
        GameObject uiHlth;
        float chngDstnTimer;
        Rigidbody rb;

        public void Tie(Transform Anchor)
        {
            Destroy(rb);
            tyingPoint.SetParent(Anchor);
            transform.SetParent(tyingPoint);
            tyingPoint.localPosition = Vector3.zero;
            tyingPoint.localRotation = Quaternion.identity;
            anim.SetTrigger("Tie");
        }

        public void UnTie()
        {
            transform.SetParent(null);
            tyingPoint.SetParent(transform);
            transform.rotation = Quaternion.identity;
            Vector3 newPos = transform.position;
            newPos.y = breathingDepth;
            transform.position = newPos;
            rb = gameObject.AddComponent<Rigidbody>();
            rb.mass = mass;
            rb.drag = drag;
            rb.angularDrag = angularDrag;
            rb.useGravity = false;
            rb.constraints = constraints;
            Debug.Log("Untied");
        }
        
        // Use this for initialization
        void Start() {
            anim = GetComponent<Animator>();
            anim.SetFloat("offset", Random.Range(0f, 1f));
            health = GetComponent<Health>();
            rb = GetComponent<Rigidbody>();
            GameManager.instance.RegisterFish(this);
            WorldUI.instance.CreateWhalePointer(transform);
            health.onDamage.AddListener(GetHurt);
            health.onDie.AddListener(Die);
            health.onHealed.AddListener(Healed);
            currState = BehaviourStates.Swimming;
            cumBreathTime = breathTime;
            currDestination = (waypointParent.TransformPoint(offset));
        }

        // Update is called once per frame
        void FixedUpdate() {
            if (currState == BehaviourStates.Swimming) {
                Swim();
            }
            else if (currState == BehaviourStates.Breathing) {
                TakeABreath();
            }
            else if(currState == BehaviourStates.Hurt) {
                HurtBehaviour();
            }
            else if(currState == BehaviourStates.MovingToSurface){
                MoveToSurface();
            }
        }

        void Swim() {
            cumBreathTime -= Time.deltaTime;
            Move();
            ChangeDestination();
            if (cumBreathTime <= 0)
            {
                targetHeight = breathingDepth;
                currState = BehaviourStates.MovingToSurface;
           }
       
        }

        void MoveToSurface()
        {
            Move();
            ChangeDestination();
            cumSurfaceTime -= Time.deltaTime;
            if (cumSurfaceTime <= 0)
            {
                cumBreathingTime = breathingTime;
                currState = BehaviourStates.Breathing;
                isOnSurface = true;
            }
        }
        
        bool ChangeDestination()
        {

            if (!waypointParent)
            {
                WPNotFound.Invoke();
                return false;
            }
            if (GlobalFunctions.DistanceOnHorizontalPlane(transform.position, currDestination) <= stoppingDistance)
            {
                currDestination = (waypointParent.TransformPoint(offset));
                OnDestinationReached.Invoke();
                return true;
            }
            return false;
        }
        
        void Move() {
            rb.MovePosition(transform.position + transform.forward * movementSpeed * Time.deltaTime);
            rb.MovePosition(Vector3.Lerp(rb.position,
                new Vector3(rb.position.x, targetHeight, rb.position.z), depthMoveSpeed * Time.deltaTime));
            anim.SetFloat(speedID, 1);
            Vector3 targetDir = currDestination - transform.position;
            targetDir.y = 0;
            Debug.DrawRay(transform.position, targetDir, Color.red);
            Vector3 DirInLocal = transform.InverseTransformDirection(targetDir);
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, Quaternion.LookRotation(targetDir), turnSpeed));
            anim.SetFloat(turnID, Mathf.Atan2(DirInLocal.x, DirInLocal.z) , turnDampTime, Time.deltaTime);
        }

        void TakeABreath() {
            cumBreathingTime -= Time.deltaTime;
            chngDstnTimer += Time.deltaTime;
            Move();
            ChangeDestination();
            if (chngDstnTimer > obstcleDtctionFrqncy && Physics.Raycast(transform.position, transform.forward, obstacleAvoidanceDistance, obstacleMask))
            {
                Debug.Log("Agay arehay ho" + gameObject.name);
                currDestination.x = transform.position.x-(transform.forward.x * obstacleAvoidanceDistance);
                currDestination.z = transform.position.z-(transform.forward.z * obstacleAvoidanceDistance);
                currDestination.y = targetHeight;
                Debug.DrawLine(transform.position, currDestination, Color.magenta, 10);
                chngDstnTimer = 0;
            }
            if (cumBreathingTime <= 0)
            {
                currState = BehaviourStates.Swimming;
                isOnSurface = false;
                targetHeight = swimmingDepth;
                cumBreathTime = breathTime;
            }
        }

        void HurtBehaviour() {
            bleedOffAmt += bleedOffSpeed * Time.deltaTime;
            if (bleedOffAmt > 1)
            {
                health.BleedOffHealth((int)bleedOffAmt);
                bleedOffAmt = 0;
            }
        }

        void GetHurt(int damage) {
            currState = BehaviourStates.Hurt;
            if(uiHlth == null)
                uiHlth = WorldUI.instance.CreateWhaleHealth(transform);
            anim.SetTrigger(hurtID);
        }

        public void Die() {
            anim.SetTrigger("Tie");
            WhaleTargeter.singleton.WhaleDead(this);
            StartCoroutine("FadeAway");
        }

        IEnumerator FadeAway()
        {
            Renderer renderer = GetComponentInChildren<Renderer>();
            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < 10; i++)
                {
                    yield return null;
                }
                renderer.enabled = !renderer.enabled;
            }
            Destroy(gameObject);
        }

        void Healed() {
            ResultScreen.instance.NumTreatedWhales++;
            currState = BehaviourStates.Breathing;
            currDestination = (waypointParent.TransformPoint(offset));
            if (transform.parent == tyingPoint)
                UnTie();
            anim.Rebind();
            Destroy(uiHlth);
        }

        void Jump() { }


    }
}