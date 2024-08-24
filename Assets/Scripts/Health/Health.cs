using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;


    private float _health;

    public UnityEvent OnHit;
    public UnityEvent OnDead;

    public float CurrentHealth => _health;
    public float MaxHealth => _maxHealth;

    public bool IsDead => _health <= 0;

    private void Awake()
    {
        _health = _maxHealth;
    }
    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (IsDead)
        {
            OnDead.Invoke();
            _health = 0;
            print("IsDead");
        }
        OnHit.Invoke();
        //print($"Current Health: {_health}");
    }
}
