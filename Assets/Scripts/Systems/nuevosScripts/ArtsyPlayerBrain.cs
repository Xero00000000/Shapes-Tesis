using ImprovedTimers;
using Unity.VisualScripting;

//using UnityEditor.Playables;
using UnityEngine;
using UnityUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class ArtsyPlayerBrain : MonoBehaviour
{
    [SerializeField] InputReader input;
    bool isUsingAbility = false;
    Vector2 moveInput;
    [SerializeField] float moveSpeed;
    [SerializeField] Transform playerModel;
    [SerializeField] Camera mainCamera;
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

        input.Move += direction => moveInput = direction;

        input.UtilityAbility += IsUtilityAbilityPressed =>
        {
            if (mannequin != null)
            {
                SwapPart(1, mannequin.currentHead);
            }
        };

        input.DefensiveAbility += IsDefensiveAbilityPressed =>
        {
            if (mannequin != null)
            {
                SwapPart(2, mannequin.currentTorso);
            }
            
        };
        input.OfensiveAbility += IsOfensiveAbilityPressed =>
        {
            if (mannequin != null)
            {
                SwapPart(3, mannequin.currentArms);
            }
            
        };
        input.MoveAbility += IsMoveAbilityPressed =>
        {
            if (mannequin != null)
            {
                SwapPart(4, mannequin.currentLegs);
            }
            
        };


        input.EnablePlayerActions();
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
            mannequin = other.GetComponent<Mannequin>();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Mannequin") && mannequin == other.GetComponent<Mannequin>())
        {
            mannequin = null;
        }
    }
}
