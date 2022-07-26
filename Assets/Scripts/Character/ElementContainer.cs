using System.Collections;
using UnityEngine;
using CP.Core.Element;

namespace CP.Character
{
    public class ElementContainer : MonoBehaviour
    {
        public Element element;
        private EnemyController m_enemyController;

        private void Awake()
        {
            m_enemyController = GetComponent<EnemyController>();
        }

        private void Start()
        {
            m_enemyController.OnEnemyDieEvent.AddListener(() => ElementController.Instance.ThrowElement(element, transform));
        }
    }
}