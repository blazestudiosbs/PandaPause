using PandaPause.Core;
using UnityEngine;
using UnityEngine.UI;

namespace PandaPause.UI
{
    public class WelcomeScreenUI : MonoBehaviour
    {
        [SerializeField] private Text welcomeTitleText;
        [SerializeField] private LatestMemoryUI latestMemoryUI;

        [SerializeField] private GameObject welcomeScreen;
        [SerializeField] private GameObject homeScreen;

        private void OnEnable()
        {
            Refresh();
        }


        public void Refresh()
        {
            if (welcomeTitleText != null)
                welcomeTitleText.text = $"Welcome back, {GetUserName()}.";

            if (latestMemoryUI != null)
                latestMemoryUI.Refresh();
        }

        public void ReturnToWelcome()
{
    if (homeScreen != null)
        homeScreen.SetActive(false);

    if (welcomeScreen != null)
        welcomeScreen.SetActive(true);

    Refresh();
}

        public void ContinueToHome()
        {
            if (welcomeScreen != null)
                welcomeScreen.SetActive(false);

            if (homeScreen != null)
                homeScreen.SetActive(true);
        }

        private string GetUserName()
{
    PandaProfile profile = PandaSaveSystem.LoadProfile();

    if (profile != null && !string.IsNullOrWhiteSpace(profile.userName))
        return profile.userName;

    return "Friend";
}
    }
}