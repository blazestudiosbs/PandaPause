using UnityEngine;

namespace PandaPause.UI
{
    public class DeveloperPanelUnlocker : MonoBehaviour
    {
        [SerializeField] private GameObject developerPanel;
        [SerializeField] private int tapsRequired = 5;

        private int tapCount;

        public void RegisterTap()
        {
            tapCount++;

            if (tapCount >= tapsRequired)
            {
                developerPanel.SetActive(!developerPanel.activeSelf);
                tapCount = 0;
            }
        }
    }
}