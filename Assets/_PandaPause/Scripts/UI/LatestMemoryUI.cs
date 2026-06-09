using PandaPause.Core;
using UnityEngine;
using UnityEngine.UI;

namespace PandaPause.UI
{
    public class LatestMemoryUI : MonoBehaviour
    {
        [SerializeField] private Text latestMemoryText;

        private void OnEnable()
        {
            Refresh();
        }

        public void Refresh()

        
        {
            Debug.Log("LatestMemoryUI Refresh called.");
string pandaName = "Your panda";

var profile = PandaSaveSystem.LoadProfile();

if (profile != null &&
    !string.IsNullOrWhiteSpace(profile.pandaName))
{
    pandaName = profile.pandaName;
}

            if (latestMemoryText == null)
            {
                Debug.LogError("LatestMemoryUI is missing LatestMemoryText reference.");
                return;
            }

            JournalDatabase database = JournalSaveSystem.LoadDatabase();
            Debug.Log($"LatestMemoryUI entries count: {(database.entries == null ? -1 : database.entries.Count)}");

            if (database.entries == null || database.entries.Count == 0)
            {
latestMemoryText.text = $"{pandaName} is ready to remember.";                return;
            }

            JournalEntry latest = database.entries[database.entries.Count - 1];
            Debug.Log($"LatestMemoryUI latest entryText: '{latest.entryText}'");

            string memory = string.IsNullOrWhiteSpace(latest.entryText)
                ? "something important"
                : latest.entryText;

latestMemoryText.text =
    $"{pandaName} remembers {GetMemoryAge(latest.dateUtc)}:\n\"{memory}\"";
        }

        private string GetMemoryAge(string dateUtc)
        {
            if (!System.DateTime.TryParse(dateUtc, out System.DateTime parsedDate))
                return "";

            System.DateTime memoryDate = parsedDate.ToLocalTime().Date;
            System.DateTime today = System.DateTime.Now.Date;

            int daysAgo = (today - memoryDate).Days;

            if (daysAgo <= 0)
                return "from today";

            if (daysAgo == 1)
                return "from yesterday";

            return $"from {daysAgo} days ago";
        }
    }
}