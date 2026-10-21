
using UnityEngine;

public class TargetHealth : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private float maxHealth = 100f;

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0f || currentHealth <= 0f)
            return;

        currentHealth -= damage;

        if (currentHealth <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
