using UnityEngine;
using UnityEngine.UI;

public class RebuildSite : MonoBehaviour
{
    [Header("Buildings")]
    [SerializeField] private GameObject oldBuilding;   // Building 1
    [SerializeField] private GameObject newBuilding;   // Edificio reconstruido

    [Header("UI")]
    [SerializeField] private GameObject rebuildPanel;  // Panel con el botón
    [SerializeField] private Button rebuildButton;

    [Header("Requirements")]
    [SerializeField] private int requiredItems = 5;    // Lo que necesitarás luego

    // Aquí luego podrás guardar el inventario del jugador
    // private PlayerInventory playerInventory;

    private bool playerInside = false;

    private void Start()
    {
        // Estado inicial
        if (newBuilding != null) newBuilding.SetActive(false);
        if (rebuildPanel != null) rebuildPanel.SetActive(false);

        if (rebuildButton != null)
            rebuildButton.onClick.AddListener(OnRebuildClicked);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Más adelante:
        // playerInventory = other.GetComponentInParent<PlayerInventory>();
        // bool hasEnoughItems = playerInventory != null && playerInventory.TotalCollectables >= requiredItems;

        // POR AHORA: siempre true
        bool hasEnoughItems = true;

        playerInside = true;

        if (hasEnoughItems && rebuildPanel != null)
        {
            rebuildPanel.SetActive(true);
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
        if (!playerInside) return; // seguridad por si acaso

        if (oldBuilding != null) oldBuilding.SetActive(false);
        if (newBuilding != null) newBuilding.SetActive(true);

        if (rebuildPanel != null)
            rebuildPanel.SetActive(false);

        // Opcional: desactivar el trigger para que no se pueda reconstruir de nuevo
        GetComponent<Collider>().enabled = false;
    }
}
