using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts
{
    public class Button : MonoBehaviour, IInteractable
    {
        private AudioSource _soundPlayer;
        private Animator _animator;

        public int maxInteractions = 0; // 0 or lower is infinity times
        private int _currentInteraction = 0; // Sets only if there isn't infinity interactions.
        [SerializeField] private bool _enabled = true;
        [SerializeField] private float _delayBeforeReset = 2;
        [SerializeField] private AudioClip _pressSound;
        [SerializeField] private AudioClip _releaseSound;
        [SerializeField] private AudioClip _errorSound;
        [SerializeField] private bool _lever = false;

        private bool _pressed = false;

        public UnityEvent pressed;
        public UnityEvent released;

        private void Update()
        {
            _animator.SetBool("Pressed", _pressed);
        }
        
        private void Awake()
        {
            _soundPlayer = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();
        }
        
        public void Interact(GameObject interactedObject)
        {
            if (_enabled && (_currentInteraction < maxInteractions || maxInteractions <= 0))
            {
                Press();
            }
            else
            {
                _soundPlayer.clip = _errorSound;
                _soundPlayer.Play();
            }
        }

        public void Press()
        {
            if (!_pressed)
            {
                if (maxInteractions > 0)
                    _currentInteraction++;
                _pressed = true;
                _soundPlayer.clip = _pressSound;
                _soundPlayer.Play();
                pressed.Invoke();
                if (_delayBeforeReset > 0) StartCoroutine(nameof(ResetTimer));
            }
            else if (_lever)
            {
                Release();
            }
        }

        private IEnumerator ResetTimer()
        {
            yield return new WaitForSeconds(_delayBeforeReset);
            Release();
        }
        
        public void Release()
        {
            StopCoroutine(nameof(ResetTimer));
            if (_pressed)
            {
                _pressed = false;
                _soundPlayer.clip = _releaseSound;
                _soundPlayer.Play();
                released.Invoke();
            }
        }

        public void Reset()
        {
            _currentInteraction = 0;
            StopCoroutine(nameof(ResetTimer));
            _pressed = false;
        }
    }
}
