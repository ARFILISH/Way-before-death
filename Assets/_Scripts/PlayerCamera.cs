using UnityEngine;

namespace _Scripts
{
    public class PlayerCamera : MonoBehaviour
    {
        public GameObject target;

        private Vector3 _velocity;

        private void Awake()
        {
            transform.position = target.transform.position;
        }

        private void Update()
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.transform.position, ref _velocity, 0.15f);
        }
    }
}
