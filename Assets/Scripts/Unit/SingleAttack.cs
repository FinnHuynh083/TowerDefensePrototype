using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SingleAttack : MonoBehaviour
{
    [SerializeField] protected Targetter _targetter;
    [SerializeField] protected float _damage;
    [SerializeField] protected UnityEvent OnAttack;
    [SerializeField] protected float _interval;

    private float _lastTimeShot = -1f;

    public virtual void TriggerVFX() => OnAttack.Invoke();

    public virtual void TriggerAttack()
    {
        if (_targetter.CurrentTarget == null)
        {
            return;
        }
        var target = _targetter.CurrentTarget;
        target.Health.TakeDamage(_damage);
        //tach sk attack va vfx ra 2 event
    }

    public virtual bool IsAttacking => _targetter.CurrentTarget != null;

    public bool AttackNow()
    {
        if (Time.time >= _lastTimeShot + _interval)
        {
            _lastTimeShot = Time.time;
            return true;
        }
        return false;
    }
    //lam interval cho tower, van de la atk spd anh huogn toi tg choi anim
    // tat loop them dk danh
    // du interval >> play anim
}
