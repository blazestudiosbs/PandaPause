using UnityEngine;

namespace PandaPause.UI
{
    public class CloseScreenUI : MonoBehaviour
    {
        [SerializeField] private GameObject screenToClose;
        [SerializeField] private GameObject screenToShow;

        public void Close()
        {
            if (screenToClose != null)
                screenToClose.SetActive(false);

            if (screenToShow != null)
                screenToShow.SetActive(true);
        }
    }
}