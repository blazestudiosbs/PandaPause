using PandaPause.Core;
using UnityEngine;

namespace PandaPause.UI
{
    public class DeveloperToolsUI : MonoBehaviour
    {
        public void ClearMemories()
{
    JournalSaveSystem.DeleteDatabase();
    Debug.Log("Panda Pause memories cleared.");
}
        public void ResetProfile()
        {
            PandaSaveSystem.DeleteProfile();
            Debug.Log("Panda Pause profile reset.");
        }
    }
}