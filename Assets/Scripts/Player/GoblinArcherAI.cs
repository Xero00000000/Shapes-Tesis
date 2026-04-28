using UnityEngine;
using System.Collections;

public class GoblinArcherAI : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float detectRange = 18f;
    [SerializeField] private float idealDistance = 12f;
    [SerializeField] private float tooCloseDistance = 7f;

    [Header("Rotation")]
    [SerializeField] private float turnSpeed = 10f;

    [Header("Arrow Attack")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float arrowSpeed = 18f;
    [SerializeField] private int arrowDamage = 10;
    [SerializeField] private float shootCooldown = 1.5f;
    [SerializeField] private float aimDelay = 0.4f;

    [Header("Push Attack")]
    [SerializeField] private float pushRange = 3f;
    [SerializeField] private int pushDamage = 5;
    [SerializeField] private float pushStrength = 12f;
    [SerializeField] private float pushCooldown = 5f;
    [SerializeField] private float pushDelay = 0.5f;

    [Header("Visual")]
    [SerializeField] private Renderer rend;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color alertColor = Color.red;

    private Transform player;

    private float lastShootTime;
    private float lastPushTime;

    private bool shooting;
    private bool pushing;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (rend != null)
            normalColor = rend.material.color;
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist > detectRange) return;

        LookAtPlayer();

        if (shooting || pushing) return;

        if (dist <= pushRange)
        {
            TryPush();
            MoveAway();
        }
        else if (dist < tooCloseDistance)
        {
            MoveAway();
        }
        else if (dist > idealDistance)
        {
            MoveTowards();
        }
        else
        {
            TryShoot();
        }
    }

    void MoveTowards()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        dir.y = 0f;
        transform.position += dir * speed * Time.deltaTime;
    }

    void MoveAway()
    {
        Vector3 dir = (transform.position - player.position).normalized;
        dir.y = 0f;
        transform.position += dir * speed * Time.deltaTime;
    }

    void LookAtPlayer()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0f;

        if (dir.sqrMagnitude < 0.01f) return;

        Quaternion rot = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            rot,
            turnSpeed * Time.deltaTime
        );
    }

    void TryShoot()
    {
        if (Time.time < lastShootTime + shootCooldown) return;

        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        shooting = true;

        yield return new WaitForSeconds(aimDelay);

        FireArrow();

        lastShootTime = Time.time;
        shooting = false;
    }

    void FireArrow()
    {
        if (arrowPrefab == null || shootPoint == null)
        {
            Debug.LogWarning("Falta prefab o punto de disparo");
            return;
        }

        Vector3 dir = (player.position - shootPoint.position).normalized;
        dir.y = 0f;

        GameObject arrow = Instantiate(
            arrowPrefab,
            shootPoint.position,
            Quaternion.LookRotation(dir)
        );

        Rigidbody rb = arrow.GetComponent<Rigidbody>();

        if (rb != null)
            rb.linearVelocity = dir * arrowSpeed;

        Projectile proj = arrow.GetComponent<Projectile>();

        if (proj != null)
        {
            proj.SetDamage(arrowDamage);
            proj.SetOwner(gameObject);
        }
    }

    void TryPush()
    {
        if (Time.time < lastPushTime + pushCooldown) return;

        StartCoroutine(Push());
    }

    IEnumerator Push()
    {
        pushing = true;

        if (rend != null)
            rend.material.color = alertColor;

        yield return new WaitForSeconds(pushDelay);

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= pushRange + 0.5f)
        {
            Health hp = player.GetComponent<Health>();

            if (hp != null)
                hp.TakeDamage(pushDamage);

            PlayerKnockback kb = player.GetComponent<PlayerKnockback>();

            if (kb != null)
            {
                Vector3 dir = player.position - transform.position;
                kb.ApplyKnockback(dir, pushStrength);
            }
        }

        if (rend != null)
            rend.material.color = normalColor;

        lastPushTime = Time.time;
        pushing = false;
    }
}