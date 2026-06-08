using System;
using PandaPause.Core;
using UnityEngine;
using UnityEngine.UI;

namespace PandaPause.UI
{
    public class JournalUI : MonoBehaviour
    {
        [SerializeField] private InputField journalInput;
        [SerializeField] private Text responseText;
        [SerializeField] private LatestMemoryUI latestMemoryUI;
        [SerializeField] private HomeScreenUI homeScreenUI;

        private string currentMood = "Unselected";

        public void SetMood(string mood)
        {
            currentMood = mood;
        }

        public void SaveEntry()
        {
            if (journalInput == null || responseText == null)
            {
                Debug.LogError("JournalUI is missing references.");
                return;
            }

            string entryText = journalInput.text.Trim();
            Debug.Log($"JournalUI saving entryText: '{entryText}'");

            if (string.IsNullOrWhiteSpace(entryText))
            {
                responseText.text = "Write a little something first. Your panda is listening.";
                return;
            }

            JournalDatabase database = JournalSaveSystem.LoadDatabase();

            JournalEntry entry = new JournalEntry
            {
                dateUtc = DateTime.UtcNow.ToString("o"),
                mood = currentMood,
                entryText = entryText,
                pandaResponse = responseText.text
            };

            database.entries.Add(entry);
            JournalSaveSystem.SaveDatabase(database);
            if (latestMemoryUI != null)
{
    latestMemoryUI.Refresh();
}
if (homeScreenUI != null)
{
    homeScreenUI.Refresh();
}
            Debug.Log($"JournalUI saved entries count: {database.entries.Count}");

            journalInput.text = "";
            responseText.text = "Thank you for sharing that. Your panda tucked it safely away.";
        }
    }
}