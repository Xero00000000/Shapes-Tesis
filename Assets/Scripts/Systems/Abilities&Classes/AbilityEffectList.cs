using System;
using System.Collections;
using System.Collections.Generic;
using ImprovedTimers;
using UnityEngine;


class InstantDamageFactory : IEffectFactory<IDamageable, GameObject, Vector3>
{
    [SerializeField] float damageValue;

    public IAbilityEffect<IDamageable, GameObject, Vector3> Create()
    {
        return new InstantDamage { damageValue = damageValue };
    }
}

class DamageOverTimeFactory : IEffectFactory<IDamageable, GameObject, Vector3>
{
    [SerializeField] float duration;
    [SerializeField] float tickInterval;
    [SerializeField] float damagePerTick;

    public IAbilityEffect<IDamageable, GameObject, Vector3> Create()
    {
        return new DamageOverTimeEffect
        {
            duration = duration,
            tickInterval = tickInterval,
            damagePerTick = damagePerTick
        };
    }
}

struct InstantDamage : IAbilityEffect<IDamageable, GameObject, Vector3>
{
    public float damageValue;

    public event Action<IAbilityEffect<IDamageable, GameObject, Vector3>> OnCompleted;

    public void Apply(IDamageable target, GameObject caster, Vector3 point)
    {
        target.TakeDamage(damageValue);
        OnCompleted?.Invoke(this);
    }
    public void Cancel()
    {
        OnCompleted?.Invoke(this);
    }
}


struct DamageOverTimeEffect : IAbilityEffect<IDamageable, GameObject, Vector3>
{
    public float duration;
    public float tickInterval;
    public float damagePerTick;

    IntervalTimer timer;
    IDamageable currentTarget;

    public event Action<IAbilityEffect<IDamageable, GameObject, Vector3>> OnCompleted;

    public void Apply(IDamageable target, GameObject caster, Vector3 point)
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

struct Teleport : IAbilityEffect<IDamageable, GameObject, Vector3>
{

    public event Action<IAbilityEffect<IDamageable, GameObject, Vector3>> OnCompleted;

    public void Apply(IDamageable target, GameObject caster, Vector3 point)
    {
        caster.transform.position = point;
        OnCompleted?.Invoke(this);
    }
    public void Cancel()
    {
        OnCompleted?.Invoke(this);
    }
}
class TeleportFactory : IEffectFactory<IDamageable, GameObject, Vector3>
{
    public IAbilityEffect<IDamageable, GameObject, Vector3> Create()
    {
        return new Teleport {  };
    }
}

struct Run : IAbilityEffect<IDamageable, GameObject, Vector3>
{

    public event Action<IAbilityEffect<IDamageable, GameObject, Vector3>> OnCompleted;

    public void Apply(IDamageable target, GameObject caster, Vector3 point)
    {
        //caster.transform.position = point;
        OnCompleted?.Invoke(this);
    }
    public void Cancel()
    {
        OnCompleted?.Invoke(this);
    }
}
class RunFactory : IEffectFactory<IDamageable, GameObject, Vector3>
{
    public IAbilityEffect<IDamageable, GameObject, Vector3> Create()
    {
        return new Run { };
    }
}

struct Buff : IAbilityEffect<IDamageable, GameObject, Vector3>
{

    public event Action<IAbilityEffect<IDamageable, GameObject, Vector3>> OnCompleted;

    public void Apply(IDamageable target, GameObject caster, Vector3 point)
    {
        //caster.transform.position = point;
        OnCompleted?.Invoke(this);
    }
    public void Cancel()
    {
        OnCompleted?.Invoke(this);
    }
}
class BuffFactory : IEffectFactory<IDamageable, GameObject, Vector3>
{
    public IAbilityEffect<IDamageable, GameObject, Vector3> Create()
    {
        return new Run { };
    }
}

struct Dash : IAbilityEffect<IDamageable, GameObject, Vector3>
{

    public event Action<IAbilityEffect<IDamageable, GameObject, Vector3>> OnCompleted;

    public void Apply(IDamageable target, GameObject caster, Vector3 point)
    {
        //caster.transform.position = point;
        OnCompleted?.Invoke(this);
    }
    public void Cancel()
    {
        OnCompleted?.Invoke(this);
    }
}
class DashFactory : IEffectFactory<IDamageable, GameObject, Vector3>
{
    public IAbilityEffect<IDamageable, GameObject, Vector3> Create()
    {
        return new Run { };
    }
}
