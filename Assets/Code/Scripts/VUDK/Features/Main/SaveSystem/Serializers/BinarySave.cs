namespace VUDK.Features.Main.SaveSystem.Serializers
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using UnityEngine;
    using VUDK.Features.Main.SaveSystem.SaveData;

    public static class BinarySave
    {
        private const string s_DefaultExtension = ".bin";

        public static void Save(SavePacketData data, string fileName, string extension = s_DefaultExtension)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Path.Combine(Application.persistentDataPath, fileName + extension.ToLower());

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(stream, data);
            }
        }

        public static bool TryLoad(out SavePacketData data, string fileName, string extension = s_DefaultExtension)
        {
            string path = Path.Combine(Application.persistentDataPath, fileName + extension.ToLower());

            if (!File.Exists(path))
            {
                data = null;
                return false;
            }

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                data = (SavePacketData)formatter.Deserialize(stream);
                return true;
            }
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
    }
}
