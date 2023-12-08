namespace VUDK.Features.AudioSFX
{
    using UnityEngine;
    using VUDK.Patterns.Pooling;
    using VUDK.Extensions;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.Initialization.Interfaces;
    using VUDK.Generic.Serializable;

    [RequireComponent(typeof(AudioSource))]
    public class AudioSFX : PooledObject, IInject<AudioClip>
    {
        private AudioSource _audioSource;
        private TimerTask _clipLenghtTask;

        private void Awake()
        {
            TryGetComponent(out _audioSource);
            _clipLenghtTask = new TimerTask();
        }

        private void OnEnable()
        {
            _clipLenghtTask.OnTaskCompleted += Stop;
        }

        private void OnDisable()
        {
            _clipLenghtTask.OnTaskCompleted -= Stop;
        }

        private void Update() => _clipLenghtTask.Process();

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

        public void Play()
        {
            PlayAtPosition(transform.position);
        }

        public void PlayAtPosition(Vector3 position)
        {
            _audioSource.Play();
            _clipLenghtTask.Start(_audioSource.clip.length);
            transform.SetPosition(position);
        }

        public void Stop()
        {
            Dispose();
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