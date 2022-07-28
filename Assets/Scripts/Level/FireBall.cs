using System.Collections;
using UnityEngine;

namespace CP.Level
{
    public class FireBall : MonoBehaviour
    {
        public Vector3 velocity;

        private void Start()
        {
            StartCoroutine(DelayDestroySelf());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            {
                var hitController = other.GetComponent<Interact.HitController>();
                hitController.TakeHit(hitController.hitBodyParts[0], hitController.hitBodyParts[0].transform.position, velocity, true);
            }
        }

        private void Update()
        {
            transform.Translate(velocity * Time.deltaTime);
        }

        private IEnumerator DelayDestroySelf()
        {
            yield return new WaitForSeconds(1.0f);
            Destroy(gameObject);
        }
    }
}