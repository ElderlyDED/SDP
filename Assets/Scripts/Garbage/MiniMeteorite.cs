using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class MiniMeteorite : Garbage
{
    protected override void CheckHealth()
    {
        _checkHp.Where(h => h <= 0).Subscribe(value => {
            DestroyThis();
        }).AddTo(_disposable);
    }
}
