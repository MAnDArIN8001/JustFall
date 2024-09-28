using System;
using UnityEngine;
using Zenject;

public class PlayerFreeRotator : MonoBehaviour
{
    public event Action<float> OnRotationCompute;

    [SerializeField] private float _rotationSpeed;

    private Vector2 _lastInput;

    private MainInput _mainInput;

    [Inject]
    private void Initialize(MainInput input)
    {
        _mainInput = input;
    }

    private void OnEnable()
    {
        if (_mainInput is not null)
        {
            _mainInput.Enable();
        }
    }

    private void OnDisable()
    {
        if (_mainInput is not null)
        {
            _mainInput.Disable();
        }
    }

    private void Update()
    {
        Vector2 inputValue = ReadInputValues();

        if (inputValue.x != 0)
        {
            transform.Rotate(0, _rotationSpeed * inputValue.x, 0);
        }

        if (inputValue.x != _lastInput.x)
        {
            OnRotationCompute?.Invoke(inputValue.x);
        }

        _lastInput = inputValue;
    }

    private Vector3 ConvertInputValueToGlobalDirection(Vector2 inputValue) => new Vector3(inputValue.x, 0, inputValue.y);

    private Vector2 ReadInputValues() => _mainInput.Controls.Movement.ReadValue<Vector2>().normalized;
}
