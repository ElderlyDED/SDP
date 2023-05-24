using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

public class UpgradeMenu : MonoBehaviour
{
    [Inject] GameStatus _gameStatus;
    [Inject] PlayerInput _playerInput;
    [Inject(Id = "Ship")] GameObject _ship;

    ShipStats _shipStats;

    [SerializeField] Animator _menuAnimator;
    [SerializeField] bool _activMenu;
    [SerializeField] int _blueDetails;
    [SerializeField] int _redDetails;

    [SerializeField] AudioSource _audioSource;

    #region UpCostShip
    [SerializeField] int _upRateShipCost;
    [SerializeField] int _nowCostHpShip;
    [SerializeField] int _nowCostDamageShip;
    #endregion

    [SerializeField] TextMeshProUGUI _textCostDamage;
    [SerializeField] TextMeshProUGUI _textCostHp;

    void Start()
    {
        _ship.TryGetComponent(out ShipStats ss);
        _shipStats = ss;
        SetCostInUI();
    }

    void OnEnable() => _playerInput.upgradeAction += StartUpgradeMenu;

    void OnDisable() => _playerInput.upgradeAction -= StartUpgradeMenu;

    void StartUpgradeMenu()
    {

        if (!_activMenu)
        {
            _gameStatus.SetGamePause();
            _activMenu = true;
            _menuAnimator.SetBool("IsUp", true);
            _menuAnimator.SetBool("IsDown", false);
        }
        else
        {
            _gameStatus.SetGamePause();
            _activMenu = false;
            _menuAnimator.SetBool("IsDown", true);
            _menuAnimator.SetBool("IsUp", false);
        }
    } 

    #region BtnScripts
    public void UpShipHp()
    {
        if (_shipStats.CountBlueDetails.Value >= _nowCostHpShip)
        {
            _shipStats.SetHpLvl(_nowCostHpShip);
            _nowCostHpShip = _nowCostHpShip * _upRateShipCost; 
            _audioSource.Play();
        }
    }

    public void UpShipDamage()
    {
        if (_shipStats.CountRedDetails.Value >= _nowCostDamageShip)
        {
            _shipStats.SetDamageLvl(_nowCostDamageShip);
            _nowCostDamageShip = _nowCostDamageShip * _upRateShipCost;
            _audioSource.Play();
        }
    }
    #endregion

    void SetCostInUI()
    {
        Observable.EveryUpdate().Subscribe(v => {
            _textCostDamage.text = _nowCostDamageShip.ToString();
            _textCostHp.text = _nowCostHpShip.ToString();
        }).AddTo(this);
    }
}
