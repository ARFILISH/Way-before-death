using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts
{
    public class BigButton : MonoBehaviour
    {
        /* Components */
        private AudioSource _soundPlayer;
        private Animator _animator;
        
        [SerializeField] private List<GameObject> _allowedBodies;

        [SerializeField] private bool _enabled = true;
        [SerializeField] private AudioClip _pressSound;
        [SerializeField] private AudioClip _releaseSound;

        public UnityEvent buttonPressed;
        public UnityEvent buttonReleased;

        private bool _active = false;

        private List<Collision> _currentCollisions = new List<Collision>();

        private void Awake()
        {
            _soundPlayer = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _animator.SetBool("Pressed", _active);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_enabled)
            {
                if (_allowedBodies.Contains(collision.body.gameObject))
                {
                    _currentCollisions.Add(collision);
                }
                if (!_active && _currentCollisions.Count > 0)
                {
                    _soundPlayer.clip = _pressSound;
                    _soundPlayer.Play();
                    buttonPressed.Invoke();
                    _active = true;
                }
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (_enabled)
            {
                if (_currentCollisions.Contains(other))
                {
                    _currentCollisions.Remove(other);
                }

                if (_active && _currentCollisions.Count <= 0)
                {
                    buttonReleased.Invoke();
                    _soundPlayer.clip = _releaseSound;
                    _soundPlayer.Play();
                    _active = false;
                }
            }
        }

        public void Enable()
        {
            _enabled = true;
        }

        public void Disable()
        {
            _enabled = false;
            _active = false;
        }
    }
}
