using UnityEngine;

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
