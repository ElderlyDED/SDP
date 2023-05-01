using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Details : MonoBehaviour
{
    [SerializeField] protected int _countDetails;
    [SerializeField] Collider2D _trigger;
    void Start()
    {
        CheckGetDetails();   
    }

    void CheckGetDetails()
    {
        _trigger.OnTriggerEnter2DAsObservable()
            .Where(t => t.TryGetComponent(out ShipStats s))
                .Subscribe(other => {
                    if(other.TryGetComponent(out ShipStats shipStats))
                        GetDetail(shipStats);
                }).AddTo(this);
    }

    protected virtual void GetDetail(ShipStats shipStats) => DestroyDetail();

    void DestroyDetail() => Destroy(gameObject);
}
