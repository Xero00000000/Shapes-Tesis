using System;
using System.Collections.Generic;
using ImprovedTimers;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float amount);
    void ApplyEffect(IAbilityEffect<IDamageable> effect);
}

public interface IAbilityEffect<TTarget>
{
    public abstract void Apply(TTarget target);
    public abstract void Cancel();
    public abstract event Action<IAbilityEffect<TTarget>> OnCompleted;
}

interface IEffectFactory<TTarget>
{
    IAbilityEffect<TTarget> Create();
}

