using UnityEngine;

class ProjectileController : MonoBehaviour
{
    AbilityData ability;
    float speed;
    float lifetime;

    public void Initialize(AbilityData ability, float speed)
    {
        this.ability = ability;
        this.speed = speed;
        Destroy(gameObject, lifetime);
    }

    void Update() => transform.Translate(Vector3.forward * (speed * Time.deltaTime));

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;
        if (other.gameObject.TryGetComponent<IDamageable>(out var target))
        {
            ability.Execute(target);
            Destroy(gameObject);
        }
    }
}
