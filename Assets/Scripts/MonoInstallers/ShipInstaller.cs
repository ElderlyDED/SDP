using UnityEngine;
using Zenject;

public class ShipInstaller : MonoInstaller
{
    [SerializeField] GameObject _shipPrefab;
    public override void InstallBindings()
    {
        var shipPrefabInstance = Container.InstantiatePrefab(_shipPrefab
            ,new Vector3(0f, 0.3f, 0f), Quaternion.identity, transform.parent = null);
        Container.Bind<GameObject>().WithId("Ship").FromInstance(shipPrefabInstance);
    }
}