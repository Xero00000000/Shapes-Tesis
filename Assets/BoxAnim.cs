using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoxAnim : MonoBehaviour
{
    [SerializeField] private Animator doorLeft;
    [SerializeField] private Animator doorRight;

    private bool isOpen;
    private bool playerInside;

    private void Start()
    {
        isOpen = false;

        doorLeft.enabled = false;
        doorRight.enabled = false;
    }
    private void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            doorLeft.enabled = true; doorRight.enabled = true;
            doorLeft.SetBool("open", isOpen);doorRight.SetBool("open", isOpen);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInside = false;
    }


}

