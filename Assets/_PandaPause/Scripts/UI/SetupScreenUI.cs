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

        private void Awake()
        {
            AutoWireIfMissing();
        }

        private void Start()
        {
            if (continueButton == null)
            {
                Debug.LogError("SetupScreenUI: ContinueButton is missing.");
                return;
            }

            if (PandaAppController.Instance == null)
            {
                Debug.LogError("SetupScreenUI: PandaAppController is missing.");
                return;
            }

            continueButton.onClick.RemoveListener(HandleContinue);
            continueButton.onClick.AddListener(HandleContinue);

            if (PandaAppController.Instance.CurrentProfile.hasCompletedSetup)
                ShowHome();
            else
                ShowSetup();
        }

        private void AutoWireIfMissing()
        {
            if (setupScreen == null)
                setupScreen = gameObject;

            if (homeScreen == null)
            {
                Transform found = transform.parent != null
                    ? transform.parent.Find("HomeScreen")
                    : null;

                if (found != null)
                    homeScreen = found.gameObject;
            }

            if (userNameInput == null)
            {
                Transform found = transform.Find("UserNameInput");
                if (found != null)
                    userNameInput = found.GetComponent<InputField>();
            }

            if (pandaNameInput == null)
            {
                Transform found = transform.Find("PandaNameInput");
                if (found != null)
                    pandaNameInput = found.GetComponent<InputField>();
            }

            if (continueButton == null)
            {
                Transform found = transform.Find("ContinueButton");
                if (found != null)
                    continueButton = found.GetComponent<Button>();
            }

            if (homeScreenUI == null && homeScreen != null)
                homeScreenUI = homeScreen.GetComponent<HomeScreenUI>();
        }

        private void HandleContinue()
        {
            string userName = userNameInput != null ? userNameInput.text.Trim() : "";
            string pandaName = pandaNameInput != null ? pandaNameInput.text.Trim() : "";

            if (string.IsNullOrWhiteSpace(userName))
                userName = "Friend";

            if (string.IsNullOrWhiteSpace(pandaName))
                pandaName = "Your panda";

            PandaAppController.Instance.CompleteSetup(userName, pandaName);
            ShowHome();
        }

        private void ShowSetup()
        {
            if (setupScreen != null)
                setupScreen.SetActive(true);

            if (homeScreen != null)
                homeScreen.SetActive(false);
        }

        private void ShowHome()
        {
            if (setupScreen != null)
                setupScreen.SetActive(false);

            if (homeScreen != null)
                homeScreen.SetActive(true);

            if (homeScreenUI != null)
                homeScreenUI.Refresh();
        }
    }
}