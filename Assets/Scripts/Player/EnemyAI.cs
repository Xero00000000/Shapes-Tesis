using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float stopDistance = 2f;

    [Header("Attack")]
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackCooldown = 1f;

    private Transform player;
    private EnemyHealth health;
    private float lastAttackTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        health = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        HandleRespawn();
        FollowPlayer();
    }

    void FollowPlayer()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > detectionRange) return;

        if (distance > stopDistance)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;
        }
        else
        {
            AttackPlayer();
        }

        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

    void AttackPlayer()
    {
        if (Time.time < lastAttackTime + attackCooldown) return;

        Health target = player.GetComponent<Health>();

        if (target != null)
        {
            target.TakeDamage(damage);
            lastAttackTime = Time.time;
        }
    }

    void HandleRespawn()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (health != null)
            {
                health.Respawn();
            }
        }
    }
}