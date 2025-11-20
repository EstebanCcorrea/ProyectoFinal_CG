using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public enum ItemType
    {
        CristalIgneo,    // 100 pts
        NucleoFundido,   // 200 pts
        CorazonVolcanico // 300 pts
    }

    public ItemType itemType;
    public int scoreValue = 100;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Comprobamos que el GlobalManager exista
        if (GlobalManager.Instance != null)
        {
            GlobalManager.Instance.RegisterItemCollected(scoreValue);
        }
        else
        {
            Debug.LogWarning("GlobalManager.Instance es NULL al recoger item: " + name);
        }

        Destroy(gameObject);
    }
}
