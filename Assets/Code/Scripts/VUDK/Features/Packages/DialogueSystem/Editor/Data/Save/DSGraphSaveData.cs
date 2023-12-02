namespace VUDK.Features.Packages.DialogueSystem.Editor.Data.Save
{
    using System.Collections.Generic;
    using UnityEngine;

    public class DSGraphSaveData : ScriptableObject
    {
        public string FileName;
        public List<DSGroupSaveData> Groups;
        public List<DSNodeSaveData> Nodes;
        public List<string> OldGroupNames;
        public List<string> OldUngroupedNodeNames;
        public Dictionary<string, List<string>> OldGroupNodeNames;

        public void Init(string fileName)
        {
            FileName = fileName;
            Groups = new List<DSGroupSaveData>();
            Nodes = new List<DSNodeSaveData>();
            OldGroupNames = new List<string>();
            OldUngroupedNodeNames = new List<string>();
            OldGroupNodeNames = new Dictionary<string, List<string>>();
        }
    }
}