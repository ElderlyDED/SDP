using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PlanetScript : MonoBehaviour
{
    [field: SerializeField] public IntReactiveProperty PlanetHp { get; private set; } = new();
    [SerializeField] int _maxPlanetHp;
    CompositeDisposable _disposble = new();
    [SerializeField] Slider _hpSlider;

    void Start()
    {
        PlanetHp.Value = _maxPlanetHp;
        SetStatsWithUI();
        CheckPlanetHp();
    }

    void CheckPlanetHp() => PlanetHp.Where(hp => hp <= 0).Subscribe(v => {
        _disposble.Clear();
        Destroy(gameObject);
    }).AddTo(_disposble);

    public void ApplyDamage(int damageCount) => PlanetHp.Value -= damageCount;

    void SetStatsWithUI()
    {
        _hpSlider.maxValue = _maxPlanetHp;
        PlanetHp.Subscribe(v => { _hpSlider.value = v; }).AddTo(_disposble);
    }
}
