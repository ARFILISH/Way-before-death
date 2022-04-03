using _Scripts;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    /* Components */
    private CharacterController _controller;
    private Animator _animator;
    [SerializeField] private AudioSource _variousSource;

    private Vector3 _velocity;
    public float _maxMoveSpeed = 8;
    [SerializeField] private float _defaultMoveSpeed = 8;
    [SerializeField] private float _grabMoveSpeed = 5;
    [SerializeField] private float _turnSpeed = 56;
    private bool _grounded;
    [SerializeField] private float _gravity = -0.5f;
    private bool _alive = true;
    [SerializeField] private AudioClip _deathSound;

    [SerializeField] private bool _inputEnabled = true;
    
    public PickupableObject currentlyHoldedObject;

    public UnityEvent death;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _maxMoveSpeed = _defaultMoveSpeed;
    }

    private void Update()
    {
        _grounded = _controller.isGrounded;

        UpdateAnimations();
        UpdateMovement();
        if(_alive && _inputEnabled)
            InteractingRays();
    }

    private void UpdateMovement()
    {
        if (_grounded)
            _velocity.y = 0;
        Vector3 rotationVector;
        if (_alive && _inputEnabled)
            rotationVector = (Input.GetAxisRaw("Vertical") * transform.forward +
                              Input.GetAxisRaw("Horizontal") * transform.right).normalized;
        else rotationVector = Vector3.zero;
        Vector3 desiredVelocity = new Vector3(rotationVector.x, 0 ,rotationVector.z) * (_maxMoveSpeed * Time.deltaTime) + new Vector3(0, _velocity.y + _gravity * Time.deltaTime, 0);
        _velocity = Vector3.MoveTowards(_velocity, desiredVelocity, Time.deltaTime * 1000);
        if(_alive && _inputEnabled) transform.Rotate(new Vector3(0, 1, 0) * (Input.GetAxisRaw("Turn") * _turnSpeed * Time.deltaTime));
        _controller.Move(_velocity);
    }

    private void UpdateAnimations()
    {
        _animator.SetFloat("Speed", Input.GetAxisRaw("Vertical"), 0.06f, Time.deltaTime);
        _animator.SetFloat("Direction", Input.GetAxisRaw("Horizontal"), 0.06f, Time.deltaTime);
        _animator.SetBool("Grounded", _grounded);
    }

    public void Die()
    {
        if (_alive)
        {
            Throw();
            _alive = false;
            _animator.SetTrigger("Die");
            _variousSource.clip = _deathSound;
            _variousSource.Play();
            death.Invoke();
        }
    }

    public void InteractingRays()
    {
        if (currentlyHoldedObject != null)
        {
            if(Input.GetButtonDown("Submit"))
                Throw();
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position,  transform.forward, out hit, 2f))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    if (Input.GetButtonDown("Submit"))
                        interactable.Interact(gameObject);
                }
            }   
        }
    }

    public void Grab()
    {
        _maxMoveSpeed = _grabMoveSpeed;
    }

    public void Throw()
    {
        if (currentlyHoldedObject != null)
        {
            currentlyHoldedObject.Throw();
            _maxMoveSpeed = _defaultMoveSpeed;
            currentlyHoldedObject.transform.parent = null;
            currentlyHoldedObject = null;   
        }
    }
}
