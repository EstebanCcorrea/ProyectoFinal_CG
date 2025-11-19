using UnityEngine;

public class DañoLava : MonoBehaviour
{
    [Header("Daño por Lava")]
    public float damageAmount = 10f;  // Daño que recibe el jugador
    public float damageInterval = 1f;  

    private float lastDamageTime = 0f;  // Para controlar el intervalo entre daños

    // Este evento será llamado cuando el player entre en el rio de lava
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))  
        {
            // Si el jugador no tiene vida, no hacemos nada
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth == null) return;

            // Controlamos el intervalo entre daños
            if (Time.time - lastDamageTime > damageInterval)
            {
                lastDamageTime = Time.time;
                playerHealth.TakeDamage(damageAmount);  // Aplica el daño al jugador

                // Verificar si el jugador ha muerto
                if (playerHealth.CurrentHealth <= 0)
                {
                    RespawnPlayer(other);  // Si se quedó sin vida, lo respawneamos
                }
            }
        }
    }

    // Método para hacer que el jugador respawnee en el punto de inicio
    private void RespawnPlayer(Collider player)
    {
        // Obtener el punto de inicio (puedes asignarlo en el Inspector)
        Transform spawnPoint = GameManager.Instance.GetSpawnPoint();
        if (spawnPoint != null)
        {
            player.transform.position = spawnPoint.position;  // Posicionamos al jugador en el spawn
            player.transform.rotation = spawnPoint.rotation;  // Aseguramos que la rotación también sea correcta

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.RestoreHealth();  
            }
        }
    }
}
