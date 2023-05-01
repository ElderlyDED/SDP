using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShipArms : MonoBehaviour
{
    [SerializeField] List<GameObject> _weapons = new();
    [Inject] PlayerInput _playerInput;
    ShipStats _shipStats;
    
    void Start()
    {
        TryGetComponent(out ShipStats ss);
        _shipStats = ss;
    }

    void OnEnable()
    {
        _playerInput.weaponSlotAction += ActivSlots;
    }

    void OnDisable()
    {
        _playerInput.weaponSlotAction -= ActivSlots;
    }

    void ActivSlots(int slotId)
    {
        _weapons[slotId].TryGetComponent(out IShooting shooting);
        shooting.Shoot(_shipStats.DamageLvl);
            
    }
}
