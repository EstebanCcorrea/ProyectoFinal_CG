using UnityEngine;
using TMPro;
// Si usas TextMeshPro, luego cambiamos esto por using TMPro;

public class KGameManager : MonoBehaviour
{
    public static KGameManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private TMP_Text remainingText;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text itemsText;


    private int totalSites;
    private int rebuiltSites;
    private float elapsedTime;
    private bool isRunning = true;

    private void Awake()
    {
        // Singleton básico
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // Si no cambias de escena no hace falta:
        // DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Cuenta cuántos RebuildSite hay en la escena
        totalSites = FindObjectsOfType<RebuildSite>().Length;

        if (endPanel != null)
            endPanel.SetActive(false);

        UpdateRemainingText();
    }

    private void Update()
    {
        if (!isRunning) return;

        elapsedTime += Time.deltaTime;
    }

    private void UpdateRemainingText()
    {
        if (remainingText == null) return;

        int remaining = totalSites - rebuiltSites;
        remainingText.text = "Edificaciones restantes: " + remaining;
    }

    public void BuildingRebuilt()
    {
        rebuiltSites++;
        UpdateRemainingText();

        if (rebuiltSites >= totalSites)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        isRunning = false;

        if (endPanel != null)
            endPanel.SetActive(true);

        if (timeText != null)
            timeText.text = "Tiempo total: " + FormatTime(elapsedTime);

        if (itemsText != null)
        {
            // Placeholder: aquí luego pondremos los ítems reales
            int collectedItems = 0;
            itemsText.text = "Ítems recolectados: " + collectedItems;
        }

        // Opcional: pausar todo
        // Time.timeScale = 0f;
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
