using System;
using UnityEngine;

public class FireObstacle : MonoBehaviour
{
    public event Action OnPreparedAttack;
    public event Action OnMadeAttack;

    private bool _isCanShoot;

    [SerializeField] private float _preparingAttackTime;
    [SerializeField] private float _timeToMakeAttack;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _damage;
    private float _currentPreparingAttackTime;

    private void OnCollisionStay(Collision collision)
    {
        
    }
}
