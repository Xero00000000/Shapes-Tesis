using UnityEngine;

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

    [SerializeField] int hpModifier;
    [SerializeField] int energy;
    [SerializeField] int physicalDefense;
    [SerializeField] int magicalDefense;
    [SerializeField] int speed;
    [SerializeField] int physicalAttack;
    [SerializeField] int magicalAttack;
    [SerializeField] int Dexterity;

    [SerializeReference] public GameObject classHead;
    [SerializeReference] public GameObject classTorso;
    [SerializeReference] public GameObject classRightArm;
    [SerializeReference] public GameObject classLeftArm;
    [SerializeReference] public GameObject classRightLeg;
    [SerializeReference] public GameObject classLeftLeg;

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
