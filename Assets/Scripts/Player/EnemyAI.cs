using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float stopDistance = 2f;

    private Transform player;
    private EnemyHealth health;

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

        if (distance <= stopDistance) return;

        Vector3 dir = (player.position - transform.position).normalized;

        transform.position += dir * moveSpeed * Time.deltaTime;

        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
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