using UnityEngine;
using Zenject;

public class PlanetInstaller : MonoInstaller
{
    [SerializeField] GameObject _planetPref;
    public override void InstallBindings()
    {
        var planetPrefabInstance = Container.InstantiatePrefab(_planetPref,
            new Vector3(0f, 0f, 0f), Quaternion.identity, transform.parent = null);
        Container.Bind<GameObject>().WithId("Planet").FromInstance(planetPrefabInstance).Lazy();
    }
}