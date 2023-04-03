using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public delegate void AccelerationAction();
    public event AccelerationAction accelerationAction;
    public delegate void DecelerationAction();
    public event DecelerationAction decelerationAction;

    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            accelerationAction?.Invoke();
        
    }
}
