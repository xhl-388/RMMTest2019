using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

namespace CP.Core.Element
{
    public enum Element
    {
        H = 1,
        O = 16,
        S = 32
    }

    public class Matter
    {
        public List<Element> Elements;
        public UnityAction CallBack;

        public Matter(List<Element> elements, UnityAction action)
        {
            Elements = elements;
            CallBack = action;
        }
    }
}