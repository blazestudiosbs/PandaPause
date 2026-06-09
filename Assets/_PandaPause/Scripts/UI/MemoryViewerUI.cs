using System;
using System.Text;
using PandaPause.Core;
using UnityEngine;
using UnityEngine.UI;

namespace PandaPause.UI
{
    public class MemoryViewerUI : MonoBehaviour
    {
        [SerializeField] private Text memoryText;
        [SerializeField] private int maxEntriesToShow = 5;

        public void ShowMemories()
        {
            if (memoryText == null)
            {
                Debug.LogError("MemoryViewerUI is missing MemoryText reference.");
                return;
            }

            JournalDatabase database = JournalSaveSystem.LoadDatabase();

            if (database.entries == null || database.entries.Count == 0)
            {
                memoryText.text ="Your panda has no memories yet.";
                return;
            }

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Memory Journal");
            builder.AppendLine();

            int shown = 0;

            for (int i = database.entries.Count - 1; i >= 0 && shown < maxEntriesToShow; i--)
            {
                JournalEntry entry = database.entries[i];

                string dateLabel = FormatDate(entry.dateUtc);
                string mood = string.IsNullOrWhiteSpace(entry.mood) ? "Unselected" : entry.mood;
                string entryText = string.IsNullOrWhiteSpace(entry.entryText) ? "No written memory." : entry.entryText;

                builder.AppendLine(dateLabel);
                builder.AppendLine($"Mood: {mood}");
                builder.AppendLine($"\"{entryText}\"");
                builder.AppendLine();

                shown++;
            }

            memoryText.text = builder.ToString();
        }

        private string FormatDate(string dateUtc)
        {
            if (!DateTime.TryParse(dateUtc, out DateTime parsedDate))
                return "Unknown date";

            DateTime localDate = parsedDate.ToLocalTime();
            DateTime today = DateTime.Now.Date;
            DateTime memoryDate = localDate.Date;

            int daysAgo = (today - memoryDate).Days;

            if (daysAgo == 0)
                return "Today";

            if (daysAgo == 1)
                return "Yesterday";

            return $"{daysAgo} days ago";
        }
    }
}