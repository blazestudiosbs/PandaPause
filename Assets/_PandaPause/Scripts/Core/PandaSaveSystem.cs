using System.IO;
using UnityEngine;

namespace PandaPause.Core
{
    public static class PandaSaveSystem
    {
        private const string SaveFileName = "panda_profile.json";

        private static string SavePath =>
            Path.Combine(Application.persistentDataPath, SaveFileName);

        public static PandaProfile LoadProfile()
        {
            if (!File.Exists(SavePath))
            {
                return new PandaProfile();
            }

            string json = File.ReadAllText(SavePath);
            return JsonUtility.FromJson<PandaProfile>(json);
        }

        public static void SaveProfile(PandaProfile profile)
        {
            string json = JsonUtility.ToJson(profile, true);
            File.WriteAllText(SavePath, json);
        }

        public static void DeleteProfile()
        {
            if (File.Exists(SavePath))
            {
                File.Delete(SavePath);
            }
        }
    }
}