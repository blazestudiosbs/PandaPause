using UnityEngine;

namespace PandaPause.UI
{
    public class OpenScreenUI : MonoBehaviour
    {
        [SerializeField] private GameObject targetScreen;

        public void Open()
        {
            if (targetScreen != null)
                targetScreen.SetActive(true);
        }
    }
}