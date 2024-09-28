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
        
    }

    private void OnDisable()
    {
        
    }
}
