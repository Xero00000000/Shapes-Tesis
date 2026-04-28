using UnityEngine;

[CreateAssetMenu(fileName = "BodyPart", menuName = "ScriptableObjects/BodyPart")]
public class BodyPartData : ScriptableObject
{
    public GameObject prefab;

    [Header("Modifiers")]
    public int hpModifier;
    public int energy;
    public int physicalDefense;
    public int magicalDefense;
    public int speed;
    public int physicalAttack;
    public int magicalAttack;
    public int dexterity;
}