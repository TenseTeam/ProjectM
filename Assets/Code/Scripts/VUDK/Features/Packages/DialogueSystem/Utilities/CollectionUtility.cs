namespace VUDK.Features.Packages.DialogueSystem.Utilities
{
    using System.Collections.Generic;

    public static class CollectionUtility
    {
        public static void AddItem<T, TKey>(this SerializableDictionary<T, List<TKey>> serializableDictionary, T key, TKey value)
        {
            if (serializableDictionary.ContainsKey(key))
            {
                serializableDictionary[key].Add(value);
                return;
            }

            serializableDictionary.Add(key, new List<TKey>() { value });
        }
    }
}