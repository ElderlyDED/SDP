using System.ComponentModel;
using UnityEngine;
using Zenject;

public class PlayerInputInstaller : MonoInstaller
{
    [SerializeField] PlayerInput _playerInputScript;
    public override void InstallBindings()
    {
        var playerInputInstance = Container.InstantiatePrefabForComponent<PlayerInput>(_playerInputScript);
        Container.Bind<PlayerInput>().FromInstance(playerInputInstance).AsSingle().NonLazy();
        
    }
}