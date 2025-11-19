using UnityEngine;

public class KeyFollowPlayer : MonoBehaviour
{
    [Header("Seguimiento")]
    public Vector3 offset = new Vector3(0, 0.5f, -1f);

    [Header("Sonido")]
    public AudioClip pickUpSound;       // SONIDO AL RECOGER
    private AudioSource audioSource;    // Reproductor del sonido

    private Transform player;
    private bool isFollowing = false;

    private void Start()
    {
        // Crea automáticamente un AudioSource si no existe
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // ⬅️ Reproduce sonido UNA sola vez al recolectar
            if (pickUpSound != null)
            {
                audioSource.PlayOneShot(pickUpSound);
            }

            player = other.transform;
            transform.SetParent(player);
            transform.localPosition = offset;
            isFollowing = true;

            // Desactiva físicas para que no se caiga
            if (TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }
        }
    }

    void Update()
    {
        if (isFollowing)
        {
            transform.localPosition = offset;
        }
    }
}
