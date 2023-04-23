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
    

    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            accelerationAction?.Invoke();
        if (Input.GetKey(KeyCode.S))
            decelerationAction?.Invoke();
        if (Input.GetKeyDown(KeyCode.Z))
            weaponSlotAction?.Invoke(0);
        if (Input.GetKeyDown(KeyCode.X))
            weaponSlotAction?.Invoke(1);
        if (Input.GetKeyDown(KeyCode.C))
            weaponSlotAction?.Invoke(2);
    }
}
