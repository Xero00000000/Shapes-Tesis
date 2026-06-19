using ImprovedTimers;
using Unity.VisualScripting;

//using UnityEditor.Playables;
using UnityEngine;
using UnityUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[RequireComponent(typeof(TargetingManager))]
class ArtsyPlayerBrain : MonoBehaviour
{
    [SerializeField] TargetingManager targetingManager;
    [SerializeField] InputReader input;
    bool isUsingAbility = false;
    Vector2 moveInput;
    [SerializeField] float moveSpeed;
    [SerializeField] Transform playerModel;
    [SerializeField] Camera mainCamera;
    CameraFollow cameraScript;
    Vector3 mouseWorldPosition;
    public Vector3 GetMovementVelocity() => moveInput;
    [SerializeField] private LayerMask floorLayer;

    CountdownTimer castTimer;

    private Mannequin mannequin;

    public int currentHead;
    public int currentTorso;
    public int currentArms;
    public int currentLegs;

    public List<GameObject> headParts;
    public List<GameObject> torsoParts;
    public List<GameObject> armsParts;
    public List<GameObject> legsParts;

    [Header("CurrentPartAbilities")]
    [SerializeField] ClassData head;
    [SerializeField] ClassData torso;
    [SerializeField] ClassData arms;
    [SerializeField] ClassData legs;
    //[SerializeField] ClassData weapon;

    [Header("AllAbilities")]
    [SerializeField] List<ClassData> ClassList;

    public bool combatMode = false;



    public void Start()
    {
        for (int i = 0; i < headParts.Count; i++)
        {
            headParts[i].SetActive(i == currentHead);
        }
        for (int i = 0; i < torsoParts.Count; i++)
        {
            torsoParts[i].SetActive(i == currentTorso);
        }
        for (int i = 0; i < armsParts.Count; i++)
        {
            armsParts[i].SetActive(i == currentArms);
        }
        for (int i = 0; i < legsParts.Count; i++)
        {
            legsParts[i].SetActive(i == currentLegs);
        }
        head = ClassList[currentHead];
        torso = ClassList[currentTorso];
        arms = ClassList[currentArms];
        legs = ClassList[currentLegs];

        input.Move += direction => moveInput = direction;

        input.UtilityAbility += IsUtilityAbilityPressed =>
        {
            if (combatMode != true)
            {
                if (mannequin != null)
                {
                    SwapPart(1, mannequin.currentHead);
                }
            }
            else
            {
                if (IsUtilityAbilityPressed && isUsingAbility == false && targetingManager.isTargetting == false)
                {
                    Cast(head, 1);
                }
            }
        };

        input.DefensiveAbility += IsDefensiveAbilityPressed =>
        {
            if (combatMode != true)
            {
                if (mannequin != null)
                {
                    SwapPart(2, mannequin.currentTorso);
                }
            }
            else
            {
                if (IsDefensiveAbilityPressed && isUsingAbility == false && targetingManager.isTargetting == false)
                {
                    Cast(torso, 2);
                }
            }
            
        };
        input.OfensiveAbility += IsOfensiveAbilityPressed =>
        {
            if (combatMode != true)
            {
                if (mannequin != null)
                {
                    SwapPart(3, mannequin.currentArms);
                }
            }
            else
            {
                if (IsOfensiveAbilityPressed && isUsingAbility == false && targetingManager.isTargetting == false)
                {
                    Cast(arms, 3);
                }
            }
            
        };
        input.MoveAbility += IsMoveAbilityPressed =>
        {
            if (combatMode != true)
            {
                if (mannequin != null)
                {
                    SwapPart(4, mannequin.currentLegs);
                }
            }
            else
            {
                if (IsMoveAbilityPressed && isUsingAbility == false && targetingManager.isTargetting == false)
                {
                    Cast(legs, 4);
                }
            }
            
        };


        input.EnablePlayerActions();

        cameraScript = mainCamera.GetComponent<CameraFollow>();
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
        }
        Move(CalculateMovementDirection());

        if (cameraScript.combatAarea == true)
        {
            combatMode = true;
        }
        else
        {
            combatMode = false;
        }

        head = ClassList[currentHead];
        torso = ClassList[currentTorso];
        arms = ClassList[currentArms];
        legs = ClassList[currentLegs];
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

    public void SwapPart(int partToSwap, int partNumber)
    {
        switch (partToSwap)
        {
            case 1:
                mannequin.SwapPart(partToSwap, currentHead);
                for (int i = 0; i < headParts.Count; i++)
                {
                    headParts[i].SetActive(i == partNumber);
                }
                break;
            case 2:
                mannequin.SwapPart(partToSwap, currentTorso);
                for (int i = 0; i < torsoParts.Count; i++)
                {
                    torsoParts[i].SetActive(i == partNumber);
                }
                break;
            case 3:
                mannequin.SwapPart(partToSwap, currentArms);
                for (int i = 0; i < headParts.Count; i++)
                {
                    armsParts[i].SetActive(i == partNumber);
                }
                break;
            case 4:
                mannequin.SwapPart(partToSwap, currentLegs);
                for (int i = 0; i < headParts.Count; i++)
                {
                    legsParts[i].SetActive(i == partNumber);
                }
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Mannequin"))
        {
            if (mannequin != null)
            {
                mannequin.UnHighlight();
            }
            mannequin = other.GetComponent<Mannequin>();
            mannequin.Highlight();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Mannequin") && mannequin == other.GetComponent<Mannequin>())
        {
            mannequin.UnHighlight();
            mannequin = null;
        }
    }

    public void Cast(ClassData classAbility, int partAbility)
    {
        AbilityData ability = null;

        switch (partAbility)
        {
            case 1: ability = classAbility.headAbility; break;
            case 2: ability = classAbility.torsoAbility; break;
            case 3: ability = classAbility.armsAbility; break;
            case 4: ability = classAbility.legsAbility; break;
            case 5: ability = classAbility.primaryAttack; break;
            case 6: ability = classAbility.secondaryAttack; break;
        }

        if (ability == null)
        {
            return;
        }

        //float castTime = ability.castTime;

        if (ability.castTime == 0)
        {
            ability.Target(targetingManager, this.gameObject, targetingManager.mouseWorldPosition);
            return;
        }

        if (castTimer != null)
        {
            try { castTimer.Stop(); } catch { }
            castTimer = null;
        }

        castTimer = new CountdownTimer(ability.castTime);
        castTimer.OnTimerStart = () => isUsingAbility = true;
        castTimer.Start();
        castTimer.OnTimerStop = () =>
        {
            isUsingAbility = false;
            ability.Target(targetingManager, this.gameObject, targetingManager.mouseWorldPosition);
        };

    }
}
