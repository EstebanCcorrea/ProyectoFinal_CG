using UnityEngine;
using System.Collections;

public class FallRespawn : MonoBehaviour
{
    [Header("Altura mínima para reaparecer")]
    public float fallThreshold = 10f;

    private CharacterController controller;
    private float fallStartY;
    private bool isFalling;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Si NO está tocando el suelo → está en el aire
        if (!controller.isGrounded)
        {
            // Marca el inicio de la caída
            if (!isFalling)
            {
                isFalling = true;
                fallStartY = transform.position.y;
                Debug.Log("[FallRespawn] Inicio de caída desde: " + fallStartY);
            }
        }
        else
        {
            // Cuando vuelve a tocar el suelo, analiza la caída
            if (isFalling)
            {
                float fallDistance = fallStartY - transform.position.y;
                Debug.Log("[FallRespawn] Fin de caída. Distancia caída: " + fallDistance);

                if (fallDistance > fallThreshold)
                {
                    Debug.Log("[FallRespawn] Caída demasiado alta (" + fallDistance + "). Ejecutando respawn con fade...");
                    StartCoroutine(RespawnSequence());
                }

                isFalling = false;
            }
        }
    }

    private IEnumerator RespawnSequence()
    {
        // 1. Fade Out (pantalla negra)
        if (FadeManager.Instance != null)
            yield return FadeManager.Instance.FadeOut(0.1f);

        // 2. Respawn real
        CheckpointManager.Instance.RespawnPlayer(gameObject);

        // 3. Fade In (vuelve a la vista del juego)
        if (FadeManager.Instance != null)
            yield return FadeManager.Instance.FadeIn(0.4f);
    }
}
