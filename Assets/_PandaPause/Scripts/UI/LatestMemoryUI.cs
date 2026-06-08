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

if (PandaAppController.Instance != null &&
    PandaAppController.Instance.CurrentProfile != null &&
    !string.IsNullOrWhiteSpace(PandaAppController.Instance.CurrentProfile.pandaName))
{
    pandaName = PandaAppController.Instance.CurrentProfile.pandaName;
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
                latestMemoryText.text = "Your panda is ready to remember.";
                return;
            }

            JournalEntry latest = database.entries[database.entries.Count - 1];
            Debug.Log($"LatestMemoryUI latest entryText: '{latest.entryText}'");

            string memory = string.IsNullOrWhiteSpace(latest.entryText)
                ? "something important"
                : latest.entryText;

            latestMemoryText.text = $"{pandaName} remembers:\n\"{memory}\"";
        }
    }
}