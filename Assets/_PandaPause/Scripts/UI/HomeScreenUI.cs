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

    JournalDatabase database = JournalSaveSystem.LoadDatabase();

    if (database.entries != null && database.entries.Count > 0)
    {
        string latestMood = database.entries[database.entries.Count - 1].mood;

        switch (latestMood)
        {
            case "Great":
                pandaPromptText.text = $"{pandaName} remembers you had a good day yesterday. How are you feeling today?";
                break;

            case "Okay":
                pandaPromptText.text = $"{pandaName} remembers yesterday was okay. How are you feeling today?";
                break;

            case "Rough":
                pandaPromptText.text = $"{pandaName} remembers yesterday was rough. How are you feeling today?";
                break;

            case "Exhausted":
                pandaPromptText.text = $"{pandaName} remembers you were exhausted yesterday. How are you feeling today?";
                break;

            default:
                pandaPromptText.text = $"{pandaName} is here. How are you feeling today?";
                break;
        }
    }
    else
    {
        pandaPromptText.text = $"{pandaName} is here. How are you feeling today?";
    }
}
    }
}