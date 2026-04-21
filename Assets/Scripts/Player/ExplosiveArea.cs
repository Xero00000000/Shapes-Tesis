using UnityEngine;

public class ExplosiveArea : MonoBehaviour
{
    [Header("Lifetime")]
    [SerializeField] private float lifeTime = 1f;

    [Header("Charge Size")]
    [SerializeField] private float minRadius = 0.5f;
    [SerializeField] private float maxRadius = 5f;
    [SerializeField] private float maxChargeTime = 3f;

    [Header("Damage")]
    [SerializeField] private int damage = 20;
    [SerializeField] private LayerMask damageLayers;

    [Header("Visual")]
    [SerializeField] private Transform visual;

    private GameObject owner;
    private float radius;

    void Start()
    {
        ApplyDamage();
        UpdateVisual();

        Destroy(gameObject, lifeTime);
    }

    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner;
    }
    public void SetCharge(float chargeTime)
    {
        float chargePercent = Mathf.Clamp01(chargeTime / maxChargeTime);
        radius = Mathf.Lerp(minRadius, maxRadius, chargePercent);
    }

    void ApplyDamage()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, damageLayers);

        foreach (Collider hit in hits)
        {
            if (owner != null && hit.transform.root.gameObject == owner)
                continue;

            Health target = hit.GetComponentInParent<Health>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }

    void UpdateVisual()
    {
        if (visual == null) return;

        visual.localScale = Vector3.one * radius;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}