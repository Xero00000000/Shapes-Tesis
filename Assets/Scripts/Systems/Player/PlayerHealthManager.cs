using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerHealthManager : MonoBehaviour, IDamageable
{
    [SerializeField] float health;
    readonly public List<IAbilityEffect<IDamageable, GameObject, Vector3>> activeEffects = new();

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    public void ApplyEffect(IAbilityEffect<IDamageable, GameObject, Vector3> effect)
    {
        effect.OnCompleted += RemoveEffect;
        activeEffects.Add(effect);
        effect.Apply(this, this.gameObject, Vector3.zero); //despues modificar esto para que mantenga las referencias de caster y eso
    }

    public void RemoveEffect(IAbilityEffect<IDamageable, GameObject, Vector3> effect)
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
