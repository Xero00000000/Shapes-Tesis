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

[Serializable] abstract class AbilityEffect //: MonoBehaviour
{
    public abstract void Execute();
}

class Damage : AbilityEffect
{
    [SerializeField] float damageValue;

    public override void Execute()
    {

    }

}

class TestEffect : AbilityEffect
{
    [SerializeField] private GameObject player;
    public override void Execute()
    {
        GameObject spawnPlace = GameObject.Find("bullshitspawn");
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(spawnPlace.transform.position.x, spawnPlace.transform.position.y, spawnPlace.transform.position.z);

        UnityEngine.Object.Destroy(cube, 5.0f);
    }

}

class TestEffectOne : AbilityEffect
{
    [SerializeField] private GameObject player;
    public override void Execute()
    {
        GameObject spawnPlace = GameObject.Find("bullshitspawn");
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        cube.transform.position = new Vector3(spawnPlace.transform.position.x, spawnPlace.transform.position.y, spawnPlace.transform.position.z);

        UnityEngine.Object.Destroy(cube, 5.0f);
    }

}

class TestEffectTwo : AbilityEffect
{
    [SerializeField] float damageValue;

    public override void Execute()
    {

    }

}
