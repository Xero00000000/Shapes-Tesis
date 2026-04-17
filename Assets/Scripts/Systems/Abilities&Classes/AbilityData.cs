using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ClassData", menuName = "ScriptableObjects/ClassData")]
class ClassData : ScriptableObject
{
    public string label;
    [SerializeReference] public AbilityData passiveAbility;
    [SerializeReference] public AbilityData headAbility;
    [SerializeReference] public AbilityData armsAbility;
    [SerializeReference] public AbilityData torsoAbility;
    [SerializeReference] public AbilityData legsAbility;
    [SerializeReference] public AbilityData primaryAttack;
    [SerializeReference] public AbilityData secondaryAttack;

    [SerializeField] int testStat;

    [SerializeField] GameObject testPart;

    void OnEnable()
    {
        if (string.IsNullOrEmpty(label)) label = name;
        if (passiveAbility == null) passiveAbility = (AbilityData)ScriptableObject.CreateInstance(typeof(AbilityData));
        if (headAbility == null) headAbility = (AbilityData)ScriptableObject.CreateInstance(typeof(AbilityData));
        if (armsAbility == null) armsAbility = (AbilityData)ScriptableObject.CreateInstance(typeof(AbilityData));
        if (torsoAbility == null) torsoAbility = (AbilityData)ScriptableObject.CreateInstance(typeof(AbilityData));
        if (legsAbility == null) legsAbility = (AbilityData)ScriptableObject.CreateInstance(typeof(AbilityData));
        if (primaryAttack == null) primaryAttack = (AbilityData)ScriptableObject.CreateInstance(typeof(AbilityData));
        if (secondaryAttack == null) secondaryAttack = (AbilityData)ScriptableObject.CreateInstance(typeof(AbilityData));
    }
}

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
