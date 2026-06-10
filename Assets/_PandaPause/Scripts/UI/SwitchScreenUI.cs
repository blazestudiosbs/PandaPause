using UnityEngine;

namespace PandaPause.UI
{
    public class SwitchScreenUI : MonoBehaviour
    {
        [SerializeField] private GameObject[] screensToHide;
        [SerializeField] private GameObject screenToShow;

        public void Switch()
        {
            foreach (GameObject screen in screensToHide)
            {
                if (screen != null)
                    screen.SetActive(false);
            }

            if (screenToShow != null)
                screenToShow.SetActive(true);
        }
    }
}