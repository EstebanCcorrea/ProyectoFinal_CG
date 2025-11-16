using UnityEngine;

public class PushableBox : MonoBehaviour
{
    public float pushForce = 3f;   // qué tan fuerte empuja el player

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Evita que la caja se voltee
        rb.freezeRotation = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        // Solo si choca el Player
        if (!collision.collider.CompareTag("Player"))
            return;

        CharacterController controller = collision.collider.GetComponent<CharacterController>();
        if (controller == null)
            return;

        // Tomamos la dirección del movimiento del Player
        Vector3 pushDirection = new Vector3(controller.velocity.x, 0, controller.velocity.z);

        // Aplicamos movimiento a la caja
        rb.linearVelocity = pushDirection * pushForce;
    }
}
