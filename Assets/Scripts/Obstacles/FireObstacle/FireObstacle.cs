using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireObstacle : MonoBehaviour
{
    public event Action OnMadeAttack;
    public event Action<float> OnPreparingTimeChanged;

    [SerializeField] private bool _isCanShoot;

    [SerializeField] private float _preparingThrowTime;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _throwingForce;
    [SerializeField] private float _currentPreparingThrowTime;

    [SerializeField] private List<IThrowable> _currentTrowables = new List<IThrowable>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IThrowable>(out var throwable))
        {
            if (!_currentTrowables.Contains(throwable))
            {
                _currentTrowables.Add(throwable);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<IThrowable>(out var throwable)
            && _isCanShoot)
        {
            _currentPreparingThrowTime += Time.deltaTime;

            OnPreparingTimeChanged?.Invoke(_currentPreparingThrowTime);

            if (_currentPreparingThrowTime >= _preparingThrowTime)
            {
                OnMadeAttack?.Invoke();

                MakeThrow();

                _isCanShoot = false;
                _currentPreparingThrowTime = 0;

                StartCoroutine(MakeReload());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<IThrowable>(out var throwable))
        {
            if (_currentTrowables.Contains(throwable))
            {
                _currentTrowables.Remove(throwable);
            }
        }
    }

    private Vector3 GetRandomDirection()
    {
        Vector3 randomDirection = new Vector3()
        {
            x = UnityEngine.Random.Range(0f, 1f),
            y = 1,
            z = UnityEngine.Random.Range(0f, 1f)
        };

        return randomDirection;
    }

    private void MakeThrow()
    {
        foreach (var throwable in _currentTrowables)
        {
            Vector3 randomDirection = GetRandomDirection();

            throwable.Throw(randomDirection, _throwingForce);
        }
    }

    private IEnumerator MakeReload()
    {
        yield return new WaitForSeconds(_reloadTime);

        _isCanShoot = true;
    }
}
