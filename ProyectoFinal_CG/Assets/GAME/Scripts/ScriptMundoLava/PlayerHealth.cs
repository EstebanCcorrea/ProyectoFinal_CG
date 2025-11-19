using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.playerHealth = this;
            GameManager.Instance.UpdateHealthUI(currentHealth, maxHealth);
        }
    }

    public void TakeDamage(float amount)
    {
        if (amount <= 0f) return;

        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0f);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateHealthUI(currentHealth, maxHealth);
        }

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ShowMessage("Has muerto en la lava...");
            GameManager.Instance.RestartScene();
        }
    }
}
