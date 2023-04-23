using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Meteorite : Garbage
{
    [SerializeField] int _pieceCount;
    [SerializeField] GameObject _piecePrefab;
    [SerializeField] float _pieceForce;

    protected override void CheckHealth()
    {
        _checkHp.Where(h => h <= 0).Subscribe(value => {
            SpawnPiece();
            Destroy(gameObject);
        }).AddTo(_disposable);
    }

    void SpawnPiece()
    {
        for (int i = 0; i < _pieceCount; i++)
        {
            var piece = Instantiate(_piecePrefab, transform.position, transform.rotation);
            piece.TryGetComponent(out Rigidbody2D rb2D);
            var direction = UnityEngine.Random.insideUnitCircle.normalized;
            rb2D.AddForce(direction * _pieceForce);
        }
    }

}
