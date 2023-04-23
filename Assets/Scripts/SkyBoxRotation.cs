using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class SkyBoxRotation : MonoBehaviour
{
    [SerializeField] Skybox _skyBox;
    [SerializeField] float _rotateRate;

    void Start()
    {
        Observable.EveryFixedUpdate().Subscribe(v => {
            _skyBox.material.SetFloat("_Rotation", Time.time * _rotateRate);
        }).AddTo(this);
    }
}
