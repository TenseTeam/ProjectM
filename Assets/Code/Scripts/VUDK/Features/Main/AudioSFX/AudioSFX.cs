namespace VUDK.Features.AudioSFX
{
    using UnityEngine;
    using VUDK.Patterns.Pooling;
    using VUDK.Extensions;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.Initialization.Interfaces;

    [RequireComponent(typeof(AudioSource))]
    public class AudioSFX : PooledObject, IInject<AudioClip>
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            TryGetComponent(out _audioSource);
        }

        public static AudioSFX Create(AudioClip clip)
        {
            GameObject goAud = MainManager.Ins.PoolsManager.Pools[PoolKeys.AudioSFX].Get();
            if (goAud.TryGetComponent(out AudioSFX audioSFX))
                audioSFX.Inject(clip);

            return audioSFX;
        }

        public void Inject(AudioClip clip)
        {
            _audioSource.clip = clip;
        }

        public void PlayClipAtPoint(Vector3 position)
        {
            _audioSource.Play();
            Invoke("Dispose", _audioSource.clip.length);
            transform.SetPosition(position);
        }

        public override void Clear()
        {
            _audioSource.Stop();
            _audioSource.clip = null;
        }

        public bool Check()
        {
            return _audioSource.clip != null;
        }
    }
}