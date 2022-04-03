using UnityEngine;
using UnityEngine.Events;

namespace _Scripts
{
    public class ConditionalEvent : MonoBehaviour
    {
        [SerializeField] private bool _condition = false;

        public UnityEvent conditionTrue;
        public UnityEvent conditionFalse;
        
        public void Call()
        {
            (_condition ? conditionTrue : conditionFalse).Invoke();
        }

        public void EnableCondition()
        {
            _condition = true;
        }
        
        public void DisableCondition()
        {
            _condition = false;
        }
    }
}
