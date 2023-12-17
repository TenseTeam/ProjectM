namespace VUDK.Features.Main.SaveSystem.Bases
{
    using VUDK.Features.Main.SaveSystem.Interfaces;
    using VUDK.Features.Main.SaveSystem.Data;
    using UnityEngine;

    public abstract class BinarySaverBase<T> : MonoBehaviour, ISaverLoader where T : SaveData
    {
        protected T SaveData;

        protected virtual void OnLoadFail() { }

        protected virtual void OnLoadSuccess(T saveData)
        {
            SaveData = saveData;
        }

        public void Load(string fileName, string extension)
        {
            if (BinarySave.TryLoad(out SaveData, fileName, extension))
                OnLoadSuccess(SaveData);
            else
                OnLoadFail();
        }

        public void Save(string fileName, string fileExtension)
        {
            BinarySave.Save(SaveData, fileName, fileExtension);
        }
    }
}