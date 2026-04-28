using UnityEngine;

abstract class TargetingStrategy
{
    protected AbilityData ability;
    protected TargetingManager targetingManager;
    protected bool isTargetting = false;

    public bool IsTargetting => isTargetting;

    public abstract void Start(AbilityData ability, TargetingManager targetingManager);
    public virtual void Update() { }
    public virtual void Cancel() { }
}
