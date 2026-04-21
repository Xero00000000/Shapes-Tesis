using UnityEngine;

class PlayerBrain : MonoBehaviour
{
    //los stats van asi por ahora, despues vemos de como cambiamos en player brain
    [SerializeField] int hpModifier;
    [SerializeField] int energy;
    [SerializeField] int physicalDefense;
    [SerializeField] int magicalDefense;
    [SerializeField] int speed;
    [SerializeField] int physicalAttack;
    [SerializeField] int magicalAttack;
    [SerializeField] int Dexterity;

    [SerializeField] ClassData head;
    [SerializeField] ClassData torso;
    [SerializeField] ClassData arms;
    [SerializeField] ClassData legs;
    [SerializeField] ClassData weapon;

    //por ahora para que funcione lo de cambiar partes, pero luego vemos de hacerlo mejor
    [SerializeField] private GameObject headOffset;
    [SerializeField] private GameObject rightArmOffset;
    [SerializeField] private GameObject leftArmOffset;
    [SerializeField] private GameObject rightLegOffset;
    [SerializeField] private GameObject leftLegOffset;
    private GameObject currentHead;
    private GameObject currentTorso;
    private GameObject currentRightArm;
    private GameObject currentLeftArm;
    private GameObject currentRightLeg;
    private GameObject currentLeftLeg;
    //private GameObject currentWeapon;

    //temporal
    public void Start()
    {
        currentHead = Instantiate(head.classHead, headOffset.gameObject.transform);
        currentTorso = Instantiate(torso.classTorso, this.gameObject.transform);
        currentRightArm = Instantiate(arms.classRightArm, rightArmOffset.gameObject.transform);
        currentLeftArm = Instantiate(arms.classLeftArm, leftArmOffset.gameObject.transform);
        currentRightLeg = Instantiate(legs.classRightLeg, rightLegOffset.gameObject.transform);
        currentLeftLeg = Instantiate(legs.classLeftLeg, leftLegOffset.gameObject.transform);
    }

    public void Execute(ClassData classAbility, int partAbility)
    {
        switch (partAbility)
        {
            case 1:
                foreach (var effects in classAbility.headAbility.effects)
                    effects.Execute();
                break;
            case 2:
                foreach (var effects in classAbility.torsoAbility.effects)
                    effects.Execute();
                break;
            case 3:
                foreach (var effects in classAbility.armsAbility.effects)
                    effects.Execute();
                break;
            case 4:
                foreach (var effects in classAbility.legsAbility.effects)
                    effects.Execute();
                break;
            case 5:
                foreach (var effects in classAbility.primaryAttack.effects)
                    effects.Execute();
                break;
            case 6:
                foreach (var effects in classAbility.secondaryAttack.effects)
                    effects.Execute();
                break;
        }
    }

    //tambien temporal
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Execute(head, 1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Execute(torso, 2);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Execute(arms, 3);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Execute(head, 4);
        }

    }

    /*
    public void SwapPart(object sender, params object[] data)
    {
        if (data[0] is int && data[1] is ClassData && data[0] is >= 1 && data[0] is <= 5)
        {
            int newPart = (int) data[0];
            ClassData newClass = (ClassData) data[1];
            
            switch (newPart)
            {
                case 1:
                    head = newClass;
                    break;
                case 2:
                    torso = newClass;
                    break;
                case 3:
                    arms = newClass;
                    break;
                case 4:
                    legs = newClass;
                    break;
                case 5:
                    weapon = newClass;
                    break;
            }
        }
        var hopeItWorks = data[] as (int partValue, ClassData theClass)?;
        
        int newPart = hopeItWorks.Value.partValue;
        ClassData newClass = hopeItWorks.Value.theClass;

        switch (newPart)
        {
            case 1:
                head = newClass;
                break;
            case 2:
                torso = newClass;
                break;
            case 3:
                arms = newClass;
                break;
            case 4:
                legs = newClass;
                break;
            case 5:
                weapon = newClass;
                break;
        }
    }*/
    public void SwapPart(object dataOne, object dataTwo)
    {
        if (dataOne is int && dataTwo is ClassData && dataOne is >= 1 && dataOne is <= 5)
        {
            int newPart = (int)dataOne;
            ClassData newClass = (ClassData)dataTwo;

            switch (newPart)
            {
                case 1:
                    head = newClass;
                    if (currentHead != null)
                    {
                        Destroy(currentHead);
                    }
                    currentHead = Instantiate(newClass.classHead, headOffset.gameObject.transform);
                    break;
                case 2:
                    torso = newClass;
                    if (currentTorso != null)
                    {
                        Destroy(currentTorso);
                    }
                    currentTorso = Instantiate(newClass.classTorso, this.gameObject.transform);
                    break;
                case 3:
                    arms = newClass;
                    if (currentRightArm != null)
                    {
                        Destroy(currentRightArm);
                    }
                    if (currentLeftArm != null)
                    {
                        Destroy(currentLeftArm);
                    }
                    currentRightArm = Instantiate(newClass.classRightArm, rightArmOffset.gameObject.transform);
                    currentLeftArm = Instantiate(newClass.classLeftArm, leftArmOffset.gameObject.transform);
                    break;
                case 4:
                    legs = newClass;
                    if (currentRightLeg != null)
                    {
                        Destroy(currentRightLeg);
                    }
                    if (currentLeftLeg != null)
                    {
                        Destroy(currentLeftLeg);
                    }
                    currentRightLeg = Instantiate(newClass.classRightLeg, rightLegOffset.gameObject.transform);
                    currentLeftLeg = Instantiate(newClass.classLeftLeg, leftLegOffset.gameObject.transform);
                    break;
                /*case 5:
                    weapon = newClass;
                    if (currentHead != null)
                    {
                        Destroy(currentHead);
                    }
                    Instantiate(newClass.classHead, headOffset.gameObject.transform);
                    break;*/
            }
        }
    }

    public void UtilityAbility()
    {
        foreach (var effect in head.headAbility.effects)
        {
            effect.Execute();
        }
    }
    public void DefenseAbility()
    {
        foreach (var effect in torso.torsoAbility.effects)
        {
            effect.Execute();
        }
    }
    public void OffenseAbility()
    {
        foreach (var effect in arms.armsAbility.effects)
        {
            effect.Execute();
        }
    }
    public void MovementAbility()
    {
        foreach (var effect in legs.legsAbility.effects)
        {
            effect.Execute();
        }
    }
    public void PrimaryAttack()
    {
        foreach (var effect in weapon.primaryAttack.effects)
        {
            effect.Execute();
        }
    }
    public void SecondaryAttack()
    {
        foreach (var effect in weapon.secondaryAttack.effects)
        {
            effect.Execute();
        }
    }

}
