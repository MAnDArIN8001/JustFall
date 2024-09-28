using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    public event Action<float> OnMovementVelocityCompute;

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
        }
    }

    private void OnDisable()
    {
        if (_input is not null)
        {
            _input.Disable();
        }   
    }

    private void FixedUpdate()
    {
        Vector2 inputValue = ReadInputValues();

        Move(inputValue);
    }

    private void Move(Vector2 inputValue)
    {
        Vector3 newVelocity = ConvertInputToLocalVelocity(inputValue);

        OnMovementVelocityCompute?.Invoke(inputValue.normalized.y);

        _rigidbody.velocity = newVelocity;
    }

    private Vector3 ConvertInputToLocalVelocity(Vector2 inputValue)
    {
        Vector3 velocityValue = transform.forward * inputValue.y * _characterModel.MovementSpeed;
        velocityValue.y = _rigidbody.velocity.y;

        return velocityValue;
    }

    private Vector2 ReadInputValues() => _input.Controls.Movement.ReadValue<Vector2>().normalized;
}
