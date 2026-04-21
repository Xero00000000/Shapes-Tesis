using UnityEngine;
using System.Collections.Generic;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private int damagePerTick = 5;
    [SerializeField] private float tickRate = 1f;

    private List<Health> targets = new List<Health>();

    void Start()
    {
        InvokeRepeating(nameof(DealDamage), 0f, tickRate);
    }

    void DealDamage()
    {
        foreach (Health target in targets)
        {
            if (target != null)
            {
                target.TakeDamage(damagePerTick);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Health target = other.GetComponentInParent<Health>();

        if (target != null && !targets.Contains(target))
        {
            targets.Add(target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Health target = other.GetComponentInParent<Health>();

        if (target != null && targets.Contains(target))
        {
            targets.Remove(target);
        }
    }
}