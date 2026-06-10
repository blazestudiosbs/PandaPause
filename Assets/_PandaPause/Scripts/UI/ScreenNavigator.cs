using UnityEngine;

namespace PandaPause.UI
{
    public class ScreenNavigator : MonoBehaviour
    {
        [SerializeField] private GameObject welcomeScreen;
        [SerializeField] private GameObject homeScreen;
        [SerializeField] private GameObject profileScreen;
        [SerializeField] private GameObject aboutScreen;
        [SerializeField] private GameObject privacyScreen;
        [SerializeField] private GameObject conservationScreen;

        public void ShowWelcome() => ShowOnly(welcomeScreen);
        public void ShowHome() => ShowOnly(homeScreen);
        public void ShowProfile() => ShowOnly(profileScreen);
        public void ShowAbout() => ShowOnly(aboutScreen);
        public void ShowPrivacy() => ShowOnly(privacyScreen);
        public void ShowConservation() => ShowOnly(conservationScreen);

        private void ShowOnly(GameObject screenToShow)
        {
            SetScreen(welcomeScreen, false);
            SetScreen(homeScreen, false);
            SetScreen(profileScreen, false);
            SetScreen(aboutScreen, false);
            SetScreen(privacyScreen, false);
            SetScreen(conservationScreen, false);

            SetScreen(screenToShow, true);
        }

        private void SetScreen(GameObject screen, bool active)
        {
            if (screen != null)
                screen.SetActive(active);
        }
    }
}