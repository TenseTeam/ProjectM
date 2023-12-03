namespace VUDK.Features.Packages.DialogueSystem.Editor.Data.Save
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Features.Packages.DialogueSystem.Utilities;

    public class DSGraphSaveData : ScriptableObject
    {
        public string FileName;
        public List<DSGroupSaveData> Groups;
        public List<DSNodeSaveData> Nodes;
        public List<string> OldGroupNames;
        public List<string> OldUngroupedNodeNames;
        public SerializableDictionary<string, List<string>> OldGroupedNodeNames;

        public void Init(string fileName)
        {
            FileName = fileName;
            Groups = new List<DSGroupSaveData>();
            Nodes = new List<DSNodeSaveData>();
            OldGroupNames = new List<string>();
            OldUngroupedNodeNames = new List<string>();
            OldGroupedNodeNames = new SerializableDictionary<string, List<string>>();
        }
    }
}