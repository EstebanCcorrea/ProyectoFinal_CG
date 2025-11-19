using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
    [Header("Puerta")]
    public Transform door;
    public Vector3 openOffset;
    public float openSpeed = 2f;
    public bool requiresKey = true;

    [Header("Sonido")]
    public AudioSource audioSource;  // AudioSource de la puerta
    public AudioClip openSound;      // Sonido al abrir

    private bool opened = false;
    private Vector3 closedPos;

    private void Start()
    {
        closedPos = door.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (opened) return;

        if (other.CompareTag("Player"))
        {
            if (requiresKey)
            {
                KeyFollowPlayer key = other.GetComponentInChildren<KeyFollowPlayer>();
                if (key == null)
                {
                    Debug.Log("El jugador no tiene la llave.");
                    return;
                }
            }

            Open();
        }
    }

    public void ActivateDoor()
    {
        if (!opened)
        {
            Open();
        }
    }

    private void Open()
    {
        opened = true;

        // **Reproducir sonido**
        if (audioSource != null && openSound != null)
        {
            audioSource.PlayOneShot(openSound);
        }

        StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor()
    {
        Vector3 targetPos = closedPos + openOffset;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * openSpeed;
            door.position = Vector3.Lerp(closedPos, targetPos, t);
            yield return null;
        }
    }
}
