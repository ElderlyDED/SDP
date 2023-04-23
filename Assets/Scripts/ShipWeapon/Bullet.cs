using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class Bullet : MonoBehaviour
{
    float _bulletSpeed;
    int _bulletDamage;
    float _bulletLifeTime;

    void Start()
    {
        BulletLifeTime();
    }

    void Update()
    {
        MovingForward();
    }

    void MovingForward()
    {
        transform.Translate(Vector2.up * Time.deltaTime * _bulletSpeed);
    }

    public void SetBulletStats(float speed, int damage, float lifeTime)
    {
        _bulletDamage = damage;
        _bulletSpeed = speed;
        _bulletLifeTime = lifeTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            damageable.ApplyDamage(_bulletDamage);
    }

    async UniTask BulletLifeTime()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_bulletLifeTime));
        Destroy(gameObject);
    }
}
