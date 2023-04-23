using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShipMove : MonoBehaviour
{
    [Inject] PlayerInput _playerInput;
    Rigidbody2D _rb2d;
    [SerializeField] Camera _mainCam;
    [SerializeField] float _speed;
    [SerializeField] Vector2 _moveVector;
    float _moveAngle;
    Vector2 _direction;

    void Start()
    {
        if(TryGetComponent(out Rigidbody2D rb))
            _rb2d = rb;
        _mainCam = Camera.main;
    }

    void OnEnable()
    {
        _playerInput.accelerationAction += Acceleration;
        _playerInput.decelerationAction += Deceleration;
    }
    void OnDisable()
    {
        _playerInput.accelerationAction -= Acceleration;
        _playerInput.decelerationAction -= Deceleration;
    }

    void Update()
    {
        RotateShip();
    }

    void Acceleration()
    {
        _rb2d.AddForce(_direction * _speed * Time.deltaTime);
    }

    void Deceleration()
    {
        _rb2d.velocity = _rb2d.velocity / 1.009f;
    }

    void RotateShip()
    {
        _direction = Input.mousePosition - _mainCam.WorldToScreenPoint(transform.position);
        _moveAngle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg + (-90f);
        transform.rotation = Quaternion.AngleAxis(_moveAngle, Vector3.forward);
    }

}
