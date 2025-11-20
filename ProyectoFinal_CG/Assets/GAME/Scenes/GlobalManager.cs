using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance { get; private set; }

    [Header("Jugador")]
    public PlayerHealth playerHealth;

    [Header("UI")]
    public Slider healthSlider;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI itemsText;
    public TextMeshProUGUI messageText;

    private int score;
    private int totalItems;
    private int collectedItemsCount;

    [Header("Música")]
    public AudioSource musicSource;  // Para música general
    public AudioClip menuMusic;
    public AudioClip levelMusic;

    [Header("Spawn")]
    public Transform playerSpawnPoint;

    [Header("KGameManager")]
    [SerializeField] private TMP_Text remainingText;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text itemsTextK;

    private int totalSites;
    private int rebuiltSites;
    private float elapsedTime;
    private bool isRunning = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);  // Eliminar duplicados
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);  // No destruir el GameManager entre escenas
    }

    private void Start()
    {
        totalItems = Object.FindObjectsOfType<CollectibleItem>().Length;  // Contar los ítems
        totalSites = FindObjectsOfType<RebuildSite>().Length;  // Contar los sitios a reconstruir
        UpdateScoreUI();
        UpdateItemsUI();
        PlayMusic(menuMusic);  // Música de menú al inicio

        if (endPanel != null)
            endPanel.SetActive(false);

        UpdateRemainingText();
    }

    public void RegisterItemCollected(int value)
    {
        score += value;
        collectedItemsCount++;  // Incrementar el contador de ítems recolectados

        UpdateScoreUI();
        UpdateItemsUI();
    }

    public void UpdateHealthUI(float current, float max)
    {
        if (healthSlider != null)
        {
            healthSlider.value = current / max;
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Puntaje: " + score;
        }
    }

    private void UpdateItemsUI()
    {
        if (itemsText != null)
        {
            itemsText.text = "Ítems: " + collectedItemsCount + " / " + totalItems;
        }
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

        if (itemsTextK != null)
        {
            // Placeholder: aquí luego pondremos los ítems reales
            int collectedItems = 0;
            itemsTextK.text = "Ítems recolectados: " + collectedItems;
        }

        // Pausar todo (si se quiere)
        // Time.timeScale = 0f;
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void PlayMusic(AudioClip music)
    {
        if (musicSource != null && music != null)
        {
            musicSource.clip = music;
            musicSource.Play();
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    // Método para obtener el punto de spawn del jugador
    public Transform GetSpawnPoint()
    {
        return playerSpawnPoint;
    }
}
