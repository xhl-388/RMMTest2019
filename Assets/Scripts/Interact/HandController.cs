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
            if (!m_redirector.entityController.isAttacking)
                return;
            
            // 测试物体是否是可被击中的对象
            Transform targetParent = collision.transform.parent;
            if (targetParent == null || targetParent.name != "Ragdoll" || targetParent == transform.parent) return;

            // 通过HitController去处理击中逻辑
            Redirector targetRedirector = collision.transform.root.GetComponent<Redirector>();
            HitController targetHitController = targetRedirector.hitController;
            if (!targetHitController.CanHit())
                return;
            if (targetHitController.TakeHit(collision.gameObject, collision.contacts[0].point, m_rigidbody.velocity))
            {
                Debug.Log("HIT DA~ZE~ ☆");
            }
        }
    }
}
