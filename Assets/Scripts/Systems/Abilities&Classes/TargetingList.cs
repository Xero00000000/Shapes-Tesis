using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityUtils;

class SelfTargeting : TargetingStrategy
{
    public override void Start(AbilityData ability, TargetingManager targetingManager)
    {
        this.ability = ability;
        this.targetingManager = targetingManager;

        if (targetingManager.transform.TryGetComponent<IDamageable>(out var target))
        {
            ability.Execute(target);
        }
    }
}

class AOETargeting : TargetingStrategy
{
    [SerializeField] GameObject aoePrefab;
    [SerializeField] float aoeRadius;
    [SerializeField] LayerMask groundLayerMask;

    GameObject previewInstance;

    public override void Start(AbilityData ability, TargetingManager targetingManager)
    {
        this.ability = ability;
        this.targetingManager = targetingManager;
        isTargetting = true;

        targetingManager.SetCurrentStrategy(this);

        if (aoePrefab != null)
        {
            previewInstance = UnityEngine.Object.Instantiate(aoePrefab, targetingManager.mouseWorldPosition + new Vector3(0f, 0.1f, 0f), Quaternion.identity);
        }

        if (targetingManager.input != null)
        {//despues veo como mierda hago un raycast event y simplifico
            targetingManager.input.PrimaryAttack += IsPrimaryAttackPressed =>
            {
                if (IsPrimaryAttackPressed)
                {
                    OnClick();
                }
                else
                {

                }
            };
        }
    }

    public override void Update()
    {
        if (!IsTargetting || previewInstance == null) return;

        previewInstance.transform.position = targetingManager.mouseWorldPosition + new Vector3(0f, 0.1f, 0f);
    }

    public override void Cancel()
    {
        isTargetting = false;

        targetingManager.ClearCurrentStrategy();

        if (previewInstance != null)
        {
            UnityEngine.Object.Destroy(previewInstance);
        }
        /*if (targetingManager.input != null)
        {
            targetingManager.input.PrimaryAttack -= Onclick;
        }*/
    }

    void OnClick()
    {
        if (isTargetting)
        {
            var targets = Physics.OverlapSphere(targetingManager.mouseWorldPosition, aoeRadius)
                .Select(c => c.GetComponent<IDamageable>())
                .OfType <IDamageable>();

            foreach (var target in targets)
            {
                ability.Execute(target);
            }

            Cancel();
        }
    }
}

class ProjectileTargeting : TargetingStrategy
{
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public override void Start(AbilityData ability, TargetingManager targetingManager)
    {
        this.ability = ability;
        this.targetingManager = targetingManager;

        if (projectilePrefab != null)
        {
            var flatForward = targetingManager.cam.transform.forward.normalized;
            var forwardRotation = Quaternion.LookRotation(flatForward);
            var projectile = Object.Instantiate(projectilePrefab, targetingManager.transform.position, forwardRotation);
            projectile.GetComponent<ProjectileController>().Initialize(ability, projectileSpeed);
        }
    }
}