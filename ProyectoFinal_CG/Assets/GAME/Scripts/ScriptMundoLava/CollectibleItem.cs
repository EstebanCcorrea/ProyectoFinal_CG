using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public enum ItemType
    {
        CristalIgneo,    // 100 pts
        NucleoFundido,   // 200 pts
        CorazonVolcanico // 300 pt

    }

    public ItemType itemType;
    public int scoreValue = 100;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Aquí se registraría el ítem recolectado
            GameManager.Instance.RegisterItemCollected(scoreValue);
            Destroy(gameObject);
        }
    }
}
