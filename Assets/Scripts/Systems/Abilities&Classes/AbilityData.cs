using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CreateAssetMenu(fileName = "AbilityData", menuName = "ScriptableObjects/AbilityData")]
class AbilityData : ScriptableObject
{
    public string label;
    [SerializeReference] public List<AbilityEffect> effects;

    void OnEnable()
    {
        if (string.IsNullOrEmpty(label)) label = name;
        if (effects == null) effects = new List<AbilityEffect>();
    }
}

[Serializable] abstract class AbilityEffect//<TTarget> //: MonoBehaviour
{
    public abstract void Apply();
    public abstract void Cancel();
    event Action<AbilityEffect/*<TTarget>*/> OnCompleted;
}

class TestEffect : AbilityEffect
{
    [SerializeField] private GameObject player;
    public override void Apply()
    {
        GameObject spawnPlace = GameObject.Find("bullshitspawn");
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(spawnPlace.transform.position.x, spawnPlace.transform.position.y, spawnPlace.transform.position.z);

        UnityEngine.Object.Destroy(cube, 5.0f);
    }

    public override void Cancel()
    {
        throw new NotImplementedException();
    }
}

class TestEffectOne : AbilityEffect
{
    [SerializeField] private GameObject player;
    public override void Apply()
    {
        GameObject spawnPlace = GameObject.Find("bullshitspawn");
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        cube.transform.position = new Vector3(spawnPlace.transform.position.x, spawnPlace.transform.position.y, spawnPlace.transform.position.z);

        UnityEngine.Object.Destroy(cube, 5.0f);
    }

    public override void Cancel()
    {
        throw new NotImplementedException();
    }
}

class TestEffectTwo : AbilityEffect
{
    [SerializeField] float damageValue;

    public override void Apply()
    {

    }

    public override void Cancel()
    {
        throw new NotImplementedException();
    }
}
