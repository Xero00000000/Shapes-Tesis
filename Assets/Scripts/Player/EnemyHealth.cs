using UnityEngine;

public class EnemyHealth : Health
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    protected override void Awake()
    {
        base.Awake();

        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    protected override void Die()
    {
        base.Die();

        Debug.Log("Enemigo murió");

        gameObject.SetActive(false);
    }

    public void Respawn()
    {
        currentHealth = maxHealth;

        transform.position = initialPosition;
        transform.rotation = initialRotation;

        gameObject.SetActive(true);
    }
}