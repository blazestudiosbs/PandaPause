using PandaPause.Core;
using UnityEngine;
using UnityEngine.UI;

namespace PandaPause.UI
{
    public class HomeScreenMoodUI : MonoBehaviour
    {
        [SerializeField] private Text responseText;

        private string PandaName
        {
            get
            {
                var profile = PandaAppController.Instance.CurrentProfile;
                return string.IsNullOrWhiteSpace(profile.pandaName) ? "Maple" : profile.pandaName;
            }
        }

        public void SelectGreat()
        {
            responseText.text = $"{PandaName} loves hearing that. What made today feel good?";
        }

        public void SelectOkay()
        {
            responseText.text = $"{PandaName} is here. Want to talk about your day?";
        }

        public void SelectRough()
        {
            responseText.text = $"{PandaName} is listening. What made today feel rough?";
        }

        public void SelectExhausted()
        {
            responseText.text = $"{PandaName} is right here. Let's take this one tiny pause at a time.";
        }
    }
}