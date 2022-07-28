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
        private Text[] m_elementTexts = new Text[3];

        private void Awake()
        {
            m_playerElementController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerElementController>();
            m_elementUI = GameObject.FindGameObjectWithTag("MainCanvas").transform.Find("ElementContainer");
            for(int i = 0; i < 3; i++)
            {
                Debug.Log(string.Format("Text{0}", i));
                m_elementTexts[i] = m_elementUI.Find(string.Format("Text{0}", i)).GetComponent<Text>();
            }
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
                    m_elementTexts[i].text = "NAE";
                }
            });
        }
    }
}