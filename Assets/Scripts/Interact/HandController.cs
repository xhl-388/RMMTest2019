using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CP.Utils;

namespace CP.Interact
{
    public class HandController : MonoBehaviour
    {
        private HitController m_hitController;
        private Redirector m_redirector;
        private Rigidbody m_rigidbody;

        private void Awake()
        {
            m_redirector = transform.root.GetComponent<Redirector>();
            m_hitController = m_redirector.hitController;
            m_rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            //if (col.impulse == Vector3.zero) return;
            Transform targetParent = collision.transform.parent;
            if (targetParent == null || targetParent.name != "Ragdoll" || targetParent == transform.parent) return;
            Redirector targetRedirector = collision.transform.root.GetComponent<Redirector>();
            HitController targetHitController = targetRedirector.hitController;
            if (targetHitController.TakeHit(collision.gameObject, collision.contacts[0].point, m_rigidbody.velocity))
            {
                Debug.Log("HIT DA~ZE~ ☆");
            }
        }
    }
}
