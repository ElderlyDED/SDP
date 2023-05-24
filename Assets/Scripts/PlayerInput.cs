using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public delegate void AccelerationAction();
    public event AccelerationAction accelerationAction;
    public delegate void DecelerationAction();
    public event DecelerationAction decelerationAction;
    public delegate void WeaponSlotAction(int slotId);
    public event WeaponSlotAction weaponSlotAction;
    public delegate void PauseAction();
    public event PauseAction pauseAction;
    public delegate void UpgradeAction();
    public event UpgradeAction upgradeAction;

    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            accelerationAction?.Invoke();
        if (Input.GetKey(KeyCode.S))
            decelerationAction?.Invoke();
        if (Input.GetKeyDown(KeyCode.Mouse0))
            weaponSlotAction?.Invoke(0);
        if (Input.GetKeyDown(KeyCode.Escape)) 
            pauseAction?.Invoke();
        if(Input.GetKeyDown(KeyCode.I))
            upgradeAction?.Invoke();
    }
}
