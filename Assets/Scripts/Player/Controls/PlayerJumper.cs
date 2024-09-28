using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJumper : MonoBehaviour
{
    public event Action OnJumped;
    public event Action OnLanded;

    [SerializeField] private bool _isOnGround;

    [SerializeField] private float _groundCheckDistance;

    [SerializeField] private LayerMask _groundableLayers;

    [SerializeField] private CharacterModel _characterModel;

    private Rigidbody _rigidbody;

    private MainInput _input;

    [Inject]
    private void Initialize(MainInput input, CharacterModel characterModel)
    {
        _input = input;
        _characterModel = characterModel;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        if (_input is not null)
        {
            _input.Enable();

            _input.Controls.Jump.performed += HandleJump;
        }
    }

    private void OnDisable()
    {
        if (_input is not null)
        {
            _input.Disable();

            _input.Controls.Jump.performed -= HandleJump;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isOnGround
            && Physics.Raycast(transform.position, -transform.up, _groundCheckDistance, _groundableLayers))
        {
            _isOnGround = true;

            OnLanded?.Invoke();
        }
    }

    private void HandleJump(InputAction.CallbackContext context)
    {
        if (!_isOnGround)
        {
            return;
        }

        _isOnGround = false;

        OnJumped?.Invoke();

        Jump();
    }

    private void Jump()
    {
        Vector3 newVelocity = new Vector3()
        {
            x = _rigidbody.velocity.x,
            y = _characterModel.JumpForce,
            z = _rigidbody.velocity.z
        };

        _rigidbody.velocity = newVelocity;
    }
}
