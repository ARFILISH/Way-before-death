using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace _Scripts
{
    public class LevelScript : MonoBehaviour
    {
        public UnityEvent levelStarted;

        private void Start()
        {
            levelStarted.Invoke();
        }
        
        public void ChangeLevel(string level)
        {
            SceneManager.LoadSceneAsync(level, LoadSceneMode.Single);
        }

        public void Quit()
        {
            Application.Quit();
        }
        
        public void PlayMusic(AudioClip musicToPlay, bool loop, float fadeDuration)
        {
            if(MusicPlayer.instance != null)
            MusicPlayer.instance.Play(musicToPlay, loop, fadeDuration);
        }
        public void PlayMusicLoopNoFade(AudioClip musicToPlay)
        {
            if(MusicPlayer.instance != null)
            MusicPlayer.instance.Play(musicToPlay, true, 0);
        }
        
        public void PlayMusicLoopNoLoopNoFade(AudioClip musicToPlay)
        {
            if(MusicPlayer.instance != null)
                MusicPlayer.instance.Play(musicToPlay, false, 0);
        }

        public void StopMusic(float fadeDuration)
        {
            if(MusicPlayer.instance != null)
            MusicPlayer.instance.Stop(fadeDuration);
        }

        public void PlayCurrent(bool loop, float fadeDuration)
        {
            if(MusicPlayer.instance != null)
            MusicPlayer.instance.PlayCurrent(loop, fadeDuration);
        }
        
        public void PlayCurrentLoop(float fadeDuration)
        {
            if(MusicPlayer.instance != null)
            MusicPlayer.instance.PlayCurrent(true, fadeDuration);
        }
    }
}
