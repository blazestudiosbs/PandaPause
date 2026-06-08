using System.IO;
using UnityEngine;

namespace PandaPause.Core
{
    
    public static class JournalSaveSystem
    {
        public static void DeleteDatabase()
{
    if (File.Exists(SavePath))
    {
        File.Delete(SavePath);
    }
}
        private static readonly string SavePath =
            Path.Combine(Application.persistentDataPath, "journal.json");

        public static void SaveDatabase(JournalDatabase database)
        {
            string json = JsonUtility.ToJson(database, true);
            File.WriteAllText(SavePath, json);
        }

        public static JournalDatabase LoadDatabase()
        {
            if (!File.Exists(SavePath))
                return new JournalDatabase();

            string json = File.ReadAllText(SavePath);
            return JsonUtility.FromJson<JournalDatabase>(json);
        }
    }
}