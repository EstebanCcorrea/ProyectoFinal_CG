using UnityEngine;
using UnityEngine.SceneManagement; 

public class PortalInteraction : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador interactuó con el portal.");
          
            SceneManager.LoadScene("ScenaAlbarracin"); 
        }
    }
}
