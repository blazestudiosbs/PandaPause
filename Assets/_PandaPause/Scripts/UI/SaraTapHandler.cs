using UnityEngine;

namespace PandaPause.UI
{
    public class SaraTapHandler : MonoBehaviour
    {
        [SerializeField] private DeveloperPanelUnlocker unlocker;

        private void OnMouseDown()
        {
            if (unlocker != null)
            {
                unlocker.RegisterTap();
            }
        }
    }
}