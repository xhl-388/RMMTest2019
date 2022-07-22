using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CP.Character
{
    public class RobotController : EntityController
    {
        // TODO:look at enemy
        public Vector3 lookAtPos;
        public float lookAtTimer;

        private bool p_lookAt;
        public bool lookAt { get => p_lookAt;
            set
            {
                p_lookAt = value;
                m_cameraController.SetLookAt(value);
            } }

        public GameObject[] targets;

        private Vector3 m_inputDirection;
        private float m_inputVelocity;
        private Rigidbody m_rigidbody;
        private Animator m_animator;
        private Transform m_camera;
        private Camera.CameraController m_cameraController;

        private bool m_isRunning;

        // Use this for initialization
        void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            m_animator = GetComponent<Animator>();
            m_camera = UnityEngine.Camera.main.transform;
            m_cameraController = m_camera.GetComponent<Camera.CameraController>();

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
            m_inputDirection = Quaternion.Euler(0, m_camera.rotation.eulerAngles.y, 0) * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
            m_inputVelocity = Mathf.Max(Mathf.Abs(Input.GetAxis("Horizontal")), Mathf.Abs(Input.GetAxis("Vertical")));

            if (lookAt)
            {
                float angle = Vector3.SignedAngle(transform.forward, m_inputDirection, Vector3.up);
                m_animator.SetFloat("direction", angle / 180);
                m_animator.SetFloat("velocity", m_inputVelocity);
            }
            else
            {
                if (m_inputDirection != Vector3.zero)
                {
                    transform.rotation = Quaternion.FromToRotation(Vector3.forward, m_inputDirection.normalized);
                }
                m_animator.SetFloat("direction", 0) ;
                m_animator.SetFloat("velocity", m_inputVelocity);
            }

            isAttacking = m_animator.GetCurrentAnimatorStateInfo(0).IsTag("attack");
            float time = m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Default") || (isAttacking && time > 0.5f))
            {
                if (Input.GetButtonDown("Fire1") && !m_isRunning)
                {

                    m_animator.SetBool("side", !m_animator.GetBool("side"));
                    m_animator.SetInteger("number", Random.Range(0, 3));
                    m_animator.SetTrigger("attack");
                    // TODO: Attack physically
                }
            }

            if (Input.GetButtonDown("Fire3"))
            {
                m_isRunning = true;
                m_animator.SetBool("run", true);
            }
            if (Input.GetButtonUp("Fire3"))
            {
                m_animator.SetBool("run", false);
                m_isRunning = false;
            }

            //Vector3 pos = transform.TransformPoint(new Vector3(0, 0.6f, 1));
            //if (lookAtTimer > 0)
            //{
            //    lookAtTimer -= Time.deltaTime;
            //    lookAtPos = Vector3.Lerp(lookAtPos, pos, (2 - lookAtTimer) / 2);
            //}
            //else
            //{
            //    lookAtPos = pos;
            //}

            // 按Q锁定敌人
            if (Input.GetKeyDown(KeyCode.Q))
            {
                lookAt = !lookAt;
                if (lookAt)
                {
                    lookAtPos = transform.TransformPoint(targets[0].transform.position);
                }
            }

            if (lookAt)
            {
                lookAtPos = transform.TransformPoint(targets[0].transform.position);
            }
        }


        void OnAnimatorIK()
        {
            if (lookAt)
            {
                m_animator.SetLookAtWeight(1, 0.5f);
                m_animator.SetLookAtPosition(lookAtPos);
            }
        }
    }
}