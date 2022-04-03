using UnityEngine;

namespace _Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class PositionSwitcher : MonoBehaviour
    {
        private AudioSource _soundPlayer;

        [SerializeField] private AudioClip _activateSound;
        [SerializeField] private AudioClip _deactivateSound;
        
        [SerializeField] private bool _active = true;
        [SerializeField] private Vector3 _notActivePosition;
        [SerializeField] private float _smoothing = 500f;
        private Vector3 _initialPosition;
        private Vector3 _velocity;

        private void Awake()
        {
            _soundPlayer = GetComponent<AudioSource>();
            _initialPosition = transform.position;
        }
        
        private void Start()
        {
            transform.position = _active ? _initialPosition : _initialPosition + _notActivePosition;
        }
        
        private void FixedUpdate()
        {
            transform.position = Vector3.SmoothDamp(transform.position, _active ? _initialPosition : _initialPosition + _notActivePosition, ref _velocity, _smoothing * Time.deltaTime);
        }

        public void Activate()
        {
            _active = true;
            _soundPlayer.clip = _activateSound;
            _soundPlayer.Play();
        }
        public void Dectivate()
        {
            _active = false;
            _soundPlayer.clip = _deactivateSound;
            _soundPlayer.Play();
        }
    }
}
