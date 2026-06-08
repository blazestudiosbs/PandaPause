using UnityEngine;
using UnityEngine.UI;

namespace PandaPause.UI
{
    public class HomeScreenMoodUI : MonoBehaviour
    {
        [SerializeField] private Text responseText;

        public void SelectGreat()
        {
            responseText.text = "I love hearing that. What made today feel good?";
        }

        public void SelectOkay()
        {
            responseText.text = "Okay is still a place to begin. Want to talk about your day?";
        }

        public void SelectRough()
        {
            responseText.text = "I'm here with you. What made today feel rough?";
        }

        public void SelectExhausted()
        {
            responseText.text = "That sounds heavy. Let's take this one tiny pause at a time.";
        }
    }
}