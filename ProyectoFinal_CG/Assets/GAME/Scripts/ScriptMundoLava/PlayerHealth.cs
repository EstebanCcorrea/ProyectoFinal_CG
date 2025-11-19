using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float CurrentHealth;  

    private void Start()
    {
        CurrentHealth = maxHealth;  

        // Si GameManager existe, lo vinculamos 
        if (GameManager.Instance != null)
        {
            GameManager.Instance.playerHealth = this;
            GameManager.Instance.UpdateHealthUI(CurrentHealth, maxHealth);  // se actualiza la UI
        }
    }

    // Método para aplicar daño al player
    public void TakeDamage(float amount)
    {
        if (amount <= 0f) return;

        CurrentHealth -= amount;
        CurrentHealth = Mathf.Max(CurrentHealth, 0f);  

        // Actualizar la UI
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateHealthUI(CurrentHealth, maxHealth);
        }

        if (CurrentHealth <= 0f)
        {
           
        }
    }

 

    public void RestoreHealth()
    {
        CurrentHealth = maxHealth; 
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateHealthUI(CurrentHealth, maxHealth); 
        }
    }
}
