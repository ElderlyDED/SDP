using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class MetioritFactory : GenericFactory<Meteorite>
{
    [SerializeField] int _maxSpawningObjects;
    [SerializeField] int _maxSpawnObjectsAll;
    [Inject] GameStatus _gameStatus;

    void Start()
    { 
        SpawnObjects();
        PlusMaxSpawnObjects();
    }

    void SpawnObjects()
    {
        Observable.EveryUpdate().Subscribe(v => {
            _spawningObjects.RemoveAll(obj => obj == null);
            if (_spawningObjects.Count < _maxSpawningObjects)
                this.GetNewInstance();
        }).AddTo(this);
    }

    void PlusMaxSpawnObjects() => _gameStatus.Min.Subscribe(v => {
        if (_maxSpawningObjects < _maxSpawnObjectsAll)
            _maxSpawningObjects++;
    }).AddTo(this);



}
