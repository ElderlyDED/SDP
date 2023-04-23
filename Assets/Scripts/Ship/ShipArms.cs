using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShipArms : MonoBehaviour
{
    [SerializeField] List<GameObject> _weapons = new();
    [Inject] PlayerInput _playerInput;
    
    void Start()
    {
        
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
        shooting.Shoot();
            
    }
}
