using UnityEngine;

namespace PandaPause.UI
{
    public class OpenScreenUI : MonoBehaviour
    {
        [SerializeField] private GameObject targetScreen;
        [SerializeField] private GameObject screenToHide;

        public void Open()
        {
            if (screenToHide != null)
                screenToHide.SetActive(false);

            if (targetScreen != null)
                targetScreen.SetActive(true);
        }
    }
}