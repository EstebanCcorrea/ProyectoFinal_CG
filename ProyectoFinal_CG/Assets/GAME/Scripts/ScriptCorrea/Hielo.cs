using UnityEngine;

public class Hielo : MonoBehaviour
{
    [Header("Configuración del Hielo")]
    public PhysicsMaterial frictionice;      // El material de hielo del piso
    [Range(0f, 1f)]
    public float inertia = 0.92f;           // Qué tanto tarda en frenar
    public float slideForce = 7f;           // Fuerza del deslizamiento
    public float minSpeed = 0.1f;           // Velocidad mínima para patinar

    private CharacterController controller;
    private Vector3 extraSlideVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (IsStandingOnIce())
        {
            ApplyIceSliding();
        }
        else
        {
            extraSlideVelocity = Vector3.zero;
        }
    }

    bool IsStandingOnIce()
    {
        RaycastHit hit;

        // Raycast desde el centro del CharacterController
        Vector3 origin = transform.position + Vector3.up * 0.1f;

        if (Physics.Raycast(origin, Vector3.down, out hit, 1.2f))
        {
            Collider c = hit.collider;

            // Si el material físico del piso es el que definiste como hielo
            if (c.sharedMaterial == frictionice)
                return true;
        }

        return false;
    }

    void ApplyIceSliding()
    {
        // Obtenemos velocidad horizontal del CharacterController
        Vector3 horizontalVel = new Vector3(controller.velocity.x, 0, controller.velocity.z);

        if (horizontalVel.magnitude > minSpeed)
        {
            // Mantener inercia
            extraSlideVelocity *= inertia;

            // Empujar en dirección del movimiento actual
            extraSlideVelocity += horizontalVel.normalized * slideForce * Time.deltaTime;
        }

        // Aplicamos movimiento extra
        controller.Move(extraSlideVelocity * Time.deltaTime);
    }
}
