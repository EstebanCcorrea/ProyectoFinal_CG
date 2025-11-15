using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
    public Transform door;        // la puerta
    public float openAngle = 90f; // ángulo al abrir
    public float openSpeed = 2f;  // velocidad de apertura
    public bool requiresKey = true;

    private bool opened = false;

    private void OnTriggerEnter(Collider other)
    {
        if (opened) return;

        if (other.CompareTag("Player"))
        {
            if (requiresKey)
            {
                // verifica si el player tiene la llave
                KeyFollowPlayer key = other.GetComponentInChildren<KeyFollowPlayer>();
                if (key == null)
                {
                    Debug.Log("El jugador no tiene la llave.");
                    return;
                }
            }

            StartCoroutine(OpenDoor());
            opened = true;
        }
    }

    IEnumerator OpenDoor()
    {
        Quaternion startRot = door.rotation;
        Quaternion endRot = Quaternion.Euler(0, openAngle, 0);

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * openSpeed;
            door.rotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }
    }
}
