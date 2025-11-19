using UnityEngine;
using UnityEngine.UI;

public class RebuildSite : MonoBehaviour
{
    [Header("Buildings")]
    public GameObject oldBuilding;   // Building 1
    public GameObject newBuilding;   // Nueva casa

    [Header("UI")]
    public GameObject rebuildPanel;  // Panel con el texto/botón
    public Button rebuildButton;     // Botón Re-Build (opcional)

    private bool playerInside = false;
    private bool canRebuild = false;
    private bool alreadyRebuilt = false;

    private void Awake()
    {
        if (newBuilding != null) newBuilding.SetActive(false);
        if (rebuildPanel != null) rebuildPanel.SetActive(false);

        // El botón sigue llamando al mismo método (por si luego lo quieres usar)
        if (rebuildButton != null)
        {
            rebuildButton.onClick.RemoveAllListeners();
            rebuildButton.onClick.AddListener(OnRebuildClicked);
        }
    }

    private void Update()
    {
        // Reconstruir con la tecla R
        if (playerInside && canRebuild && !alreadyRebuilt)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                OnRebuildClicked();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || alreadyRebuilt) return;

        playerInside = true;

        // Más adelante aquí metes la condición real de ítems
        canRebuild = true; // por ahora siempre true

        if (canRebuild && rebuildPanel != null)
        {
            rebuildPanel.SetActive(true);   // Mostrar panel de “Rebuild (R)”
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInside = false;

        if (rebuildPanel != null)
            rebuildPanel.SetActive(false);
    }

    private void OnRebuildClicked()
    {
        if (!playerInside || !canRebuild || alreadyRebuilt) return;

        alreadyRebuilt = true;

        if (oldBuilding != null) oldBuilding.SetActive(false);
        if (newBuilding != null) newBuilding.SetActive(true);

        if (rebuildPanel != null)
            rebuildPanel.SetActive(false);

        // Desactivar el trigger para no usarlo otra vez
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;
    }
}
