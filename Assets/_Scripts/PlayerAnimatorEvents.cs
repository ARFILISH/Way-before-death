using UnityEngine;

namespace _Scripts
{
    public class PlayerAnimatorEvents : MonoBehaviour
    {
        [SerializeField] private AudioSource _footstepSource;

        public void Footstep()
        {
            _footstepSource.pitch = Random.Range(0.86f, 1.23f);
            _footstepSource.Play();
        }
    }
}
