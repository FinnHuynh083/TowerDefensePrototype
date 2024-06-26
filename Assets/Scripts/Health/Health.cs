using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    private float _health;

    public UnityEvent OnHit;
    public UnityEvent OnDead;

    public bool IsDead => _health <= 0;

    private void Start()
    {
        _health = _maxHealth;
    }
    public void TakeDamage(float damage)
    {
        _health -= damage;
        OnHit.Invoke();
        if (IsDead)
        {
            OnDead.Invoke();
            _health = 0;
            print("IsDead");
        }
        print($"Current Health: {_health}");
    }
}
