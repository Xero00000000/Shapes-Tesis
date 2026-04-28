using UnityEngine;
using System.Collections;

public class GoblinArcherAI : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float detectionRange = 18f;
    [SerializeField] private float preferredDistance = 12f;
    [SerializeField] private float minDistance = 7f;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Arrow Attack")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float arrowForce = 18f;
    [SerializeField] private int arrowDamage = 10;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float aimTime = 0.4f;

    [Header("Close Push Attack")]
    [SerializeField] private float pushTriggerDistance = 3f;
    [SerializeField] private int pushDamage = 5;
    [SerializeField] private float pushForce = 12f;
    [SerializeField] private float pushCooldown = 5f;
    [SerializeField] private float pushWindUp = 0.5f;

    [Header("Attack Feedback")]
    [SerializeField] private Renderer goblinRenderer;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color warningColor = Color.red;

    private Transform player;
    private float lastAttackTime;
    private float lastPushTime;
    private bool isShooting;
    private bool isPushing;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (goblinRenderer != null)
            normalColor = goblinRenderer.material.color;
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > detectionRange) return;

        RotateTowardsPlayer();

        if (isShooting || isPushing) return;

        if (distance <= pushTriggerDistance)
        {
            TryPushPlayer();
            MoveAwayFromPlayer();
        }
        else if (distance < minDistance)
        {
            MoveAwayFromPlayer();
        }
        else if (distance > preferredDistance)
        {
            MoveTowardsPlayer();
        }
        else
        {
            TryShoot();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0f;
        dir.Normalize();

        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    private void MoveAwayFromPlayer()
    {
        Vector3 dir = transform.position - player.position;
        dir.y = 0f;
        dir.Normalize();

        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    private void RotateTowardsPlayer()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0f;

        if (dir.sqrMagnitude < 0.01f) return;

        Quaternion targetRotation = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private void TryShoot()
    {
        if (Time.time < lastAttackTime + attackCooldown) return;

        StartCoroutine(ShootRoutine());
    }

    private IEnumerator ShootRoutine()
    {
        isShooting = true;

        yield return new WaitForSeconds(aimTime);

        ShootArrow();

        lastAttackTime = Time.time;
        isShooting = false;
    }

    private void ShootArrow()
    {
        if (arrowPrefab == null || firePoint == null)
        {
            Debug.LogWarning("Falta asignar Arrow Prefab o FirePoint");
            return;
        }

        Vector3 dir = player.position - firePoint.position;
        dir.y = 0f;
        dir.Normalize();

        GameObject arrow = Instantiate(
            arrowPrefab,
            firePoint.position,
            Quaternion.LookRotation(dir)
        );

        Rigidbody rb = arrow.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = dir * arrowForce;
        }

        Projectile projectile = arrow.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.SetDamage(arrowDamage);
            projectile.SetOwner(gameObject);
        }
    }

    private void TryPushPlayer()
    {
        if (Time.time < lastPushTime + pushCooldown) return;

        StartCoroutine(PushRoutine());
    }

    private IEnumerator PushRoutine()
    {
        isPushing = true;

        if (goblinRenderer != null)
            goblinRenderer.material.color = warningColor;

        yield return new WaitForSeconds(pushWindUp);

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= pushTriggerDistance + 0.5f)
        {
            Health health = player.GetComponent<Health>();

            if (health != null)
            {
                health.TakeDamage(pushDamage);
            }

            PlayerKnockback knockback = player.GetComponent<PlayerKnockback>();

            if (knockback != null)
            {
                Vector3 pushDir = player.position - transform.position;
                knockback.ApplyKnockback(pushDir, pushForce);
            }
            else
            {
                Debug.LogWarning("El Player no tiene PlayerKnockback");
            }
        }

        if (goblinRenderer != null)
            goblinRenderer.material.color = normalColor;

        lastPushTime = Time.time;
        isPushing = false;
    }
}