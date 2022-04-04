using System;
using UnityEngine;

namespace _Scripts
{
    public class PickupableObject : MonoBehaviour, IInteractable
    {
        private AudioSource _soundPlayer;
        private bool _pickedUp = false;
        private Rigidbody _rigidBody;

        [SerializeField] private string _name = "default name";
        
        [SerializeField] private AudioClip _pickupSound;
        [SerializeField] private AudioClip _throwSound;
        
        public void Interact(GameObject interactedObject)
        {
            if (!_pickedUp)
            {
                PickUp(interactedObject);
            }
            else Throw();
        }

        public string Description()
        {
            return "pickup " + _name;
        }

        private void Awake()
        {
            _soundPlayer = GetComponent<AudioSource>();
            _rigidBody = GetComponent<Rigidbody>();
        }

        private void PickUp(GameObject interactedObject)
        {
            _pickedUp = true;
            transform.parent = interactedObject.transform;
            transform.position = transform.parent.position + transform.parent.forward * 1.5f + new Vector3(0, 0.5f, 0);
            _rigidBody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            if (interactedObject.GetComponent<PlayerController>() != null)
            {
                interactedObject.GetComponent<PlayerController>().currentlyHoldedObject = this;
                interactedObject.GetComponent<PlayerController>().Grab();
                _soundPlayer.clip = _pickupSound;
                _soundPlayer.Play();
            }
        }

        public void Throw()
        {
            _rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
            _pickedUp = false;
            _soundPlayer.clip = _throwSound;
            _soundPlayer.Play();
            transform.position = transform.parent.position + transform.parent.forward * 0.93f + new Vector3(0, 0.5f, 0);
            transform.parent = null;
        }

        private void Update()
        {
            if (_pickedUp)
            {
                _rigidBody.MovePosition(transform.position = transform.parent.position + transform.parent.forward * 0.75f + new Vector3(0, 0.5f, 0));
            }
        }
    }
}
