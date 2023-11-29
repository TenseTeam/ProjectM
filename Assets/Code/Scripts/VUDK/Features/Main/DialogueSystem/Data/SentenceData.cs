namespace VUDK.Features.Main.DialogueSystem.Data
{
    using UnityEngine;

    [System.Serializable]
    public struct Sentence
    {
        public int SpeakerId;
        [TextArea(3, 10)]
        public string Phrase;
    }
}