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

    [SerializeReference] public List<IEffectFactory<IDamageable, GameObject, Vector3>> effects;

    [Header("Targeting")]
    [SerializeReference] TargetingStrategy targetingStrategy;

    public void Target(TargetingManager targetingManager, GameObject caster, Vector3 point)
    {
        if (targetingStrategy != null)
        {
            targetingStrategy.Start(this, targetingManager, caster, point);
        }
        Debug.Log($"anda 1");
    }

    void OnEnable()
    {
        if (string.IsNullOrEmpty(label)) label = name;
        if (effects == null) effects = new List<IEffectFactory<IDamageable, GameObject, Vector3>>();
    }

    public void Execute(IDamageable target, GameObject caster, Vector3 point)
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
                enemy.ApplyEffect(runtimeEffect);
            }
            else
            {
                runtimeEffect.Apply(target);
            }*/
        }
        Debug.Log($"anda 2");
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

        if (castSoundEffect != null)
        {
            AudioSource.PlayClipAtPoint(castSoundEffect, targetMb.transform.position);
        }
    }
}

class TestEffectFactory : IEffectFactory<IDamageable, GameObject, Vector3>
{
    //[SerializeField] private GameObject player;

    public IAbilityEffect<IDamageable, GameObject, Vector3> Create()
    {
        return new TestEffect { /*player = player*/ };
    }
}

class TestEffect : IAbilityEffect<IDamageable, GameObject, Vector3>
{
    //public GameObject player;

    public event Action<IAbilityEffect<IDamageable, GameObject, Vector3>> OnCompleted;

    public void Apply(IDamageable target, GameObject caster, Vector3 point)
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

class TestEffectOne : IAbilityEffect<IDamageable, GameObject, Vector3>
{
    [SerializeField] private GameObject player;

    public event Action<IAbilityEffect<IDamageable, GameObject, Vector3>> OnCompleted;

    public void Apply(IDamageable target, GameObject caster, Vector3 point)
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
