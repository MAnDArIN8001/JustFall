using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerView : MonoBehaviour
{
    private PlayerMover _playerMover;

    private PlayerFreeRotator _playerFreeRotator;

    private PlayerJumper _playerJumper;

    private Animator _animator;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _playerFreeRotator = GetComponent<PlayerFreeRotator>();
        _playerJumper = GetComponent<PlayerJumper>();

        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (_playerMover is not null)
        {
            _playerMover.OnMovementVelocityCompute += HandleMovement;
        }   

        if (_playerFreeRotator is not null)
        {
            _playerFreeRotator.OnRotationCompute += HandleRotation;
        }

        if ( _playerJumper is not null)
        {
            _playerJumper.OnJumped += HandleJump;
            _playerJumper.OnLanded += HandleLanding;
        }
    }

    private void OnDisable()
    {
        if (_playerMover is not null)
        {
            _playerMover.OnMovementVelocityCompute -= HandleMovement;
        }

        if (_playerFreeRotator is not null)
        {
            _playerFreeRotator.OnRotationCompute -= HandleRotation;
        }

        if (_playerJumper is not null)
        {
            _playerJumper.OnJumped -= HandleJump;
            _playerJumper.OnLanded -= HandleLanding;
        }
    }

    private void HandleMovement(float movementVelocity)
    {
        _animator.SetFloat(PlayerAnimationConsts.MovementParametrKey, movementVelocity);
    }

    private void HandleRotation(float rotationVelocity)
    {
        _animator.SetFloat(PlayerAnimationConsts.RotationParametrKey, rotationVelocity);
    }

    private void HandleJump()
    {
        _animator.SetTrigger(PlayerAnimationConsts.OnJumpedTriggerKey);
    }

    private void HandleLanding() 
    {
        _animator.SetTrigger(PlayerAnimationConsts.OnLandedTriggerKey);
    }
}
