using PandaPause.Core;
using UnityEngine;
using UnityEngine.UI;

namespace PandaPause.UI
{
    public class SetupScreenUI : MonoBehaviour
    {
        [SerializeField] private InputField userNameInput;
        [SerializeField] private InputField pandaNameInput;
        [SerializeField] private Button continueButton;

        [SerializeField] private GameObject setupScreen;
        [SerializeField] private GameObject homeScreen;
        [SerializeField] private HomeScreenUI homeScreenUI;

        private void Start()
        {
            continueButton.onClick.AddListener(HandleContinue);

            if (PandaAppController.Instance.CurrentProfile.hasCompletedSetup)
                ShowHome();
            else
                ShowSetup();
        }

        private void HandleContinue()
        {
            string userName = string.IsNullOrWhiteSpace(userNameInput.text) ? "Friend" : userNameInput.text.Trim();
            string pandaName = string.IsNullOrWhiteSpace(pandaNameInput.text) ? "Maple" : pandaNameInput.text.Trim();

            PandaAppController.Instance.CompleteSetup(userName, pandaName);
            ShowHome();
        }

        private void ShowSetup()
        {
            setupScreen.SetActive(true);
            homeScreen.SetActive(false);
        }

        private void ShowHome()
        {
            setupScreen.SetActive(false);
            homeScreen.SetActive(true);
            homeScreenUI.Refresh();
        }
    }
}