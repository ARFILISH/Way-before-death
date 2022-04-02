using UnityEngine;

namespace _Scripts
{
    public class Door : MonoBehaviour
    {
        private Animator _animator;
        private AudioSource _soundPlayer;
        
        [SerializeField] private bool _opened;
        [SerializeField] private AudioClip _openSound;
        [SerializeField] private AudioClip _closeSound;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _soundPlayer = GetComponent<AudioSource>();
        }
        
        private void FixedUpdate()
        {
            _animator.SetBool("Opened", _opened);
        }

        public void Open()
        {
            _soundPlayer.clip = _openSound;
            _soundPlayer.Play();
            _opened = true;
        }

        public void Close()
        {
            _soundPlayer.clip = _closeSound;
            _soundPlayer.Play();
            _opened = false;
        }

        public void Switch()
        {
            if (_opened) Close();
            else Open();
        }
    }
}
