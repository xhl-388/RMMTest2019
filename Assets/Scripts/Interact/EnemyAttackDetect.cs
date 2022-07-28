using System.Collections;
using UnityEngine;
using CP.Character;

namespace CP.Interact
{
    public class EnemyAttackDetect : MonoBehaviour
    {
        private EnemyController m_enemyController;

        private void Awake()
        {
            m_enemyController = GetComponentInParent<EnemyController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                m_enemyController.SetTarget(other.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                m_enemyController.ClearTarget();
            }
        }
    }
}