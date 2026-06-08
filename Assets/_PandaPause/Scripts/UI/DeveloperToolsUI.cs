using PandaPause.Core;
using UnityEngine;

namespace PandaPause.UI
{
    public class DeveloperToolsUI : MonoBehaviour
    {
        public void ResetProfile()
        {
            PandaSaveSystem.DeleteProfile();
            Debug.Log("Panda Pause profile reset.");
        }
    }
}