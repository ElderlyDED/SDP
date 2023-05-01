using UnityEngine;
using Zenject;

public class GameStatusInstaller : MonoInstaller
{
    [SerializeField] GameObject _gameStatusPrefab;
    public override void InstallBindings()
    {
        var gameStatusInstance = Container.InstantiatePrefabForComponent<GameStatus>(_gameStatusPrefab);
        Container.Bind<GameStatus>().FromInstance(gameStatusInstance).AsSingle().NonLazy();
    }
}