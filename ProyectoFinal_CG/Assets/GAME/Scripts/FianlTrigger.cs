using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinishTrigger : MonoBehaviour
{
    [Header("UI Final")]
    public GameObject panelFinal;
    public TextMeshProUGUI txtFecha;
    public TextMeshProUGUI txtTiempo;
    public TextMeshProUGUI txtScore;

    [Header("Nombre de la escena de menú")]
    public string nombreEscenaMenu = "Menu";

    [Header("Opciones")]
    public float tiempoParaVolverAlMenu = 10f; // segundos antes de volver automáticamente

    private bool terminado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (terminado) return;
        if (other.CompareTag("Player"))
        {
            terminado = true;
            MostrarResultados();
            StartCoroutine(VolverAlMenuAutomaticamente());
        }
    }

    void MostrarResultados()
    {
        // Guardar tiempo final
        TimerSimpleUI timer = FindObjectOfType<TimerSimpleUI>();
        if (timer != null)
            timer.GuardarTiempoFinal();

        // Guardar resultado en JSON
        if (GameManager1.Instance != null)
            GameManager1.Instance.GuardarResultadoEnJSON();

        // Activar panel final
        if (panelFinal != null)
            panelFinal.SetActive(true);

        // Actualizar UI
        if (txtFecha != null)
            txtFecha.text = "Fecha: " + System.DateTime.Now.ToString("dd/MM/yyyy");

        if (txtTiempo != null && GameManager1.Instance != null)
        {
            float tiempo = GameManager1.Instance.tiempoGuardado;
            int minutos = Mathf.FloorToInt(tiempo / 60f);
            int segundos = Mathf.FloorToInt(tiempo % 60f);
            txtTiempo.text = $"Tiempo: {minutos:00}:{segundos:00}";
        }

        if (txtScore != null && GameManager1.Instance != null)
            txtScore.text = "Score: " + GameManager1.Instance.totalScore;
    }

    // Coroutine que espera X segundos y luego vuelve al menú
    private IEnumerator VolverAlMenuAutomaticamente()
    {
        yield return new WaitForSeconds(tiempoParaVolverAlMenu);

        // Reiniciar variables
        if (GameManager1.Instance != null)
        {
            GameManager1.Instance.totalScore = 0;
            GameManager1.Instance.tiempoGuardado = 0f;
        }

        SceneManager.LoadScene(nombreEscenaMenu);
    }
}
