using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CP.Interact
{
    public class HitController : MonoBehaviour
    {
        public GameObject[] hitBodyParts;
        public int[] hitDamage;

        private Animator m_animator;
        private Rigidbody m_rigidbody;
        private Collider m_collider;
        private RagdollMecanimMixer.RamecanMixer m_ramecanMixer;
        private Utils.Redirector m_redirector;

        [SerializeField]
        private int maxHealth = 100;
        private int m_currentHealth;
        private bool m_isDead;

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_rigidbody = GetComponent<Rigidbody>();
            m_collider = GetComponent<Collider>();
            m_ramecanMixer = GetComponent<RagdollMecanimMixer.RamecanMixer>();
            m_redirector = transform.parent.GetComponent<Utils.Redirector>();
            m_currentHealth = maxHealth;

            Debug.Assert(hitBodyParts.Length == hitDamage.Length);
        }

        public void Die()
        {
            m_ramecanMixer.BeginStateTransition("dead");
            m_animator.SetBool("dead", true);
            m_rigidbody.isKinematic = true;
            m_collider.enabled = false;
            m_isDead = true;

            m_redirector.entityController.Die();
        }

        public bool TakeHit(GameObject go, Vector3 point, Vector3 impulse)
        {
            int hitBoneIdx = -1;
            for(int i  = 0; i < hitBodyParts.Length; i++)
            {
                if(go == hitBodyParts[i])
                {
                    hitBoneIdx = i;
                    break;
                }
            }

            if (hitBoneIdx < 0) return false;

            m_currentHealth = Mathf.Max(m_currentHealth - hitDamage[hitBoneIdx], 0);
            if (m_currentHealth == 0)
                Die();

            Debug.Log(string.Format("{0} take {1} damage", name, hitDamage[hitBoneIdx]));

            // 物理作用
            Rigidbody boneRb = go.GetComponent<Rigidbody>();
            float force = 400 * (m_isDead ? 1 : 0.8f);
            boneRb.AddForceAtPosition(impulse.normalized * force, point, ForceMode.Impulse);
            Vector3 dir = new Vector3(impulse.x, 0, impulse.z);
            m_rigidbody.AddForce(dir.normalized * force, ForceMode.Impulse);

            return true;
        }

        public bool CanHit()
        {
            return !m_isDead;
        }
    }
}
