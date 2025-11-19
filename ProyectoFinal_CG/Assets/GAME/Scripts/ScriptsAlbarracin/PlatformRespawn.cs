using UnityEngine;
using System.Collections;

public class KillPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("[KillPlatform] Plataforma peligrosa tocada. Respawn...");

            // Iniciar secuencia igual que FallRespawn
            other.GetComponent<FallRespawn>().StartCoroutine(
                other.GetComponent<FallRespawn>().RespawnSequenceFromOutside()
            );
        }
    }
}
