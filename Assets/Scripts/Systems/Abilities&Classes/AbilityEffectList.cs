using System;
using System.Collections;
using System.Collections.Generic;
using ImprovedTimers;
using UnityEngine;

[Serializable]
public class DamageEffect : IEffect<IDamageable>
{
    public int damageAmmount;

    public void Apply(IDamageable target)
    {
        target.TakeDamage(damageAmmount);
    }

    public void Cancel()
    {

    }
}

class InstantDamage : AbilityEffect
{
    [SerializeField] float damageValue;

    public event Action<AbilityEffect> OnCompleted;

    public override void Apply()
    {
        OnCompleted?.Invoke(this);
    }
    public override void Cancel()
    {
        OnCompleted?.Invoke(this);
    }
}

[Serializable]
public class DamageOverTimeEffect : IEffect<IDamageable>
{
    public float duration;
    public float tickInterval;
    public int damagePerTick;

    IntervalTimer timer;
    IDamageable currentTarget;

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
        //OnCompleted?.Invoke(this);
    }
}
