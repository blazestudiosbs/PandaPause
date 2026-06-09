using PandaPause.Core;
using UnityEngine;
using UnityEngine.UI;

namespace PandaPause.UI
{
    public class ProfileUI : MonoBehaviour
    {
        [SerializeField] private InputField userNameInput;
        [SerializeField] private InputField pandaNameInput;

        private void OnEnable()
        {
            LoadProfile();
        }

        public void LoadProfile()
        {
            if (PandaAppController.Instance == null)
                return;

            PandaProfile profile = PandaAppController.Instance.CurrentProfile;

            if (profile == null)
                return;

            userNameInput.text = profile.userName;
            pandaNameInput.text = profile.pandaName;
        }

        public void SaveProfile()
        {
            if (PandaAppController.Instance == null)
                return;

            PandaProfile profile = PandaAppController.Instance.CurrentProfile;

            if (profile == null)
                return;

            profile.userName = userNameInput.text.Trim();
            profile.pandaName = pandaNameInput.text.Trim();

            PandaSaveSystem.SaveProfile(profile);

            Debug.Log("Profile saved.");
        }

        public void CloseProfile()
        {
            gameObject.SetActive(false);
        }
    }
}