using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MetioritFactory : GenericFactory<Meteorite>
{
    [SerializeField] int maxSpawningObjects;
    [ContextMenu("suka")]
    void Start()
    { 
        Observable.EveryUpdate().Subscribe(v => {
            _spawningObjects.RemoveAll(obj => obj == null);
            if (_spawningObjects.Count < maxSpawningObjects)
                this.GetNewInstance();
        }).AddTo(this); 
    }
}
