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
            Debug.Log(string.Format("Throw Element: {0} at {1}", element.ToString(), transform.position));
        }
    }
}
