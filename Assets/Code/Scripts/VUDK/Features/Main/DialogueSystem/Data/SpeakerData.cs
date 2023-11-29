namespace VUDK.Features.Main.DialogueSystem.Data
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "VUDK/Dialogue/Speaker")]
    public class SpeakerData : ScriptableObject
    {
        public string SpeakerName;
        public Sprite SpeakerImage;
    }
}