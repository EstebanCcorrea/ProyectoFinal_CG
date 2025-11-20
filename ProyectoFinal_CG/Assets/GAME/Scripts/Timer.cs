using UnityEngine;
using TMPro;

public class TimerSimpleUI : MonoBehaviour
{
    [Header("Referencias UI")]
    public TextMeshProUGUI txtMinutos;
    public TextMeshProUGUI txtSegundos;

    private float tiempo;

    void Update()
    {
        tiempo += Time.deltaTime;

        int minutos = Mathf.FloorToInt(tiempo / 60f);
        int segundos = Mathf.FloorToInt(tiempo % 60f);

        // Actualizar los textos
        if (txtMinutos != null)
            txtMinutos.text = minutos.ToString("00");

        if (txtSegundos != null)
            txtSegundos.text = segundos.ToString("00");
    }
}
