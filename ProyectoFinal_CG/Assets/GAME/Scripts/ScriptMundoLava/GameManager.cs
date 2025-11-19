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
    

    private int score;
    private int totalItems;
    private int collectedItemsCount; // Renombrado para mayor claridad

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // si solo usas 1 escena, igual no estorba
    }

    private void Start()
    {
        // Contamos los CollectibleItems en la escena
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

    // Corregir error de tipografía
    public bool AllItemsCollected()
    {
        return collectedItemsCount >= totalItems && totalItems > 0; // Cambié cCollectibleItem por collectedItemsCount
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

    public void RestartScene()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }

    public void LoadScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
