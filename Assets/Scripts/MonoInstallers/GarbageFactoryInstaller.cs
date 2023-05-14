using UnityEngine;
using Zenject;

public class GarbageFactoryInstaller : MonoInstaller
{
    [SerializeField] GameObject _garbageFactoryPref;
    public override void InstallBindings()
    {
        var garbageFactoryPrefabInstance = Container.InstantiatePrefab(_garbageFactoryPref, 
            new Vector3(0, 0, 0), Quaternion.identity, transform.parent = null);
        Container.Bind<GameObject>().WithId("GarbageFactory").FromInstance(garbageFactoryPrefabInstance);
    }
}