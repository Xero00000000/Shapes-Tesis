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

    /*
    internal int HpModifier => hpModifier;
    internal int Energy => energy;
    internal int PhysicalDefense => physicalDefense;
    internal int MagicalDefense => magicalDefense;
    internal int Speed => speed;
    internal int PhysicalAttack => physicalAttack;
    internal int MagicalAttack => magicalAttack;
    internal int DexterityValue => Dexterity;
    */

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

    /*
   public int TotalHP => hpModifier + Sum(p => p.hpModifier);
   public int TotalEnergy => energy + Sum(p => p.energy);
   public int TotalPhysicalDefense => physicalDefense + Sum(p => p.physicalDefense);
   public int TotalMagicalDefense => magicalDefense + Sum(p => p.magicalDefense);
   public int TotalSpeed => speed + Sum(p => p.speed);
   public int TotalPhysicalAttack => physicalAttack + Sum(p => p.physicalAttack);
   public int TotalMagicalAttack => magicalAttack + Sum(p => p.magicalAttack);
   public int TotalDexterity => Dexterity + Sum(p => p.dexterity);

   int Sum(System.Func<BodyPartData, int> selector)
   {
       int total = 0;

       BodyPartData[] parts = { head, torso, rightArm, leftArm, rightLeg, leftLeg };

       foreach (var part in parts)
       {
           if (part != null)
               total += selector(part);
       }

       return total;
   }
   */
}