using PandaPause.Core;
using UnityEngine;
using UnityEngine.UI;

namespace PandaPause.UI
{
    public class PandaNameTextUI : MonoBehaviour
    {
        [SerializeField] private Text targetText;

        [TextArea]
        [SerializeField] private string template = "{PandaName}";

        private void OnEnable()
        {
            Refresh();
        }

        public void Refresh()
        {
            if (targetText == null)
            {
                Debug.LogError("PandaNameTextUI is missing Target Text reference.");
                return;
            }

            string pandaName = GetPandaName();

            Debug.Log($"PandaNameTextUI resolved panda name: '{pandaName}'");

            targetText.text = template.Replace("{PandaName}", pandaName);
        }

        private string GetPandaName()
        {
            PandaProfile profile = PandaSaveSystem.LoadProfile();

            if (profile == null)
            {
                Debug.Log("PandaNameTextUI profile is NULL.");
                return "your panda";
            }

            Debug.Log($"PandaNameTextUI profile user='{profile.userName}' panda='{profile.pandaName}'");

            if (!string.IsNullOrWhiteSpace(profile.pandaName))
            {
                return profile.pandaName;
            }

            return "your panda";
        }
    }
}