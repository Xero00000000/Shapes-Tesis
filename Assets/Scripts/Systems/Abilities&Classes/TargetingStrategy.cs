using UnityEngine;

abstract class TargetingStrategy
{
    protected AbilityData ability;
    protected TargetingManager targetingManager;
    protected bool isTargetting = false;

    public bool IsTargetting => isTargetting;

    public abstract void Start(AbilityData ability, TargetingManager targetingManager, GameObject caster, Vector3 point);
    public virtual void Update() { }
    public virtual void Cancel() { }
}
