using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string nombreEscenaJuego = "Nivel1"; // Cambia por tu escena

    public void Jugar()
    {
        SceneManager.LoadScene(nombreEscenaJuego);
    }
}
