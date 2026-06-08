using PandaPause.Core;
using UnityEngine;
using UnityEngine.UI;

namespace PandaPause.UI
{
    public class HomeScreenUI : MonoBehaviour
    {
        [SerializeField] private Text greetingText;
        [SerializeField] private Text pandaPromptText;

        private void OnEnable()
        {
            Refresh();
        }

        public void Refresh()
        {
            if (PandaAppController.Instance == null) return;

            var profile = PandaAppController.Instance.CurrentProfile;

            string userName = string.IsNullOrWhiteSpace(profile.userName) ? "Friend" : profile.userName;
            string pandaName = string.IsNullOrWhiteSpace(profile.pandaName) ? "Maple" : profile.pandaName;

            greetingText.text = $"Hi, {userName}.";
            pandaPromptText.text = $"{pandaName} is here. How are you feeling today?";
        }
    }
}