using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Levels;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameData
{
    public static class SaveSystem
    {
        private static string _filepath = Application.persistentDataPath + "/Save.dat"; 
        
        public static void Save()
        {
            using (FileStream file = File.Create(_filepath))
            {
                new BinaryFormatter().Serialize(file, PlayerStats.Instance);
            }
        }

        public static void Load()
        {
            using (FileStream file = File.Open(_filepath, FileMode.OpenOrCreate))
            {
                if (file.Length == 0)
                {
                    PlayerStats.Instance = new PlayerStats();
                }
                else
                {
                    object loadedData = new BinaryFormatter().Deserialize(file);
                    PlayerStats.Instance = (PlayerStats) loadedData;
                }
            }
        }

        public static void DeleteSave()
        {
            File.Delete(_filepath);
        }
    }
}