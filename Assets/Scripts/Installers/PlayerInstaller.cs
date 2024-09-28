using Cinemachine;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Quaternion _initialRotation;

    [SerializeField] private Transform _initialPosition;

    [SerializeField] private GameObject _playerPrefab;

    [SerializeField] private CinemachineVirtualCamera _mainVirtualCamera;

    public override void InstallBindings()
    {
        Container.Bind<CharacterModel>().FromResources(PlayerResourcesConsts.PlayerMainModel);

        GameObject playerInstance = Container.InstantiatePrefab(_playerPrefab, _initialPosition.position, _initialRotation, null);

        _mainVirtualCamera.LookAt = playerInstance.transform;
        _mainVirtualCamera.Follow = playerInstance.transform;
    }
}
