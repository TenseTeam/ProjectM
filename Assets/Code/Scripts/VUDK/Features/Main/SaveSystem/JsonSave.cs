namespace VUDK.Features.Main.SaveSystem
{
    using System.IO;
    using Newtonsoft.Json;
    using UnityEngine;
    using VUDK.Features.Main.SaveSystem.Data;

    public static class JsonSave
    {
        private const string s_DefaultExtension = ".json";

        public static void Save<T>(T data) where T : SaveData
        {
            Save(data, typeof(T).Name);
        }

        public static void Save<T>(T data, string fileName, string extension = s_DefaultExtension) where T : SaveData
        {
            string path = Path.Combine(Application.persistentDataPath, fileName + extension.ToLower());
            string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);

            using (StreamWriter streamWriter = File.CreateText(path))
            {
                streamWriter.Write(jsonData);
            }
        }

        public static bool TryLoad<T>(out T data) where T : SaveData
        {
            return TryLoad(out data, typeof(T).Name);
        }

        public static bool TryLoad<T>(out T data, string fileName, string extension = s_DefaultExtension) where T : SaveData
        {
            string path = Path.Combine(Application.persistentDataPath, fileName + extension.ToLower());

            if (!File.Exists(path))
            {
                data = null;
                return false;
            }

            string jsonData = File.ReadAllText(path);
            data = JsonConvert.DeserializeObject<T>(jsonData);
            return true;
        }

        public static bool DeleteSave(string fileName, string extension = s_DefaultExtension)
        {
            string path = Path.Combine(Application.persistentDataPath, fileName + extension);

            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }

            return false;
        }

        public static string[] GetFilePaths(string extension = s_DefaultExtension)
        {
            string[] files = Directory.GetFiles(Application.persistentDataPath, "*" + extension);
            return files;
        }

        public static string[] GetFileNames(string extension = s_DefaultExtension)
        {
            string[] files = GetFilePaths(extension);
            for (int i = 0; i < files.Length; i++)
                files[i] = Path.GetFileNameWithoutExtension(files[i]);

            return files;
        }
    }
}
