using UnityEngine;
using UnityEngine.Events;

namespace _Scripts
{
    public class Timer : MonoBehaviour
    {
        private int _lastRoundValue;
        private int _currentRoundValue;
        
        public float timeInSeconds = 5;
        public bool loop = false;
        [SerializeField] private bool _active = false;

        private float _timeRemained = 5;
        
        [System.Serializable]
        public class SecondElapsed : UnityEvent<int> {}
        
        public UnityEvent coutdown;
        public UnityEvent secondElapsedNoArgs;
        public SecondElapsed secondElapsed;

        private void Awake()
        {
            _timeRemained = timeInSeconds;
        }

        private void Update()
        {
            if (_active)
            {
                if (_timeRemained > 0)
                {
                    _timeRemained -= Time.deltaTime;
                    _lastRoundValue = _currentRoundValue;
                    _currentRoundValue = Mathf.FloorToInt(_timeRemained);
                    if(_currentRoundValue != _lastRoundValue)
                    {
                        secondElapsedNoArgs.Invoke();
                        secondElapsed.Invoke(_currentRoundValue);
                    }
                }
                else
                {
                    _timeRemained = 0;
                    coutdown.Invoke();
                    _active = false;
                    if (loop)
                    {
                        _timeRemained = timeInSeconds;
                        _active = true;
                    }
                }
            }
        }

        public void StartTimer()
        {
            _timeRemained = timeInSeconds;
            _active = true;
        }

        public void StopTimer()
        {
            _active = false;
        }

        public void IncreaseTimer(float value)
        {
            _timeRemained += value;
        }
    }
}