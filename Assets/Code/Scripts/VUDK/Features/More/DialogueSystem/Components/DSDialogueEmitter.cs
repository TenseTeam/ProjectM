namespace VUDK.Features.More.DialogueSystem.Components
{
    using UnityEngine;
    using UnityEngine.Events;
    using VUDK.Features.More.DialogueSystem.Data;
    using VUDK.Features.More.DialogueSystem.Events;

    [RequireComponent(typeof(AudioSource))]
    public class DSDialogueEmitter : MonoBehaviour
    {
        [Header("Source Actor")]
        [SerializeField]
        private DSActorData _actorData;

        private AudioSource _audioSource;

        [Header("Events")]
        public UnityEvent OnBeginSpeaking;
        public UnityEvent OnEndSpeaking;

        private void Awake()
        {
            TryGetComponent(out _audioSource);
        }

        private void OnEnable()
        {
            _actorData.OnEmitDialogue += EmitDialogue;
            DSEvents.OnDMNext += Stop;
            DSEvents.OnDMDisable += Stop;
        }

        private void OnDisable()
        {
            _actorData.OnEmitDialogue -= EmitDialogue;
            DSEvents.OnDMNext -= Stop;
            DSEvents.OnDMDisable -= Stop;
        }

        private void EmitDialogue(AudioClip clip)
        {
            OnBeginSpeaking?.Invoke();
            _audioSource.PlayOneShot(clip);
        }

        private void Stop()
        {
            OnEndSpeaking?.Invoke();
            _audioSource.Stop();
        }
//#if UNITY_EDITOR

//        private void OnDrawGizmos()
//        {
//            Gizmos.DrawIcon(transform.position, "test.png", true);// TODO: Do this if is a package
//        }
//#endif
    }
}