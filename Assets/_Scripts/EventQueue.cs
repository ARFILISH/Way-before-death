using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts
{
    public class EventQueue : MonoBehaviour
    {
        public List<UnityEvent> eventsToCall;

        [SerializeField] private int _currentQueuePlace = 0;

        public void CallQueuePlace(int place)
        {
            _currentQueuePlace = Mathf.Min(place, eventsToCall.Count - 1);
            eventsToCall[_currentQueuePlace].Invoke();
        }

        public void AddQueuePlace()
        {
            CallQueuePlace(_currentQueuePlace + 1);
        }
        
        public void SubstractQueuePlace()
        {
            CallQueuePlace(_currentQueuePlace - 1);
        }
    }
}