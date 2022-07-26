using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace CP.Core.Element
{

    public class ElementModel : MonoBehaviour
    {
        public Dictionary<string, Matter> matters = new Dictionary<string, Matter>();
        private void Start()
        {
            LoadData();
        }

        private void LoadData()
        {
            matters.Add("H2O",
                new Matter(new List<Element>() { Element.H, Element.H, Element.O },
                new UnityAction(() => Debug.Log("H2O take in"))));
        }
    }

}