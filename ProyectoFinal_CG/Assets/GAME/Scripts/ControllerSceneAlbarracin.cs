using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerSceneAlbarracin : MonoBehaviour
{
    [Header("Nombre de la siguiente escena")]
    public string nombreEscena;

    [Header("Referencia al Timer (arrastrar desde la escena)")]
    public TimerSimpleUI timer;

    private void OnTriggerEnter(Collider other)
    {
        // Solo reaccionar si el Player entra en el trigger
        if (other.CompareTag("Player"))
        {
            // Guardar tiempo antes de cambiar
            if (timer != null)
                timer.GuardarTiempoFinal();

            // Cambiar de escena
            SceneManager.LoadScene(nombreEscena);
        }
    }
}
