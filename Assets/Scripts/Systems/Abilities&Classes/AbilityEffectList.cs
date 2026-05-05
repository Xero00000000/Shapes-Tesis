using System;
using System.Collections;
using System.Collections.Generic;
using ImprovedTimers;
using UnityEngine;


class InstantDamageFactory : IEffectFactory<IDamageable>
{
    [SerializeField] float damageValue;

    public IAbilityEffect<IDamageable> Create()
    {
        return new InstantDamage { damageValue = damageValue };
    }
}

class DamageOverTimeFactory : IEffectFactory<IDamageable>
{
    [SerializeField] float duration;
    [SerializeField] float tickInterval;
    [SerializeField] float damagePerTick;

    public IAbilityEffect<IDamageable> Create()
    {
        return new DamageOverTimeEffect
        {
            duration = duration,
            tickInterval = tickInterval,
            damagePerTick = damagePerTick
        };
    }
}

struct InstantDamage : IAbilityEffect<IDamageable>
{
    public float damageValue;

    public event Action<IAbilityEffect<IDamageable>> OnCompleted;

    public void Apply(IDamageable target)
    {
        target.TakeDamage(damageValue);
        OnCompleted?.Invoke(this);
    }
    public void Cancel()
    {
        OnCompleted?.Invoke(this);
    }
}


struct DamageOverTimeEffect : IAbilityEffect<IDamageable>
{
    public float duration;
    public float tickInterval;
    public float damagePerTick;

    IntervalTimer timer;
    IDamageable currentTarget;

    public event Action<IAbilityEffect<IDamageable>> OnCompleted;

    public void Apply(IDamageable target)
    {
        currentTarget = target;
        timer = new IntervalTimer(duration, tickInterval);
        timer.OnInterval = OnInterval;
        timer.OnTimerStop = OnStop;
        timer.Start();
    }

    void OnInterval() => currentTarget?.TakeDamage(damagePerTick);
    void OnStop() => Cleanup();


    public void Cancel()
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
