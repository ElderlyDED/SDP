using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ShipStats : MonoBehaviour
{
    #region UniRx
    CompositeDisposable _disposable = new CompositeDisposable();
    public IntReactiveProperty _checkShipHp = new IntReactiveProperty();
    public IntReactiveProperty _checkShipShield = new IntReactiveProperty();
    #endregion

    [SerializeField] float _hpRegenRate;
    [SerializeField] float _ShieldRegenRate;

    [field: SerializeField] public int CountBlueDetails { get; private set; }
    [field: SerializeField] public int CountRedDetails { get; private set; }

    [field: SerializeField] public int HpLvl { get; private set; }
    [field: SerializeField] public int DamageLvl { get; private set; }

    void Start()
    {
        CheckShipHp();
    }

    void CheckShipHp() => _checkShipHp.Where(hp => hp <= 0).Subscribe(v => { DestroyShip(); }).AddTo(_disposable);

    void ApplyDamage(int damageCount)
    {
        if (_checkShipShield.Value > 0)
            _checkShipShield.Value -= damageCount / 2;
        else if (_checkShipShield.Value <= 0)
            _checkShipHp.Value -= damageCount;
    }

    void DestroyShip() => Debug.Log("ShipDie");

    public void ApplyBlueDetails(int countDetails) => CountBlueDetails += countDetails;
    public void ApplyRdDetails(int countDetails) => CountRedDetails += countDetails;
    public void SetHpLvl(int minusBlueDetails) 
    {
        CountBlueDetails -= minusBlueDetails;
        HpLvl++;
    }
    public void SetDamageLvl(int minusRedDetails)
    {
        CountRedDetails -= minusRedDetails;
        DamageLvl++;
    }
}
