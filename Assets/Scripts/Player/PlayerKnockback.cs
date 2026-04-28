using UnityEngine;
using System.Collections;

public class PlayerKnockback : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private PadreMove playerMove;

    [Header("Knockback")]
    [SerializeField] private float knockbackDrag = 6f;
    [SerializeField] private float controlLockTime = 0.25f;

    private Vector3 knockbackVelocity;
    private bool isKnocked;

    private void Awake()
    {
        if (controller == null)
            controller = GetComponent<CharacterController>();

        if (playerMove == null)
            playerMove = GetComponent<PadreMove>();
    }

    private void Update()
    {
        if (controller == null) return;

        if (knockbackVelocity.magnitude > 0.05f)
        {
            controller.Move(knockbackVelocity * Time.deltaTime);
            knockbackVelocity = Vector3.Lerp(knockbackVelocity, Vector3.zero, knockbackDrag * Time.deltaTime);
        }
        else
        {
            knockbackVelocity = Vector3.zero;
        }
    }

    public void ApplyKnockback(Vector3 direction, float force)
    {
        direction.y = 0f;
        direction.Normalize();

        knockbackVelocity = direction * force;

        if (!isKnocked)
            StartCoroutine(LockControlRoutine());
    }

    private IEnumerator LockControlRoutine()
    {
        isKnocked = true;

        if (playerMove != null)
            playerMove.enabled = false;

        yield return new WaitForSeconds(controlLockTime);

        if (playerMove != null)
            playerMove.enabled = true;

        isKnocked = false;
    }
}