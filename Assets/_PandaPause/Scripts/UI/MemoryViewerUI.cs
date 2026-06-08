using System.Text;
using PandaPause.Core;
using UnityEngine;
using UnityEngine.UI;

namespace PandaPause.UI
{
    public class MemoryViewerUI : MonoBehaviour
    {
        [SerializeField] private Text memoryText;

        public void ShowMemories()
        {
            JournalDatabase database = JournalSaveSystem.LoadDatabase();

            if (database.entries.Count == 0)
            {
                memoryText.text = "No memories yet.";
                return;
            }

            StringBuilder builder = new StringBuilder();

            foreach (JournalEntry entry in database.entries)
            {
                builder.AppendLine(entry.dateUtc);
                builder.AppendLine($"Mood: {entry.mood}");
                builder.AppendLine(entry.entryText);
                builder.AppendLine();
            }

            memoryText.text = builder.ToString();
        }
    }
}