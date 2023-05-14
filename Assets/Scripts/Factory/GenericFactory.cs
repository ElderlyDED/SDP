using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericFactory<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] T _prefab;

    [SerializeField] float _maxHorizontal;
    [SerializeField] float _minHorizontal;
    [SerializeField] int _spawnCount;
    [SerializeField] protected List<GameObject> _spawningObjects = new();
    [SerializeField] List<float> _verticalPos = new();

    public T GetNewInstance()
    {
        Vector2 pos = new Vector2 (Random.Range(_minHorizontal, _maxHorizontal),
            _verticalPos[Random.Range(0, _verticalPos.Count)]);
        _spawnCount++;
        var obj = Instantiate(_prefab, pos, Quaternion.identity);
        _spawningObjects.Add(obj.gameObject);
        return obj;
    }
}
