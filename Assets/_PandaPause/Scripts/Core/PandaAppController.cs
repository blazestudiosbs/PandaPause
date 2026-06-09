using UnityEngine;

namespace PandaPause.Core
{

    
    public class PandaAppController : MonoBehaviour
    {
        public static PandaAppController Instance { get; private set; }

        public PandaProfile CurrentProfile { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            CurrentProfile = PandaSaveSystem.LoadProfile();
        }

        public void CompleteSetup(string userName, string pandaName)
        {
            CurrentProfile.userName = userName.Trim();
            CurrentProfile.pandaName = pandaName.Trim();
            CurrentProfile.hasCompletedSetup = true;

            PandaSaveSystem.SaveProfile(CurrentProfile);
        }

        public void ResetProfile()
        {
            PandaSaveSystem.DeleteProfile();
            CurrentProfile = new PandaProfile();
        }
    }
}