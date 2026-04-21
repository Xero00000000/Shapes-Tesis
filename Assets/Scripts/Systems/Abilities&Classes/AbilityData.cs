using UnityEngine;
using System;
using System.Collections.Generic;

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

[Serializable] abstract class AbilityEffect
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
