using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f;

    private int damage;
    private GameObject owner;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // =========================
    // SETTERS
    // =========================
    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner;

        // Ignorar colisión con el que dispara
        Collider myCol = GetComponent<Collider>();
        Collider ownerCol = owner.GetComponent<Collider>();

        if (myCol != null && ownerCol != null)
        {
            Physics.IgnoreCollision(myCol, ownerCol);
        }
    }

    // =========================
    // COLLISION (normal)
    // =========================
    private void OnCollisionEnter(Collision collision)
    {
        HandleHit(collision.collider);
    }

    // =========================
    // TRIGGER (por si lo usas)
    // =========================
    private void OnTriggerEnter(Collider other)
    {
        HandleHit(other);
    }

    // =========================
    // HIT LOGIC (UNIFICADO)
    // =========================
    private void HandleHit(Collider hit)
    {
        // evitar auto-damage
        if (owner != null && hit.transform.root.gameObject == owner)
            return;

        Health target = hit.GetComponentInParent<Health>();

        if (target != null)
        {
            target.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}