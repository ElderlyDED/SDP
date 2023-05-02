using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ShipStats : MonoBehaviour
{
    #region UniRx
    CompositeDisposable _disposable = new CompositeDisposable();
    [field: SerializeField] public IntReactiveProperty CheckShipHp { get; private set; } = new();
    [field: SerializeField] public IntReactiveProperty CheckShipShield { get; private set; } = new();
    [field: SerializeField] public IntReactiveProperty MaxHp { get; private set; } = new();
    [field: SerializeField] public IntReactiveProperty MaxShield { get; private set; } = new();
    [field: SerializeField] public IntReactiveProperty CountBlueDetails { get; private set; } = new();
    [field: SerializeField] public IntReactiveProperty CountRedDetails { get; private set; } = new();
    #endregion

    [SerializeField] float _hpRegenRate;
    [SerializeField] float _ShieldRegenRate;
    [SerializeField] bool _takeDamage;

    [field: SerializeField] public int HpLvl { get; private set; }
    [field: SerializeField] public int DamageLvl { get; private set; }

    void Start()
    {
        CheckHp();
        CheckHpToRegen();
    }

    void CheckHp() => CheckShipHp.Where(hp => hp <= 0).Subscribe(v => { DestroyShip(); }).AddTo(_disposable);

    void ApplyDamage(int damageCount)
    {
        if (CheckShipShield.Value > 0)
            CheckShipShield.Value -= damageCount / 2;
        else if (CheckShipShield.Value <= 0)
            CheckShipHp.Value -= damageCount;
    }

    void DestroyShip() => Debug.Log("ShipDie");

    public void ApplyBlueDetails(int countDetails) => CountBlueDetails.Value += countDetails;
    public void ApplyRdDetails(int countDetails) => CountRedDetails.Value += countDetails;

    public void SetHpLvl(int minusBlueDetails) 
    {
        CountBlueDetails.Value -= minusBlueDetails;
        HpLvl++;
    }

    public void SetDamageLvl(int minusRedDetails)
    {
        CountRedDetails.Value -= minusRedDetails;
        DamageLvl++;
    }

    void CheckHpToRegen()
    {
        Observable.EveryFixedUpdate().Subscribe(v => {
            if (!_takeDamage)
                if (CheckShipShield.Value < MaxShield.Value)
                    CheckShipShield.Value += 1;
        }).AddTo(_disposable);
    }
}
