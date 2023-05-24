using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.Asteroids;

public class MachineGun : MonoBehaviour, IShooting
{
    [SerializeField] AudioSource _shootSound;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _shootPointFirstGun;
    [SerializeField] Transform _shootPointSecondGun;
    [SerializeField] float _bulletSpeed;
    [SerializeField] int _bulletDamage;
    [SerializeField] float _bulletLifeTime;
    [SerializeField] int _upDamage;
    public void Shoot(int damageLvl)
    {
        int damage = _bulletDamage + (_upDamage * damageLvl);
        var firstBullet = Instantiate(_bulletPrefab, _shootPointFirstGun.position, transform.rotation);
        if(firstBullet.TryGetComponent(out Bullet fBulletScript))
            fBulletScript.SetBulletStats(_bulletSpeed, damage, _bulletLifeTime);
        var secondBullet = Instantiate(_bulletPrefab, _shootPointSecondGun.position, transform.rotation);
        if (secondBullet.TryGetComponent(out Bullet sBulletScript))
            sBulletScript.SetBulletStats(_bulletSpeed, damage, _bulletLifeTime);
        _shootSound.Play();
    }
} 
