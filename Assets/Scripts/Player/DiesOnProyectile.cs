using UnityEngine;

public class DiesOnProyectile : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private LayerMask projectileLayer;

    private void OnCollisionEnter(Collision collision)
    {
        CheckHit(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckHit(other.gameObject);
    }

    void CheckHit(GameObject other)
    {
        if (((1 << other.layer) & projectileLayer) == 0)
            return;

        Die();
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} murió por proyectil");

        Destroy(gameObject);
    }
}