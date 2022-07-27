using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CP.Interact;
using Assets.ProgressBars.Scripts;
using UnityEngine.Events;

namespace CP.UI
{
    public class HealthUIUpdater : MonoBehaviour
    {
        private HitController m_playerHitController;
        private GuiProgressBarUI m_progressBarUI;

        private void Awake()
        {
            m_playerHitController = GameObject.FindGameObjectWithTag("Player").GetComponent<HitController>();
            Debug.Assert(m_playerHitController != null);

            m_progressBarUI = GameObject.FindGameObjectWithTag("MainCanvas").GetComponentInChildren<GuiProgressBarUI>();
            Debug.Assert(m_progressBarUI != null);

            m_progressBarUI.Value = 1;
        }

        private void Start()
        {
            m_playerHitController.OnHitEvent.AddListener((damage, cur, max) =>
            {
                m_progressBarUI.Value = (float)cur / max;
                Debug.Log(string.Format("ProgressBar UI updated with new percent {0}", (float)cur / max));
            });
        }


    }
}
