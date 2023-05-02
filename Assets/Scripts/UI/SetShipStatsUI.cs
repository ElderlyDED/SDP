using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SetShipStatsUI : MonoBehaviour
{
    CompositeDisposable _disposable = new CompositeDisposable();

    [Inject] GameObject _ship;
    ShipStats _shipStats;

    #region UiElement
    [SerializeField] TextMeshProUGUI _redDetailsText;
    [SerializeField] TextMeshProUGUI _blueDetailsText;
    [SerializeField] Slider _healthSlider;
    [SerializeField] Slider _shieldSlider;
    #endregion

    void Start()
    {
        _ship.TryGetComponent(out ShipStats ss);
        _shipStats = ss;
        SetDetailsValue();
        SetSlidersValue();
    }

    void SetDetailsValue()
    {
        _shipStats.CountBlueDetails.Subscribe(v => { _blueDetailsText.text = v.ToString(); }).AddTo(_disposable);
        _shipStats.CountRedDetails.Subscribe(v => { _redDetailsText.text = v.ToString(); }).AddTo(_disposable);
    }

    void SetSlidersValue()
    {
        _shipStats.MaxHp.Subscribe(v => { _healthSlider.maxValue = v; }).AddTo(_disposable);
        _shipStats.CheckShipHp.Subscribe(v => { _healthSlider.value = v; }).AddTo(_disposable);
        _shipStats.MaxShield.Subscribe(v => { _shieldSlider.maxValue = v; }).AddTo(_disposable);
        _shipStats.CheckShipShield.Subscribe(v => { _shieldSlider.value = v; }).AddTo(_disposable);
    }
}
