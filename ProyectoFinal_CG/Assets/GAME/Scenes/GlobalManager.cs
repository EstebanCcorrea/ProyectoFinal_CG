using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance { get; private set; }

    [Header("Jugador")]
    public PlayerHealth playerHealth;
    public Transform playerSpawnPoint;

    [Header("UI General")]
    public Slider healthSlider;
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Items;
    public TextMeshProUGUI Mensaje;

    [Header("UI Final")]
    [SerializeField] private TMP_Text remainingText;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text itemsTextK;

    [Header("Música")]
    public AudioSource musicSource;
    public AudioClip menuMusic;
    public AudioClip levelMusic;

    private int score;
    private int totalItems;
    private int collectedItemsCount;

    private int totalSites;
    private int rebuiltSites;
    private float elapsedTime;
    private bool isRunning = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Solo buscar referencias si no estamos en el menú
        if (scene.name != "Menu")
        {
            playerHealth = Object.FindFirstObjectByType<PlayerHealth>();
            healthSlider = Object.FindFirstObjectByType<Slider>();
            Score = GameObject.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();
           Items = GameObject.Find("ItemsText")?.GetComponent<TextMeshProUGUI>();
           Mensaje = GameObject.Find("MessageText")?.GetComponent<TextMeshProUGUI>();

            remainingText = GameObject.Find("RemainingText")?.GetComponent<TMP_Text>();
            timeText = GameObject.Find("TimeText")?.GetComponent<TMP_Text>();
            itemsTextK = GameObject.Find("ItemsTextK")?.GetComponent<TMP_Text>();
            endPanel = GameObject.Find("EndPanel");

            playerSpawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawn")?.transform;

            totalItems = Object.FindObjectsByType<CollectibleItem>(FindObjectsSortMode.None).Length;
            totalSites = Object.FindObjectsByType<RebuildSite>(FindObjectsSortMode.None).Length;

            UpdateScoreUI();
            UpdateItemsUI();
            UpdateRemainingText();

            PlayMusic(levelMusic);
        }
        else
        {
            PlayMusic(menuMusic);
        }
    }

    private void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
        }
    }

    public void RegisterItemCollected(int value)
    {
        score += value;
        collectedItemsCount++;
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
        if (Score != null)
        {
            Score.text = "Puntaje: " + score;
        }
    }

    private void UpdateItemsUI()
    {
        if (Items != null)
        {
            Items.text = "Ítems: " + collectedItemsCount + " / " + totalItems;
        }
    }

    private void UpdateRemainingText()
    {
        if (remainingText != null)
        {
            int remaining = totalSites - rebuiltSites;
            remainingText.text = "Edificaciones restantes: " + remaining;
        }
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
            itemsTextK.text = "Ítems recolectados: " + collectedItemsCount;
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

    public Transform GetSpawnPoint()
    {
        return playerSpawnPoint;
    }
}