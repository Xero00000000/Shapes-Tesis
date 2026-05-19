using System;
using System.Collections.Generic;
using ImprovedTimers;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float amount);
    void ApplyEffect(IAbilityEffect<IDamageable, GameObject, Vector3> effect);
}

public interface IAbilityEffect<TTarget, TCaster, TPoint>
{
    public abstract void Apply(TTarget target, TCaster caster, TPoint point);
    public abstract void Cancel();
    public abstract event Action<IAbilityEffect<TTarget, TCaster, TPoint>> OnCompleted;
}

interface IEffectFactory<TTarget, TCaster, TPoint>
{
    IAbilityEffect<TTarget, TCaster, TPoint> Create();
}

