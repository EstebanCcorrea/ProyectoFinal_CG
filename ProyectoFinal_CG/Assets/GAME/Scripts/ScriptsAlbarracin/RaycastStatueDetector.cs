using UnityEngine;

public class RaycastStatueDetector : MonoBehaviour
{
    public float detectionDistance = 6f;    // Distancia máxima
    public LayerMask detectionMask;         // Capa donde está la estatua
    public DoorTrigger door;                // Referencia a la puerta

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, detectionDistance, detectionMask))
        {
            if (hit.collider.CompareTag("Statue"))
            {
                door.ActivateDoor();
            }
        }
    }
}
