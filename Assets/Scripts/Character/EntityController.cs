using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CP.Character
{
    public abstract class EntityController : MonoBehaviour
    {
        [HideInInspector]
        public bool isAttacking;
    }
}
