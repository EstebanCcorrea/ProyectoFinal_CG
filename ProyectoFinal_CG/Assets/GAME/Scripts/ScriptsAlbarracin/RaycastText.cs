using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    public float rayDistance = 5f;
    public LayerMask ignorePlayer; // Asignar desde el Inspector

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.red);

        if (Physics.Raycast(ray, out hit, rayDistance, ~ignorePlayer))
        {
            Debug.Log("Raycast golpeó: " + hit.collider.name);
        }
        else
        {
            Debug.Log("Nada delante");
        }
    }
}
