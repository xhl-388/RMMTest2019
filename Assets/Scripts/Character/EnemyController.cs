using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CP.Character
{
    public class EnemyController : EntityController
    {
        public UnityEvent OnEnemyDieEvent;

        public override void Die()
        {
            OnEnemyDieEvent?.Invoke();

            Debug.Log(string.Format("Enemy {0} died", name));
        }
    }

}