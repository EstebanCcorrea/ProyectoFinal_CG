using UnityEngine;

public class DañoLava : MonoBehaviour
{
    [Header("Daño por Lava")]
    public float damageAmount = 10f;  
    public float damageInterval = 1f;  

    private float lastDamageTime = 0f;  


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))  
        {
            
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth == null) return;

            
            if (Time.time - lastDamageTime > damageInterval)
            {
                lastDamageTime = Time.time;
                playerHealth.TakeDamage(damageAmount); 

             
                if (playerHealth.CurrentHealth <= 0)
                {
                    RespawnPlayer(other);  
                }
            }
        }
    }

   
    private void RespawnPlayer(Collider player)
    {
        // Obtener el punto de inicio (puedes asignarlo en el Inspector)
        Transform spawnPoint = GameManager.Instance.GetSpawnPoint();
        if (spawnPoint != null)
        {
            player.transform.position = spawnPoint.position;  // Posicionamos al jugador en el spawn
            player.transform.rotation = spawnPoint.rotation;  

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.RestoreHealth();  
            }
        }
    }
}
