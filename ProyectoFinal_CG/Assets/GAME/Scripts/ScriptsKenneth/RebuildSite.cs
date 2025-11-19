using UnityEngine;
using UnityEngine.UI;

public class RebuildSite : MonoBehaviour
{
    [Header("Buildings")]
    public GameObject oldBuilding;
    public GameObject newBuilding;

    [Header("UI")]
    public GameObject rebuildPanel;
    public Button rebuildButton;

    private bool playerInside = false;
    private bool canRebuild = false;
    private bool alreadyRebuilt = false;

    private void Awake()
    {
        if (newBuilding != null) newBuilding.SetActive(false);
        if (rebuildPanel != null) rebuildPanel.SetActive(false);

        if (rebuildButton != null)
        {
            rebuildButton.onClick.RemoveAllListeners();
            rebuildButton.onClick.AddListener(OnRebuildClicked);
        }
    }

    private void Update()
    {
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

        canRebuild = true;

        if (canRebuild && rebuildPanel != null)
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
        if (!playerInside || !canRebuild || alreadyRebuilt) return;

        alreadyRebuilt = true;

        if (oldBuilding != null) oldBuilding.SetActive(false);
        if (newBuilding != null) newBuilding.SetActive(true);

        if (rebuildPanel != null)
            rebuildPanel.SetActive(false);


        if (KGameManager.Instance != null)
        {
            KGameManager.Instance.BuildingRebuilt();
        }


        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;
    }
}
