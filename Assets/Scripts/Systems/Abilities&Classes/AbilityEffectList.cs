using System;
using System.Collections;
using System.Collections.Generic;
using ImprovedTimers;
using UnityEngine;


class InstantDamage : AbilityEffect<IDamageable>
{
    [SerializeField] float damageValue;

    public override event Action<AbilityEffect<IDamageable>> OnCompleted;

    public override void Apply(IDamageable target)
    {
        target.TakeDamage(damageValue);
        OnCompleted?.Invoke(this);
    }
    public override void Cancel()
    {
        OnCompleted?.Invoke(this);
    }
}

[Serializable]
class DamageOverTimeEffect : AbilityEffect<IDamageable>
{
    public float duration;
    public float tickInterval;
    public int damagePerTick;

    IntervalTimer timer;
    IDamageable currentTarget;

    public override event Action<AbilityEffect<IDamageable>> OnCompleted;

    public override void Apply(IDamageable target)
    {
        currentTarget = target;
        timer = new IntervalTimer(duration, tickInterval);
        timer.OnInterval = OnInterval;
        timer.OnTimerStop = OnStop;
        timer.Start();
    }

    void OnInterval() => currentTarget?.TakeDamage(damagePerTick);
    void OnStop() => Cleanup();


    public override void Cancel()
    {
        timer?.Stop();
        Cleanup();
    }

    void Cleanup()
    {
        timer = null;
        currentTarget = null;
        OnCompleted?.Invoke(this);
    }
}
