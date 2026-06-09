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
    string pandaName = GetPandaName();

    if (string.IsNullOrWhiteSpace(entryText))
    {
        responseText.text = $"{pandaName} is listening. Write a little something first.";
        return;
    }

    JournalDatabase database = JournalSaveSystem.LoadDatabase();

    string pandaResponse = PandaResponseGenerator.GetJournalResponse(
        pandaName,
        currentMood,
        entryText
    );

    JournalEntry entry = new JournalEntry
    {
        dateUtc = DateTime.UtcNow.ToString("o"),
        mood = currentMood,
        entryText = entryText,
        pandaResponse = pandaResponse
    };

    database.entries.Add(entry);
    JournalSaveSystem.SaveDatabase(database);

    journalInput.text = "";
    responseText.text = pandaResponse;

    if (latestMemoryUI != null)
        latestMemoryUI.Refresh();

    if (homeScreenUI != null)
        homeScreenUI.Refresh();
}

private string GetPandaName()
{
    if (PandaAppController.Instance != null &&
        PandaAppController.Instance.CurrentProfile != null &&
        !string.IsNullOrWhiteSpace(PandaAppController.Instance.CurrentProfile.pandaName))
    {
        return PandaAppController.Instance.CurrentProfile.pandaName;
    }

    return "Your panda";
}
    }
}