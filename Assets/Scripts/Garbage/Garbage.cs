using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public abstract class Garbage : MonoBehaviour, IDamageable
{
    [SerializeField] protected int _hp;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] Transform _target;
    [SerializeField] int _destroyDamage;
    [SerializeField] int _planetDamage;
    #region Reactive Property
    protected CompositeDisposable _disposable = new CompositeDisposable();
    [SerializeField] protected IntReactiveProperty _checkHp = new IntReactiveProperty();
    #endregion
    #region Loot Property
    [SerializeField] List<GameObject> _details = new();
    [SerializeField] int _minCountDropDetails;
    [SerializeField] int _maxCountDropDetails;
    [SerializeField] float _detailForce;
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
            collision.TryGetComponent(out PlanetScript ps);
            ps.ApplyDamage(_planetDamage);
            DestroyThis();
        }
        if (collision.tag == "Ship")
        {
            collision.TryGetComponent(out ShipStats ss);
            ss.ApplyDamage(_destroyDamage);
            DestroyThis();
        }
    }

    public void ApplyDamage(int damageCount) => _checkHp.Value -= damageCount;

    protected virtual void DestroyThis()
    {
        DropLoot();
        _disposable.Clear();
        Destroy(gameObject);
    }

    void DropLoot()
    {
        int randCountDetails = Random.Range(_minCountDropDetails, _maxCountDropDetails);
        for (int i = 0; i < randCountDetails; i++)
        {
            GameObject randDetail = _details[Random.Range(0, _details.Count)];
            var detail = Instantiate(randDetail, transform.position, transform.rotation);
            detail.TryGetComponent(out Rigidbody2D rb2D);
            var direction = UnityEngine.Random.insideUnitCircle.normalized;
            rb2D.AddForce(direction * _detailForce);
        }
    }
}
