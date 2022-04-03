using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace _Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : MonoBehaviour
    {
        private AudioSource _musicSource;

        public static MusicPlayer instance { get; private set; }

        private void Start()
        {
            if (instance != null)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(this);
                _musicSource = GetComponent<AudioSource>();   
            }
        }

        public void Play(AudioClip musicToPlay, bool loop, float fadeDuration = 0)
        {
            Stop(0f);
            _musicSource.clip = musicToPlay;
            _musicSource.loop = loop;
            _musicSource.Play();
            _musicSource.volume = 0;
            _musicSource.DOFade(0.7f, fadeDuration);
        }

        public void Stop(float fadeDuration)
        {
            _musicSource.DOFade(0, fadeDuration = 0);
            if (fadeDuration > 0)
            {
                StartCoroutine(nameof(StopMusic), fadeDuration);
            }
            else _musicSource.Stop();
        }

        private IEnumerator StopMusic(float fadeDuration)
        {
            yield return new WaitForSeconds(fadeDuration);
            _musicSource.Stop();
        }

        public void PlayCurrent(bool loop, float fadeDuration = 0)
        {
            if (!_musicSource.isPlaying)
            {
                Play(_musicSource.clip, loop, fadeDuration);
            }
        }
    }
}
