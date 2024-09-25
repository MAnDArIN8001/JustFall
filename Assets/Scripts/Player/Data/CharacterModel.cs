using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterModel", menuName = "Gameplay/CharacterModel")]
public class CharacterModel : ScriptableObject
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;

    public float MovementSpeed => _movementSpeed;
    public float JumpForce => _jumpForce;
}
