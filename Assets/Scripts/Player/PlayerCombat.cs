using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerCombat : MonoBehaviour
{
    [Header("Ataque rápido (Click Izquierdo)")]
    [SerializeField] private GameObject quickProjectilePrefab;
    [SerializeField] private Transform quickFirePoint;
    [SerializeField] private Vector3 quickOffset;
    [SerializeField] private float quickProjectileForce = 10f;
    [SerializeField] private int quickProjectileDamage = 8;
    [SerializeField] private float quickCooldown = 0.3f;

    [Header("Ataque cargado (Click Derecho)")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform chargedFirePoint;
    [SerializeField] private Vector3 chargedOffset;
    [SerializeField] private float projectileForce = 15f;
    [SerializeField] private float maxChargeTime = 2f;
    [SerializeField] private int projectileDamage = 25;
    [SerializeField] private float chargedCooldown = 1f;

    [Header("Post Proceso Ki")]
    [SerializeField] private ScriptableRendererFeature brolyFeature;
    [SerializeField] private Material kiEffectMaterial;
    [SerializeField] private Transform kiCenterTarget;

    private float explosionValue;
    private bool isExploding;

    private float lastQuickAttackTime;
    private float lastChargedAttackTime;

    private float chargeStartTime;
    private bool isCharging;

    void Start()
    {
        kiEffectMaterial = Instantiate(kiEffectMaterial);

        // apagar efecto al inicio
        if (brolyFeature != null)
            brolyFeature.SetActive(false);
    }

    void Update()
    {
        HandleQuickShot();
        HandleChargedAttack();
        UpdateKiEffect();
    }

    #region QUICK ATTACK

    void HandleQuickShot()
    {
        if (isCharging) return;

        if (Input.GetMouseButtonDown(0) && Time.time >= lastQuickAttackTime + quickCooldown)
        {
            lastQuickAttackTime = Time.time;

            Vector3 spawnPos = quickFirePoint.position + quickFirePoint.TransformDirection(quickOffset);

            GameObject projectile = Instantiate(quickProjectilePrefab, spawnPos, quickFirePoint.rotation);

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
                rb.linearVelocity = quickFirePoint.forward * quickProjectileForce;

            Projectile proj = projectile.GetComponent<Projectile>();
            if (proj != null)
            {
                proj.SetDamage(quickProjectileDamage);
                proj.SetOwner(gameObject);
            }
        }
    }

    #endregion

    #region CHARGED ATTACK

    void HandleChargedAttack()
    {
        // INICIO CARGA
        if (Input.GetMouseButtonDown(1) && Time.time >= lastChargedAttackTime + chargedCooldown)
        {
            isCharging = true;
            chargeStartTime = Time.time;

            if (brolyFeature != null)
                brolyFeature.SetActive(true);
        }

        // SOLTAR
        if (Input.GetMouseButtonUp(1) && isCharging)
        {
            float chargeTime = Mathf.Clamp(Time.time - chargeStartTime, 0f, maxChargeTime);
            float chargePercent = chargeTime / maxChargeTime;

            ShootProjectile(chargePercent);

            lastChargedAttackTime = Time.time;
            isCharging = false;

            StartExplosion();
        }
    }

    void ShootProjectile(float chargePercent)
    {
        Vector3 spawnPos = chargedFirePoint.position + chargedFirePoint.TransformDirection(chargedOffset);

        GameObject projectile = Instantiate(projectilePrefab, spawnPos, chargedFirePoint.rotation);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            float finalForce = projectileForce * (1f + chargePercent);
            rb.linearVelocity = chargedFirePoint.forward * finalForce;
        }

        Projectile proj = projectile.GetComponent<Projectile>();
        if (proj != null)
        {
            proj.SetDamage(Mathf.RoundToInt(projectileDamage * (1f + chargePercent)));
            proj.SetOwner(gameObject);
        }
    }

    #endregion

    #region KI EFFECT

    void UpdateKiEffect()
    {
        // CENTRO
        Transform target = kiCenterTarget != null ? kiCenterTarget : transform;
        Vector3 screenPos = Camera.main.WorldToViewportPoint(target.position);
        kiEffectMaterial.SetVector("_Center", new Vector2(screenPos.x, screenPos.y));

        // CARGA
        if (isCharging)
        {
            float chargeTime = Mathf.Clamp(Time.time - chargeStartTime, 0f, maxChargeTime);
            float chargePercent = chargeTime / maxChargeTime;

            kiEffectMaterial.SetFloat("_Charge", chargePercent);
        }
        else
        {
            kiEffectMaterial.SetFloat("_Charge", 0f);
        }

        // EXPLOSIÓN
        if (isExploding)
        {
            float chargeTime = Mathf.Clamp(Time.time - chargeStartTime, 0f, maxChargeTime);
            float chargePercent = chargeTime / maxChargeTime;

            explosionValue += Time.deltaTime * (2f + chargePercent * 6f);

            kiEffectMaterial.SetFloat("_Explosion", explosionValue);

            if (explosionValue >= 1f)
            {
                explosionValue = 0f;
                isExploding = false;

                kiEffectMaterial.SetFloat("_Explosion", 0f);

                if (brolyFeature != null)
                    brolyFeature.SetActive(false);
            }
        }
    }

    void StartExplosion()
    {
        isExploding = true;
        explosionValue = 0f;
    }

    #endregion
}