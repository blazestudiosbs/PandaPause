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
            if (greetingText == null || pandaPromptText == null)
            {
                Debug.LogError("HomeScreenUI is missing text references.");
                return;
            }

            PandaProfile savedProfile = PandaSaveSystem.LoadProfile();

if (PandaAppController.Instance == null || PandaAppController.Instance.CurrentProfile == null)
{
    string savedUserName = string.IsNullOrWhiteSpace(savedProfile.userName) ? "Friend" : savedProfile.userName;
    string savedPandaName = string.IsNullOrWhiteSpace(savedProfile.pandaName) ? "Your panda" : savedProfile.pandaName;

    greetingText.text = $"Hi, {savedUserName}.";
    pandaPromptText.text = $"{savedPandaName} is here. How are you feeling today?";
    return;
}

            var profile = PandaAppController.Instance.CurrentProfile;

            string userName = string.IsNullOrWhiteSpace(profile.userName)
                ? "Friend"
                : profile.userName;

            string pandaName = string.IsNullOrWhiteSpace(profile.pandaName)
                ? "Your panda"
                : profile.pandaName;

            greetingText.text = $"Hi, {userName}.";

            JournalDatabase database = JournalSaveSystem.LoadDatabase();

            if (database.entries == null || database.entries.Count == 0)
            {
                
                pandaPromptText.text = $"{pandaName} is here. How are you feeling today?";
                return;
            }

            JournalEntry latest = database.entries[database.entries.Count - 1];
            Debug.Log($"Prompt generated: '{pandaPromptText.text}'");

            pandaPromptText.text = PandaFollowUpGenerator.GetFollowUp(
                pandaName,
                latest.mood,
                latest.entryText
            );
        }
    }
}