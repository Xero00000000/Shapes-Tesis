using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityUtils;

class SelfTargeting : TargetingStrategy
{
    public override void Start(AbilityData ability, TargetingManager targetingManager, GameObject caster, Vector3 point)
    {
        this.ability = ability;
        this.targetingManager = targetingManager;
        

        if (targetingManager.transform.TryGetComponent<IDamageable>(out var target))
        {
            ability.Execute(target, caster, point);
        }

        Debug.Log($"anda 3");
    }
}

class AOETargeting : TargetingStrategy
{
    [SerializeField] GameObject aoePrefab;
    [SerializeField] float aoeRadius;
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] float heightOffset;

    GameObject previewInstance;

    public override void Start(AbilityData ability, TargetingManager targetingManager, GameObject caster, Vector3 point)
    {
        this.ability = ability;
        this.targetingManager = targetingManager;
        isTargetting = true;
        targetingManager.isTargetting = true;

        targetingManager.SetCurrentStrategy(this);

        if (aoePrefab != null)
        {
            previewInstance = UnityEngine.Object.Instantiate(aoePrefab, targetingManager.mouseWorldPosition + new Vector3(0f, heightOffset, 0f), Quaternion.identity);
        }

        if (targetingManager.input != null)
        {//despues veo como mierda hago un raycast event y simplifico
            targetingManager.input.PrimaryAttack += IsPrimaryAttackPressed =>
            {
                if (IsPrimaryAttackPressed)
                {
                    OnClick(caster, point);
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

        previewInstance.transform.position = targetingManager.mouseWorldPosition + new Vector3(0f, heightOffset, 0f);
        previewInstance.transform.rotation = targetingManager.lookRotation;
    }

    public override void Cancel()
    {
        isTargetting = false;
        targetingManager.isTargetting = false;

        targetingManager.ClearCurrentStrategy();

        if (previewInstance != null)
        {
            UnityEngine.Object.Destroy(previewInstance);
        }
        /*if (targetingManager.input != null)
        {
            targetingManager.input.PrimaryAttack -= Onclick;
        }*/
        //targetingManager.input.PrimaryAttack -= IsPrimaryAttackPressed => { };
    }

    void OnClick(GameObject caster, Vector3 point)
    {
        if (isTargetting)
        {
            var targets = Physics.OverlapSphere(targetingManager.mouseWorldPosition, aoeRadius)
                .Select(c => c.GetComponent<IDamageable>())
                .OfType <IDamageable>();

            foreach (var target in targets)
            {
                ability.Execute(target, caster, point);
            }

            Cancel();
        }
    }
}

class ProjectileTargeting : TargetingStrategy
{
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float projectileLifetime;
    public override void Start(AbilityData ability, TargetingManager targetingManager, GameObject caster, Vector3 point)
    {
        this.ability = ability;
        this.targetingManager = targetingManager;

        if (projectilePrefab != null)
        {
            /*
            var flatForward = targetingManager.cam.transform.forward.normalized;
            var forwardRotation = Quaternion.LookRotation(flatForward);
            var projectile = Object.Instantiate(projectilePrefab, targetingManager.transform.position, forwardRotation);
            projectile.GetComponent<ProjectileController>().Initialize(ability, projectileSpeed, projectileLifetime);
            */
            var projectile = Object.Instantiate(projectilePrefab, targetingManager.transform.position, targetingManager.lookRotation);
            projectile.GetComponent<ProjectileController>().Initialize(ability, projectileSpeed, projectileLifetime);
        }
    }
}

class ConjureAOETargeting : TargetingStrategy
{
    [SerializeField] GameObject conjurePrefab;
    [SerializeField] GameObject previewPrefab;
    [SerializeField] float aoeRadius;
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] float aoeLifetime;
    [SerializeField] float heightOffset;

    GameObject previewInstance;

    public override void Start(AbilityData ability, TargetingManager targetingManager, GameObject caster, Vector3 point)
    {
        this.ability = ability;
        this.targetingManager = targetingManager;
        isTargetting = true;
        targetingManager.isTargetting = true;

        targetingManager.SetCurrentStrategy(this);

        if (previewPrefab != null)
        {
            previewInstance = UnityEngine.Object.Instantiate(previewPrefab, targetingManager.mouseWorldPosition + new Vector3(0f, heightOffset, 0f), targetingManager.lookRotation);
        }

        if (targetingManager.input != null)
        {//despues veo como mierda hago un raycast event y simplifico
            targetingManager.input.PrimaryAttack += IsPrimaryAttackPressed =>
            {
                if (IsPrimaryAttackPressed)
                {
                    OnClick(caster, point);
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

        previewInstance.transform.position = targetingManager.mouseWorldPosition + new Vector3(0f, heightOffset, 0f);
        previewInstance.transform.rotation = targetingManager.lookRotation;
    }

    public override void Cancel()
    {
        isTargetting = false;
        targetingManager.isTargetting = false;

        targetingManager.ClearCurrentStrategy();

        if (previewInstance != null)
        {
            UnityEngine.Object.Destroy(previewInstance);
        }
        /*if (targetingManager.input != null)
        {
            targetingManager.input.PrimaryAttack -= Onclick;
        }*/
        //targetingManager.input.PrimaryAttack -= IsPrimaryAttackPressed => { };
    }

    void OnClick(GameObject caster, Vector3 point)
    {
        if (isTargetting)
        {/*
            var targets = Physics.OverlapSphere(targetingManager.mouseWorldPosition, aoeRadius)
                .Select(c => c.GetComponent<IDamageable>())
                .OfType<IDamageable>();

            foreach (var target in targets)
            {
                ability.Execute(target);
            }*/
            var conjuration = Object.Instantiate(conjurePrefab, targetingManager.mouseWorldPosition + new Vector3(0f, heightOffset, 0f), targetingManager.lookRotation);
            conjuration.GetComponent<AOEController>().Initialize(ability, aoeLifetime);


            Cancel();
        }
        
    }
}

class PointTargeting : TargetingStrategy
{
    [SerializeField] GameObject conjurePrefab;
    [SerializeField] GameObject previewPrefab;
    [SerializeField] float aoeRadius;
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] float aoeLifetime;

    GameObject previewInstance;

    public override void Start(AbilityData ability, TargetingManager targetingManager, GameObject caster, Vector3 point)
    {
        this.ability = ability;
        this.targetingManager = targetingManager;
        isTargetting = true;
        targetingManager.isTargetting = true;

        targetingManager.SetCurrentStrategy(this);

        if (previewPrefab != null)
        {
            previewInstance = UnityEngine.Object.Instantiate(previewPrefab, targetingManager.mouseWorldPosition + new Vector3(0f, 0.1f, 0f), Quaternion.identity);
        }

        if (targetingManager.input != null)
        {//despues veo como mierda hago un raycast event y simplifico
            targetingManager.input.PrimaryAttack += IsPrimaryAttackPressed =>
            {
                if (IsPrimaryAttackPressed)
                {
                    OnClick(caster, point);
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
        targetingManager.isTargetting = false;

        targetingManager.ClearCurrentStrategy();

        if (previewInstance != null)
        {
            UnityEngine.Object.Destroy(previewInstance);
        }
        /*if (targetingManager.input != null)
        {
            targetingManager.input.PrimaryAttack -= Onclick;
        }*/
        //targetingManager.input.PrimaryAttack -= IsPrimaryAttackPressed => { };
    }

    void OnClick(GameObject caster, Vector3 point)
    {
        if (isTargetting)
        {/*
            var targets = Physics.OverlapSphere(targetingManager.mouseWorldPosition, aoeRadius)
                .Select(c => c.GetComponent<IDamageable>())
                .OfType<IDamageable>();

            foreach (var target in targets)
            {
                ability.Execute(target);
            }*/
            var conjuration = Object.Instantiate(conjurePrefab, targetingManager.mouseWorldPosition + new Vector3(0f, 0.1f, 0f), Quaternion.identity);
            conjuration.GetComponent<AOEController>().Initialize(ability, aoeLifetime);


            Cancel();
        }

    }
}