using UnityEngine;

class ProjectileController : MonoBehaviour
{
    AbilityData ability;
    float speed;
    float lifetime;

    public void Initialize(AbilityData ability, float speed, float newLifetime)
    {
        this.ability = ability;
        this.speed = speed;
        this.lifetime = newLifetime;
        Destroy(gameObject, lifetime);
    }

    void Update() => transform.Translate(Vector3.forward * (speed * Time.deltaTime));

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;
        if (other.gameObject.TryGetComponent<IDamageable>(out var target))
        {
            ability.Execute(target, this.gameObject, Vector3.zero);
            Destroy(gameObject);
        }/*
        else
        {
            Destroy(gameObject);
        }*/
    }
}
