using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CP.Interact
{
    public class HitController : MonoBehaviour
    {
        public GameObject[] hitBodyParts;
        private Animator animator;
        private Rigidbody rb;
        private Collider col;
        private RagdollMecanimMixer.RamecanMixer ramecanMixer;
        private bool dead;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();
            ramecanMixer = GetComponent<RagdollMecanimMixer.RamecanMixer>();
        }

        public void Die()
        {
            ramecanMixer.BeginStateTransition("dead");
            animator.SetBool("dead", true);
            rb.isKinematic = true;
            col.enabled = false;
            dead = true;
        }

        public bool TakeHit(GameObject go, Vector3 point, Vector3 impulse)
        {
            bool hit = false;
            foreach (GameObject bone in hitBodyParts)
            {
                if (go == bone)
                {
                    hit = true;
                    break;
                }
            }
            if (!hit) return false;

            // TODO: 条件待明确
            if (true)
                Die();

            // 物理作用
            Rigidbody boneRb = go.GetComponent<Rigidbody>();
            boneRb.AddForceAtPosition(impulse.normalized * 400, point, ForceMode.Impulse);
            Vector3 dir = new Vector3(impulse.x, 0, impulse.z);
            rb.AddForce(dir.normalized * 400, ForceMode.Impulse);

            return true;
        }

        public bool CanHit()
        {
            return !dead;
        }
    }
}
