using UnityEngine;

public class HeartInteraction : MonoBehaviour
{
    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.E;
    public Camera playerCamera;

    private Animator heartAnimator;

    void Start()
    {
        
        heartAnimator = GameObject.Find("heart").GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerCamera == null || heartAnimator == null) return;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            if (hit.collider.CompareTag("Heart"))
            {
                if (Input.GetKeyDown(interactKey))
                {
                    // Activar la animación del corazón
                    heartAnimator.SetTrigger("StartHeartPulse"); 

                    // Llamada para recolectar el ítem
                    CollectHeart(hit.collider.gameObject);
                }
            }
        }
    }

    void CollectHeart(GameObject heart)
    {
        // Aquí puedes aumentar el puntaje del jugador o activar algún efecto visual
        Debug.Log("Corazón recolectado!");

        // Desactivar el corazón o destruirlo
        heart.SetActive(false); // O usa Destroy(heart);
    }
}
