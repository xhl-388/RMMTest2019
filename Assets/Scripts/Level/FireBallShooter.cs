using System.Collections;
using UnityEngine;

namespace CP.Level
{
    public class FireBallShooter : MonoBehaviour
    {
        private bool m_characterInControl = false;
        private bool m_canShoot = true;
        private Character.BackpackManager m_backpackManager;
        private GameObject m_fireBallPrefab;

        private void Awake()
        {
            m_backpackManager = GameObject.FindGameObjectWithTag("Player").GetComponent<Character.BackpackManager>();
        }

        private void Start()
        {
            m_fireBallPrefab = Resources.Load<GameObject>("Prefabs/Level/FireBall");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                m_characterInControl = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                m_characterInControl = false;
            }
        }

        private void Update()
        {
            if (!m_characterInControl || !m_canShoot)
                return;

            if(Input.GetKeyDown(KeyCode.E))
            {
                if (m_backpackManager.HasMatter("O2"))
                    ShootMultiFire();
                else
                    ShootCommonFire();
            }
        }

        private void ShootCommonFire()
        {
            GameObject.Instantiate(m_fireBallPrefab, transform.position + Vector3.forward, transform.rotation);
            PostShoot();
        }

        private void ShootMultiFire()
        {
            for (int i = -2; i <= 2; i++)
            {
                GameObject.Instantiate(m_fireBallPrefab, transform.position + Vector3.forward, 
                    Quaternion.Euler(0, i * 30, 0) * transform.rotation);
            }
            PostShoot();
        }

        private void PostShoot()
        {
            m_canShoot = false;
            StartCoroutine(DelayRecoverShoot());
        }

        private IEnumerator DelayRecoverShoot()
        {
            yield return new WaitForSeconds(1.5f);
            m_canShoot = true;
        }
    }
}