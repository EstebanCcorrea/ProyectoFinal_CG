using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerSimpleUI : MonoBehaviour
{
    public TextMeshProUGUI txtMinutos;
    public TextMeshProUGUI txtSegundos;

    private float tiempo = 0f;

    private void OnEnable()
    {
        // Cargar tiempo previamente guardado
        tiempo = GameManager1.Instance != null ? GameManager1.Instance.tiempoGuardado : 0f;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            if (txtMinutos != null) txtMinutos.text = "00";
            if (txtSegundos != null) txtSegundos.text = "00";
            return;
        }

        tiempo += Time.deltaTime;

        int minutos = Mathf.FloorToInt(tiempo / 60f);
        int segundos = Mathf.FloorToInt(tiempo % 60f);

        if (txtMinutos != null)
            txtMinutos.text = minutos.ToString("00");

        if (txtSegundos != null)
            txtSegundos.text = segundos.ToString("00");

        // Guardar continuamente en GameManager
        if (GameManager1.Instance != null)
            GameManager1.Instance.tiempoGuardado = tiempo;
    }

    public void GuardarTiempoFinal()
    {
        if (GameManager1.Instance != null)
            GameManager1.Instance.GuardarTiempo(tiempo);
    }
}
