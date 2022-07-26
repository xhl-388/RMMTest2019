using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CP.Core.Element
{
    public class ElementController : MonoBehaviour
    {
        public static ElementController Instance { get; private set; }

        private ElementModel m_elementModel;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            m_elementModel = GetComponent<ElementModel>();
        }

        public void ThrowElement(Element element, Transform transform)
        {
            var prefab = Resources.Load<GameObject>(string.Format("Prefabs/ElementDrops/{0}_DropedItem", element.ToString()));
            var go = GameObject.Instantiate(prefab, transform.position, Quaternion.identity);
            var rig = go.GetComponent<Rigidbody>();
            var dir = Vector3.up * 3 + new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f)).normalized;
            rig.AddForce(dir * 100.0f);
            Debug.Log(string.Format("Throw Element: {0} at {1}", element.ToString(), transform.position));
        }
    }
}
