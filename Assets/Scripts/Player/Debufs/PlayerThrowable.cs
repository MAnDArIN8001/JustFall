using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerThrowable : MonoBehaviour, IThrowable
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Throw(Vector3 throwDirection, float throwingForce)
    {
        _rigidbody.velocity = throwDirection * throwingForce;
    }
}
