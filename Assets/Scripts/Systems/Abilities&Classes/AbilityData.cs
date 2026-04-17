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

    void OnEnable()
    {
        if (string.IsNullOrEmpty(label)) label = name;
        if (passiveAbility == null) passiveAbility = new AbilityData();
        if (headAbility == null) headAbility = new AbilityData();
        if (armsAbility == null) armsAbility = new AbilityData();
        if (torsoAbility == null) torsoAbility = new AbilityData();
        if (legsAbility == null) legsAbility = new AbilityData();
        if (primaryAttack == null) primaryAttack = new AbilityData();
        if (secondaryAttack == null) secondaryAttack = new AbilityData();
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
