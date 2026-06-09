using UnityEngine;

namespace PandaPause.UI
{
    public class ProfileButtonUI : MonoBehaviour
    {
        [SerializeField] private GameObject profileScreen;

        public void OpenProfile()
        {
            if (profileScreen != null)
                profileScreen.SetActive(true);
        }
    }
}