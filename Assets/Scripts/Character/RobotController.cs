using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CP.Character
{
    public class RobotController : MonoBehaviour
    {
        public Vector3 lookAtPos;
        public float lookAtTimer;
        public bool lookAt;

        public GameObject[] targets;

        private Vector3 inputDirection;
        private float inputVelocity;
        private Rigidbody rb;
        private Animator animator;
        private Transform cam;

        private float findTargetTimer;
        private bool isFocused;
        private bool isRun;
        private bool isAttacking;

        // Use this for initialization
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            cam = UnityEngine.Camera.main.transform;

            targets = GameObject.FindGameObjectsWithTag("Enemy");

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }

        void FixedUpdate()
        {
        }


        // Update is called once per frame
        void Update()
        {
            inputDirection = Quaternion.Euler(0, cam.rotation.eulerAngles.y, 0) * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
            inputVelocity = Mathf.Max(Mathf.Abs(Input.GetAxis("Horizontal")), Mathf.Abs(Input.GetAxis("Vertical")));

            float angle = Vector3.SignedAngle(transform.forward, inputDirection, Vector3.up);
            animator.SetFloat("direction", angle / 180);
            animator.SetFloat("velocity", inputVelocity);

            isAttacking = animator.GetCurrentAnimatorStateInfo(0).IsTag("attack");
            float time = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Default") || (isAttacking && time > 0.5f))
            {
                if (Input.GetButtonDown("Fire1") && !isRun)
                {

                    animator.SetBool("side", !animator.GetBool("side"));
                    animator.SetInteger("number", Random.Range(0, 3));
                    animator.SetTrigger("attack");
                    // TODO: Attack physically
                }
            }

            if (Input.GetButtonDown("Fire3"))
            {
                isRun = true;
                animator.SetBool("run", true);
            }
            if (Input.GetButtonUp("Fire3"))
            {
                animator.SetBool("run", false);
                isRun = false;
            }

            Vector3 pos = transform.TransformPoint(new Vector3(0, 0.6f, 1));
            if (lookAtTimer > 0)
            {
                lookAtTimer -= Time.deltaTime;
                lookAtPos = Vector3.Lerp(lookAtPos, pos, (2 - lookAtTimer) / 2);
            }
            else
            {
                lookAtPos = pos;
            }
        }


        void OnAnimatorIK()
        {
            if (lookAt)
            {
                animator.SetLookAtWeight(1, 0.5f);
                animator.SetLookAtPosition(lookAtPos);
            }
        }
    }
}