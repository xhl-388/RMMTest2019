using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace CP.Utils
{
    [SerializeField]
    public class BodyHitEvent: UnityEvent<int,int,int>
    {

    }

    [SerializeField]
    public class ElementChangeEvent:UnityEvent<Character.PlayerElementController>
    {

    }
}