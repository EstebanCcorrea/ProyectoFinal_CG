using UnityEngine;
using System.Collections;

public class FallRespawn : MonoBehaviour
{
    [Header("Altura mínima para reaparecer")]
    public float fallThreshold = 10f;

    [Header("Audio de muerte")]
    public AudioSource deathAudioSource;
    public AudioClip deathSound;

    private CharacterController controller;
    private float fallStartY;
    private bool isFalling;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        
        if (!controller.isGrounded)
        {
            if (!isFalling)
            {
                isFalling = true;
                fallStartY = transform.position.y;
                Debug.Log("[FallRespawn] Inicio de caída desde: " + fallStartY);
            }
        }
        else
        {
            if (isFalling)
            {
                float fallDistance = fallStartY - transform.position.y;
                Debug.Log("[FallRespawn] Fin de caída. Distancia caída: " + fallDistance);

                if (fallDistance > fallThreshold)
                {
                    Debug.Log("[FallRespawn] Caída demasiado alta (" + fallDistance + "). Ejecutando respawn...");
                    StartCoroutine(RespawnSequence());
                }

                isFalling = false;
            }
        }
    }

    private IEnumerator RespawnSequence()
    {
       
        PlayDeathSound();

        if (FadeManager.Instance != null)
            yield return FadeManager.Instance.FadeOut(0.1f);

    
        CheckpointManager.Instance.RespawnPlayer(gameObject);

      
        if (FadeManager.Instance != null)
            yield return FadeManager.Instance.FadeIn(0.4f);
    }


    public IEnumerator RespawnSequenceFromOutside()
    {
      
        PlayDeathSound();

       
        if (FadeManager.Instance != null)
            yield return FadeManager.Instance.FadeOut(0.3f);

        
        CheckpointManager.Instance.RespawnPlayer(gameObject);

     
        if (FadeManager.Instance != null)
            yield return FadeManager.Instance.FadeIn(0.4f);
    }

    private void PlayDeathSound()
    {
        if (deathAudioSource != null && deathSound != null)
        {
            deathAudioSource.PlayOneShot(deathSound);
        }
    }
}
