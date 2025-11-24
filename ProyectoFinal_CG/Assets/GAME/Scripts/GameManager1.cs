using UnityEngine;

public class GameManager1 : MonoBehaviour
{
    public static GameManager1 Instance;

    [Header("Variables Globales")]
    public int totalScore = 0;
    public float tiempoGuardado = 0f; // 👈 Aquí se guardará el tiempo del Timer

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        totalScore += amount;
        Debug.Log("Score actual = " + totalScore);
    }

    public void GuardarTiempo(float tiempo)
    {
        tiempoGuardado = tiempo;  // 👈 SOLO guarda el tiempo, NO lo cuenta
        Debug.Log("Tiempo guardado = " + tiempoGuardado);
    }
}
