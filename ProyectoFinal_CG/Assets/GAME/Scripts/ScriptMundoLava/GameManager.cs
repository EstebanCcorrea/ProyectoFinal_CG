using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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
    public Transform GetSpawnPoint()
    {
        return playerSpawnPoint; 
    }
    private void Start()
    {
        totalItems = Object.FindObjectsByType<CollectibleItem>(FindObjectsSortMode.None).Length;
        UpdateScoreUI();
        UpdateItemsUI();
    }

    public void RegisterItemCollected(int value)
    {
        score += value;
        collectedItemsCount++; // Incrementar el contador de ítems recolectados

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

    public bool AllItemsCollected()
    {
        return collectedItemsCount >= totalItems && totalItems > 0;
    }

    public void ShowMessage(string msg, float duration = 2f)
    {
        if (messageText == null) return;

        messageText.gameObject.SetActive(true);
        messageText.text = msg;
        CancelInvoke(nameof(HideMessage));
        Invoke(nameof(HideMessage), duration);
    }

    private void HideMessage()
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }
    }

    public Transform playerSpawnPoint;

    public void RestartScene()
    {
        
        if (playerSpawnPoint != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = playerSpawnPoint.position;
            player.transform.rotation = playerSpawnPoint.rotation;
            player.GetComponent<PlayerHealth>().RestoreHealth();  // Restaurar vida
        }

        // Luego reinicia la escena
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void LoadScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
