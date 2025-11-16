using UnityEngine;

public class KeyFollowPlayer : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 0.5f, -1f); // Ajusta la posición detrás del player
    private Transform player;
    private bool isFollowing = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            transform.SetParent(player);  // Ahora la llave se vuelve hija del player
            transform.localPosition = offset; // Se coloca detrás del player
            isFollowing = true;

            // Opcional: desactiva físicas para evitar que se caiga o choque
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
            // Permite que la llave mantenga la posición detrás del player
            transform.localPosition = offset;
        }
    }
}
