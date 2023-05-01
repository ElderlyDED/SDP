using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UpgradeMenu : MonoBehaviour
{
    [Inject] GameStatus _gameStatus;
    [Inject] PlayerInput _playerInput;
    [Inject] GameObject _ship;
    ShipStats _shipStats;

    [SerializeField] Animator _menuAnimator;
    [SerializeField] bool _activMenu;
    [SerializeField] int _blueDetails;
    [SerializeField] int _redDetails;

    #region UpCostShip
    [SerializeField] int _upRateShipCost;
    [SerializeField] int _nowCostHpShip;
    [SerializeField] int _nowCostDamageShip;
    #endregion

    void Start()
    {
        _ship.TryGetComponent(out ShipStats ss);
        _shipStats = ss;
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
        if (_shipStats.CountBlueDetails >= _nowCostHpShip)
        {
            _shipStats.SetHpLvl(_nowCostHpShip);
            _nowCostHpShip = _nowCostHpShip * _upRateShipCost; 
        }
    }

    public void UpShipDamage()
    {
        if (_shipStats.CountRedDetails >= _nowCostDamageShip)
        {
            _shipStats.SetDamageLvl(_nowCostDamageShip);
            _nowCostDamageShip = _nowCostDamageShip * _upRateShipCost;
        }
    }
    #endregion

}
