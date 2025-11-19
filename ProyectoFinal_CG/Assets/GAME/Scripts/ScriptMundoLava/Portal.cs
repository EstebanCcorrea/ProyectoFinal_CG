using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.Return; // click en Enter para cmabiar de escena
    public Camera playerCamera;

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey)) 
        {
            Debug.Log("Jugador interactuó con el portal.");
            // Cambiar a la siguiente escena
            SceneManager.LoadScene("SceneAlbarracin"); 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true; // El player esta cerquita del portal
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false; // El player se fue del área de interacción
        }
    }
}