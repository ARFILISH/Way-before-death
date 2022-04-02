using System;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] private int _numberToOpen = 2;
        [SerializeField] private bool _opened = false;
        
        public UnityEvent gateOpened;
        public UnityEvent gateClosed;

        private int _currentNumber = 0;

        private void Start()
        {
            if (_opened)
            {
                SetGateNumber(_numberToOpen);
            }
        }
        
        public void OpenGate()
        {
            _opened = true;
            gateOpened.Invoke();
        }

        public void CloseGate()
        {
            _opened = false;
            gateClosed.Invoke();
        }

        public void SetGateNumber(int number)
        {
            _currentNumber = Mathf.Min(number, _numberToOpen);
            if (_currentNumber == _numberToOpen && !_opened)
            {
                OpenGate();
            }
            else if (_currentNumber != _numberToOpen && _opened)
            {
                CloseGate();
            }
        }

        public void AddGateNumber()
        {
            SetGateNumber(_currentNumber + 1);
        }

        public void SubstractGateNumber()
        {
            SetGateNumber(_currentNumber - 1);
        }

        public void ResetGate()
        {
            SetGateNumber(0);
        }
    }
}
