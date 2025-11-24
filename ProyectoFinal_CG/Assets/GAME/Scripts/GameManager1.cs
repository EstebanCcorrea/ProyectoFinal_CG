using UnityEngine;
using System.IO;
using System.Collections.Generic;

[System.Serializable]
public class Resultado
{
    public string fecha;             // Fecha de la partida
    public float tiempo;             // Tiempo en segundos
    public string tiempoReloj;  // Tiempo en formato mm:ss
    public int score;                // Score obtenido
}

[System.Serializable]
public class ResultadoLista
{
    public List<Resultado> resultados = new List<Resultado>();
}

public class GameManager1 : MonoBehaviour
{
    public static GameManager1 Instance;

    [Header("Variables Globales")]
    public int totalScore = 0;
    public float tiempoGuardado = 0f;

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

    // Sumar score
    public void AddScore(int amount)
    {
        totalScore += amount;
        Debug.Log("Score actual = " + totalScore);
    }

    // Guardar tiempo en segundos
    public void GuardarTiempo(float tiempo)
    {
        tiempoGuardado = tiempo;
        Debug.Log("Tiempo guardado = " + tiempoGuardado);
    }

    // Guardar resultado en JSON (historial completo)
    public void GuardarResultadoEnJSON()
    {
        Resultado nuevoResultado = new Resultado();
        nuevoResultado.fecha = System.DateTime.Now.ToString("dd/MM/yyyy");
        nuevoResultado.tiempo = tiempoGuardado;

        // Convertir tiempo a formato mm:ss
        int minutos = Mathf.FloorToInt(tiempoGuardado / 60f);
        int segundos = Mathf.FloorToInt(tiempoGuardado % 60f);
        nuevoResultado.tiempoReloj = $"{minutos:00}:{segundos:00}";

        nuevoResultado.score = totalScore;

        // Ruta para StreamingAssets
        string carpeta = Path.Combine(Application.dataPath, "StreamingAssets");
        if (!Directory.Exists(carpeta))
            Directory.CreateDirectory(carpeta);

        string path = Path.Combine(carpeta, "resultados.json");

        ResultadoLista lista = new ResultadoLista();

        // Si el archivo ya existe, leer historial
        if (File.Exists(path))
        {
            string jsonExistente = File.ReadAllText(path);
            lista = JsonUtility.FromJson<ResultadoLista>(jsonExistente);
            if (lista.resultados == null)
                lista.resultados = new List<Resultado>();
        }

        // Agregar nuevo resultado
        lista.resultados.Add(nuevoResultado);

        // Guardar JSON
        string json = JsonUtility.ToJson(lista, true); // pretty print
        File.WriteAllText(path, json);

        Debug.Log("Resultado guardado en JSON: " + path);
    }
}
