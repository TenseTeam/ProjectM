namespace ProjectM.Features.Artwork
{
    using System;
    using UnityEngine;
    using VUDK.Features.More.DialogueSystem.Components;

    [RequireComponent(typeof(DSDialogueEmitter))]
    public class PaintArtworkActor : PaintArtwork
    {
        [Header("Artwork Animator")]
        [SerializeField]
        private Animator _animator;

        private DSDialogueEmitter _dialogueEmitter;

        protected override void Awake()
        {
            base.Awake();
            TryGetComponent(out _dialogueEmitter);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _dialogueEmitter.OnBeginSpeaking.AddListener(PlaySpeakAnimation);
            _dialogueEmitter.OnEndSpeaking.AddListener(StopSpeakAnimation);
        }

        private void PlaySpeakAnimation()
        {
            _animator.SetBool("IsSpeaking", true);
        }

        private void StopSpeakAnimation()
        {
            _animator.SetBool("IsSpeaking", false);
        }
    }
}
