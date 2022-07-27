using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CP.Interact;

namespace CP.Character
{
    public class EnemyController : EntityController
    {
        public UnityEvent OnEnemyDieEvent;

        public bool lookAt;

        private Transform m_target;

        private Vector3 m_moveTargetPosition;
        private Vector3 m_inputDirection;
        private float m_inputVelocity;
        private Rigidbody m_rigidbody;
        private Animator m_animator;
        private HitController m_hitController;
        
        private bool m_isRun;

        public bool IsDead
        {
            get
            {
                if (m_hitController == null)
                    m_hitController = GetComponent<HitController>();
                return m_hitController.IsDead;
            }
        }

        public override void Die()
        {
            isAttacking = false;

            OnEnemyDieEvent?.Invoke();

            Debug.Log(string.Format("Enemy {0} died", name));
        }

        private void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            m_animator = GetComponent<Animator>();
            m_hitController = GetComponent<HitController>();

            m_target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            if (m_hitController.IsDead) return;

            if (m_target != null)
            {
                m_inputDirection = m_moveTargetPosition - m_rigidbody.position;
            }
            else
            {
                m_inputDirection = Vector3.zero;
            }

            float vel = Mathf.Clamp(m_inputDirection.magnitude * 2, -1, 1);
            m_inputVelocity = Mathf.Abs(vel);

            float angle = Vector3.SignedAngle(transform.forward, (m_inputDirection * vel).normalized, Vector3.up);
            m_animator.SetFloat("direction", angle / 180);
            m_animator.SetFloat("velocity", m_inputVelocity);

            isAttacking = m_animator.GetCurrentAnimatorStateInfo(0).IsTag("attack");
            float time = m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            float dist = Vector3.Distance(m_target.position, m_rigidbody.position);
            if (dist <= 0.6f && !m_isRun)
            {
                if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Default"))
                {
                    m_animator.SetBool("side", !m_animator.GetBool("side"));
                    m_animator.SetInteger("number", Random.Range(0, 3));
                    m_animator.SetTrigger("attack");
                }
            }

            if (dist > 6 && !m_isRun)
            {
                m_isRun = true;
                m_animator.SetBool("run", true);
            }
            if (dist <= 6 && m_isRun)
            {
                m_animator.SetBool("run", false);
                m_isRun = false;
            }

        }

        private void LateUpdate()
        {
            if (m_target == null && isAttacking) return;

            if (m_target != null)
            {
                m_moveTargetPosition = m_target.position;
            }

            Vector3 directionToTarget = m_target != null ? m_target.position - m_rigidbody.position : transform.forward;
            Quaternion rotation = Quaternion.LookRotation(directionToTarget.normalized, Vector3.up);
            m_rigidbody.rotation = Quaternion.Slerp(m_rigidbody.rotation, Quaternion.Euler(0, rotation.eulerAngles.y, 0), Time.deltaTime * 10);
        }

        private void OnAnimatorIK()
        {
            if (lookAt)
            {
                m_animator.SetLookAtWeight(1, 0.5f);
                m_animator.SetLookAtPosition(m_target.position);
            }
        }
    }

}