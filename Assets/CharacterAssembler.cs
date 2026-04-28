using UnityEngine;

public class CharacterAssembler : MonoBehaviour
{
    [SerializeField] private ClassData classData;

    [Header("Anchors")]
    [SerializeField] private Transform headAnchor;
    [SerializeField] private Transform torsoAnchor;
    [SerializeField] private Transform rightArmAnchor;
    [SerializeField] private Transform leftArmAnchor;
    [SerializeField] private Transform rightLegAnchor;
    [SerializeField] private Transform leftLegAnchor;

    private GameObject currentHead, currentTorso;
    private GameObject currentRightArm, currentLeftArm;
    private GameObject currentRightLeg, currentLeftLeg;

    void Start()
    {
        Rebuild();
    }

    public void Rebuild()
    {
        Clear();

        SpawnPart(classData.head, headAnchor, ref currentHead);
        SpawnPart(classData.torso, torsoAnchor, ref currentTorso);
        SpawnPart(classData.rightArm, rightArmAnchor, ref currentRightArm);
        SpawnPart(classData.leftArm, leftArmAnchor, ref currentLeftArm);
        SpawnPart(classData.rightLeg, rightLegAnchor, ref currentRightLeg);
        SpawnPart(classData.leftLeg, leftLegAnchor, ref currentLeftLeg);

        DebugStats();
    }

    void SpawnPart(BodyPartData part, Transform anchor, ref GameObject current)
    {
        if (part == null || part.prefab == null || anchor == null) return;

        current = Instantiate(part.prefab, anchor);
        current.transform.localPosition = Vector3.zero;
        current.transform.localRotation = Quaternion.identity;
    }

    void Clear()
    {
        DestroyIfExists(currentHead);
        DestroyIfExists(currentTorso);
        DestroyIfExists(currentRightArm);
        DestroyIfExists(currentLeftArm);
        DestroyIfExists(currentRightLeg);
        DestroyIfExists(currentLeftLeg);
    }
    public void ChangeHead(BodyPartData newHead)
    {
        classData.head = newHead;
        Rebuild();
    }
    void DestroyIfExists(GameObject obj)
    {
        if (obj != null) Destroy(obj);
    }

    void DebugStats()
    {
        Debug.Log($"HP: {classData.TotalHP}");
        Debug.Log($"ATK: {classData.TotalPhysicalAttack}");
        Debug.Log($"DEF: {classData.TotalPhysicalDefense}");
    }
}