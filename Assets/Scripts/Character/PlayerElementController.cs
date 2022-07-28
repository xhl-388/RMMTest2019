using System.Collections.Generic;
using UnityEngine;
using CP.Core.Element;
using UnityEngine.Events;
using CP.Utils;
using System;

namespace CP.Character
{
    public class PlayerElementController : MonoBehaviour
    {
        public const int MaxElementSize = 3;
        public ElementChangeEvent OnElementChangeEvent = new ElementChangeEvent();

        private Element[] m_elements = new Element[3];
        public int currentSize = 0;

        private bool IsFull()
        {
            return currentSize == MaxElementSize;
        }

        public bool AddElement(Element element)
        {
            if (IsFull())
                return false;
            m_elements[currentSize++] = element;
            OnElementChangeEvent.Invoke(this);
            return true;
        }

        public Element GetElementByIndex(int idx)
        {
            if (idx < 0 || idx >= currentSize)
            {
                throw new Exception("Invalid index!");
            }
            return m_elements[idx];
        }

    }
}