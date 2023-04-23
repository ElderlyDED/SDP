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

}
