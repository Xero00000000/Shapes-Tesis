using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using ImprovedTimers;

[CreateAssetMenu(fileName = "AbilityData", menuName = "ScriptableObjects/AbilityData")]
class AbilityData : ScriptableObject
{
    public string label;

    public float castTime;
    [SerializeField] AudioClip castSoundEffect;
    [SerializeField] GameObject castVisualEffect;
    [SerializeField] GameObject runningVisualEffect;

    //[Header("Effects")]
    [SerializeReference] public List<IEffectFactory<IDamageable>> effects;

    [Header("Targeting")]
    [SerializeReference] TargetingStrategy targetingStrategy;

    public void Target(TargetingManager targetingManager)
    {
        if (targetingStrategy != null)
        {
            targetingStrategy.Start(this, targetingManager);
        }
    }

    void OnEnable()
    {
        if (string.IsNullOrEmpty(label)) label = name;
        if (effects == null) effects = new List<IEffectFactory<IDamageable>>();
    }

    public void Execute(IDamageable target)
    {
        HandleVFX(target);
        HandleSFX(target);

        foreach (var effect in effects)
        {
            var runtimeEffect = effect.Create();
            target.ApplyEffect(runtimeEffect);
            /*
            if (target is EnemyBrainTest enemy)
            {
                enemy.ApplyEffect(effect);
            }
            else
            {
                effect.Apply(target);
            }*/
        }
    }

    void HandleVFX(IDamageable target)
    {
        var targetMb = target as MonoBehaviour;
        if (targetMb == null) return;

        if (castVisualEffect != null)
        {
            Instantiate(castVisualEffect, targetMb.transform.position, Quaternion.identity);
        }

        if (runningVisualEffect != null)
        {
            Instantiate(runningVisualEffect, targetMb.transform);
        }
    }

    void HandleSFX (IDamageable target)
    {
        var targetMb = target as MonoBehaviour;
        if (targetMb == null) return;
        
        AudioSource.PlayClipAtPoint(castSoundEffect, targetMb.transform.position);
    }
}

class TestEffect : IAbilityEffect<IDamageable>
{
    [SerializeField] private GameObject player;

    public event Action<IAbilityEffect<IDamageable>> OnCompleted;

    public void Apply(IDamageable target)
    {
        GameObject spawnPlace = GameObject.Find("bullshitspawn");
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(spawnPlace.transform.position.x, spawnPlace.transform.position.y, spawnPlace.transform.position.z);

        UnityEngine.Object.Destroy(cube, 5.0f);
    }

    public void Cancel()
    {
        throw new NotImplementedException();
    }
}

class TestEffectOne : IAbilityEffect<IDamageable>
{
    [SerializeField] private GameObject player;

    public event Action<IAbilityEffect<IDamageable>> OnCompleted;

    public void Apply(IDamageable target)
    {
        GameObject spawnPlace = GameObject.Find("bullshitspawn");
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        cube.transform.position = new Vector3(spawnPlace.transform.position.x, spawnPlace.transform.position.y, spawnPlace.transform.position.z);

        UnityEngine.Object.Destroy(cube, 5.0f);
    }

    public void Cancel()
    {
        throw new NotImplementedException();
    }
}
