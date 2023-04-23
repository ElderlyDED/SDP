using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public abstract class Garbage : MonoBehaviour, IDamageable
{
    [SerializeField] protected int _hp;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] Transform _target;
    #region Reactive Property
    protected CompositeDisposable _disposable = new CompositeDisposable();
    [SerializeField] protected IntReactiveProperty _checkHp = new IntReactiveProperty();
    #endregion

    void Start()
    {
        _checkHp.Value = _hp;
        _target = GameObject.FindGameObjectWithTag("Planet").transform;
        
        CheckHealth();
        Observable.EveryFixedUpdate().Subscribe(v => {
            if (_checkHp.Value > 0)
                    MoveToPlanet();
            else
                _disposable.Clear();
        }).AddTo(_disposable);
    }

    protected virtual void CheckHealth()
    {
        _checkHp.Where(h => h <= 0).Subscribe(value => {
            DestroyThis();
        }).AddTo(_disposable);
    }

    void MoveToPlanet()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target.position, _moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Planet")
        {
            DestroyThis();
        }
    }

    public void ApplyDamage(int damageCount) => _checkHp.Value -= damageCount;

    void DestroyThis()
    {
        _disposable.Clear();
        Destroy(gameObject);
    }
}
