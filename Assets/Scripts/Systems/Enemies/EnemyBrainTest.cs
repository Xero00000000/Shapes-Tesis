using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

class EnemyBrainTest : MonoBehaviour, IDamageable
{
    [SerializeField] float health;
    readonly public List<IAbilityEffect<IDamageable>> activeEffects = new();

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    public void ApplyEffect(IAbilityEffect<IDamageable> effect)
    {
        effect.OnCompleted += RemoveEffect;
        activeEffects.Add(effect);
        effect.Apply(this);
    }

     public void RemoveEffect(IAbilityEffect<IDamageable> effect)
    {
        effect.OnCompleted -= RemoveEffect;
        activeEffects.Remove(effect);
    }

    void Die()
    {
        foreach (var effect in activeEffects)
        {
            effect.OnCompleted -= RemoveEffect;
            effect.Cancel();
        }
        activeEffects.Clear();

        Destroy(gameObject);
    }
}
