using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CP.Character;
using CP.Core.Element;


namespace CP.Level.Common
{
    public class DropedElement : MonoBehaviour
    {
        public Element element;
        private int m_playerLayer;
        private SphereCollider m_sphereCollider;
        private bool m_canCollect = false;

        private void Awake()
        {
            m_playerLayer = LayerMask.GetMask("Human");
            m_sphereCollider = GetComponent<SphereCollider>();
        }

        private void Start()
        {
            StartCoroutine(DelayCollect());
        }

        private void FixedUpdate()
        {
            if (!m_canCollect)
            {
                return;
            }

            var hits = Physics.OverlapSphere(transform.position, m_sphereCollider.radius * transform.lossyScale.x, m_playerLayer);
            foreach(var hit in hits)
            {
                if (hit.CompareTag("Player"))
                {
                    var pac = hit.transform.GetComponent<PlayerElementController>();
                    if (pac.AddElement(element))
                    {
                        Debug.Log(string.Format("Player get {0} element", element.ToString()));
                        Destroy(gameObject);
                    }
                    break;
                }
            }
        }

        private IEnumerator DelayCollect()
        {
            yield return new WaitForSeconds(0.5f);
            m_canCollect = true;
        }
    }
}
