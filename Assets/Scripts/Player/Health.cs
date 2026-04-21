using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected int currentHealth;

    [Header("Eventos")]
    public UnityEvent onDamage;
    public UnityEvent onDeath;
    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int amount)
    {
        if (amount <= 0) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        onDamage?.Invoke();

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public virtual void Heal(int amount)
    {
        if (amount <= 0) return;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    protected virtual void Die()
    {
        onDeath?.Invoke();
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetHealthPercent()
    {
        return (float)currentHealth / maxHealth;
    }
}