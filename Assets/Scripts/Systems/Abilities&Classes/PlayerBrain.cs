using UnityEngine;
using UnityUtils;
using ImprovedTimers;

[RequireComponent(typeof(TargetingManager))]
class PlayerBrain : MonoBehaviour
{
    [SerializeField] TargetingManager targetingManager;
    [SerializeField] InputReader input;
    bool isUsingAbility;
    Vector2 moveInput;
    [SerializeField] float moveSpeed;
    [SerializeField] Transform playerModel;
    [SerializeField] Camera mainCamera;
    Vector3 mouseWorldPosition;
    public Vector3 GetMovementVelocity() => moveInput;
    [SerializeField] private LayerMask floorLayer;

    CountdownTimer castTimer;

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


    public void Start()
    {
        //temporal
        currentHead = Instantiate(head.classHead, headOffset.gameObject.transform);
        currentTorso = Instantiate(torso.classTorso, this.gameObject.transform);
        currentRightArm = Instantiate(arms.classRightArm, rightArmOffset.gameObject.transform);
        currentLeftArm = Instantiate(arms.classLeftArm, leftArmOffset.gameObject.transform);
        currentRightLeg = Instantiate(legs.classRightLeg, rightLegOffset.gameObject.transform);
        currentLeftLeg = Instantiate(legs.classLeftLeg, leftLegOffset.gameObject.transform);

        input.Move += direction => moveInput = direction;
        
        input.UtilityAbility += IsUtilityAbilityPressed =>
        {
            if (IsUtilityAbilityPressed)
            {
                Cast(head, 1);
            }
            else
            {

            }
        };
        input.DefensiveAbility += IsDefensiveAbilityPressed =>
        {
            if (IsDefensiveAbilityPressed)
            {
                Cast(torso, 2);
            }
            else
            {

            }
        };
        input.OfensiveAbility += IsOfensiveAbilityPressed =>
        {
            if (IsOfensiveAbilityPressed)
            {
                Cast(arms, 3);
            }
            else
            {

            }
        };
        input.MoveAbility += IsMoveAbilityPressed =>
        {
            if (IsMoveAbilityPressed)
            {
                Cast(legs, 4);
            }
            else
            {

            }
        };
        input.PrimaryAttack += IsPrimaryAttackPressed =>
        {
            if (IsPrimaryAttackPressed)
            {
                Cast(weapon, 5);
            }
            else
            {

            }
        };
        input.SecondaryAttack += IsSecondaryAttackPressed =>
        {
            if (IsSecondaryAttackPressed)
            {
                Cast(weapon, 6);
            }
            else
            {

            }
        };


        input.EnablePlayerActions();
    }

    public void Cast(ClassData classAbility, int partAbility)
    {
        switch (partAbility)
        {
            case 1:
                castTimer = new CountdownTimer(classAbility.headAbility.castTime);
                //castTimer.OnTimerStart = () => ?;
                castTimer.OnTimerStop = () => classAbility.headAbility.Target(targetingManager);
                break;
            case 2:
                castTimer = new CountdownTimer(classAbility.headAbility.castTime);
                //castTimer.OnTimerStart = () => ?;
                castTimer.OnTimerStop = () => classAbility.torsoAbility.Target(targetingManager);
                break;
            case 3:
                castTimer = new CountdownTimer(classAbility.headAbility.castTime);
                //castTimer.OnTimerStart = () => ?;
                castTimer.OnTimerStop = () => classAbility.armsAbility.Target(targetingManager);
                break;
            case 4:
                castTimer = new CountdownTimer(classAbility.headAbility.castTime);
                //castTimer.OnTimerStart = () => ?;
                castTimer.OnTimerStop = () => classAbility.legsAbility.Target(targetingManager);
                break;
            case 5:
                castTimer = new CountdownTimer(classAbility.headAbility.castTime);
                //castTimer.OnTimerStart = () => ?;
                castTimer.OnTimerStop = () => classAbility.primaryAttack.Target(targetingManager);
                break;
            case 6:
                castTimer = new CountdownTimer(classAbility.headAbility.castTime);
                //castTimer.OnTimerStart = () => ?;
                castTimer.OnTimerStop = () => classAbility.secondaryAttack.Target(targetingManager);
                break;
        }
    }

    //tambien temporal
    public void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, floorLayer))
        {
            mouseWorldPosition = raycastHit.point;

            Vector3 direction = mouseWorldPosition - transform.position;
            direction.y = 0f;

            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
            }

            targetingManager.mouseWorldPosition = mouseWorldPosition;
        }
        //playerModel.rotation = Quaternion.LookRotation(mouseWorldPosition);
        Move(CalculateMovementDirection());

        /*
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
        }*/
    }

    void Move(Vector3 direction)
    {
        if (direction.sqrMagnitude > 0.01f)
        {
            transform.position += direction * (Time.deltaTime * moveSpeed);
        }
    }

    Vector3 CalculateMovementDirection()
    {
        Vector3 cameraForward = new Vector3(mainCamera.transform.forward.x, 0, mainCamera.transform.forward.z).normalized;
        Vector3 cameraRight = new Vector3(mainCamera.transform.right.x, 0, mainCamera.transform.right.z).normalized;

        return cameraForward * moveInput.y + cameraRight * moveInput.x;
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

    public void UtilityAbility(IDamageable target)
    {
        head.headAbility.Execute(target);
    }
    public void DefenseAbility(IDamageable target)
    {
        torso.torsoAbility.Execute(target);
    }
    public void OffenseAbility(IDamageable target)
    {
        arms.armsAbility.Execute(target);
    }
    public void MovementAbility(IDamageable target)
    {
        legs.legsAbility.Execute(target);
    }
    public void PrimaryAttack(IDamageable target)
    {
        weapon.primaryAttack.Execute(target);
    }
    public void SecondaryAttack(IDamageable target)
    {
        weapon.secondaryAttack.Execute(target);
    }

}
