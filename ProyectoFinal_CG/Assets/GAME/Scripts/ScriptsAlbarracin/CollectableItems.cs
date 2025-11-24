using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public int value = 1;                // Valor de puntos que dará
    public AudioClip pickupSound;        // Sonido al recoger
    public float volume = 1f;            // Volumen

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager1.Instance.AddScore(value);

            // Reproducir sonido si existe
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position, volume);
            }

            Destroy(gameObject);
        }
    }
}
