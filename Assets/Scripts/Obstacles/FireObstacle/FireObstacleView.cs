using UnityEngine;

public class FireObstacleView : MonoBehaviour
{
    private FireObstacle _fireObstacle;

    private void Awake()
    {
        _fireObstacle = GetComponent<FireObstacle>();
    }

    private void OnEnable()
    {
        if (_fireObstacle is not null)
        {
            _fireObstacle.OnMadeAttack += HandleAttack;
        }
    }

    private void OnDisable()
    {
        if (_fireObstacle is not null)
        {
            _fireObstacle.OnMadeAttack -= HandleAttack;
        }
    }

    private void HandleAttack()
    {

    }
}
