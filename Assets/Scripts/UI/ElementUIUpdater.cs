using System.Collections;
using UnityEngine;
using CP.Character;
using UnityEngine.UI;

namespace CP.UI
{
    public class ElementUIUpdater : MonoBehaviour
    {
        private PlayerElementController m_playerElementController;
        private Transform m_elementUI;
        private Text[] m_elementTexts;

        private void Awake()
        {
            m_playerElementController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerElementController>();
            m_elementUI = GameObject.FindGameObjectWithTag("MainCanvas").transform.Find("ElementContainer");
            m_elementTexts = m_elementUI.GetComponentsInChildren<Text>();
        }

        private void Start()
        {
            m_playerElementController.OnElementChangeEvent.AddListener((pec) =>
            {
                for(int i = 0; i < pec.currentSize; i++)
                {
                    m_elementTexts[i].text = pec.GetElementByIndex(i).ToString();
                }
                for(int i = pec.currentSize; i < PlayerElementController.MaxElementSize; i++)
                {
                    m_elementTexts[i].text = "N";
                }
            });
        }
    }
}