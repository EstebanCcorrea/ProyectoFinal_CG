using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Escenas")]
    public string nombreEscenaJuego = ""; 
    public string nombreEscenaMenu = "Menu";   

    // Llamar desde el botón "Jugar"
    public void Jugar()
    {
        SceneManager.LoadScene(nombreEscenaJuego);
    }

   
    public void SalirJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
