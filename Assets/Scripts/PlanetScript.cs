using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlanetScript : MonoBehaviour
{
    [Inject] GameStatus _gameStatus;
    [field: SerializeField] public IntReactiveProperty PlanetHp { get; private set; } = new();
    [SerializeField] int _maxPlanetHp;
    CompositeDisposable _disposble = new();
    [SerializeField] Slider _hpSlider;
    [SerializeField] AudioSource _audioSource;

    void Start()
    {
        PlanetHp.Value = _maxPlanetHp;
        SetStatsWithUI();
        CheckPlanetHp();
    }

    void CheckPlanetHp() => PlanetHp.Where(hp => hp <= 0).Subscribe(v => {
        _gameStatus.EndGame();  
        _disposble.Clear();
        Destroy(gameObject);
    }).AddTo(_disposble);

    public void ApplyDamage(int damageCount)
    {
        PlanetHp.Value -= damageCount;
        _audioSource.Play();
    }
    void SetStatsWithUI()
    {
        _hpSlider.maxValue = _maxPlanetHp;
        PlanetHp.Subscribe(v => { _hpSlider.value = v; }).AddTo(_disposble);
    }
}
