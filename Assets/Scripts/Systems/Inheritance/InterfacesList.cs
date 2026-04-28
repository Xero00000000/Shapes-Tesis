using System;
using System.Collections.Generic;
using ImprovedTimers;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float amount);
}

public interface IEffect<TTarget>
{
    void Apply(TTarget target);
    void Cancel();
}
