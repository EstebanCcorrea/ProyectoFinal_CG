using UnityEngine;

public class Hielo : MonoBehaviour
{
    [Header("Configuración del Hielo")]
    [Tooltip("Qué tanto tarda en frenar el jugador")]
    [Range(0f, 1f)]
    public float inertia = 0.93f;

    [Tooltip("Fuerza de empuje en la dirección actual")]
    [Range(0f, 20f)]
    public float glideStrength = 8f;

    [Tooltip("Suaviza el giro (estilo Mario 64)")]
    [Range(0f, 1f)]
    public float turnSmoothing = 0.12f;

    [Tooltip("Velocidad mínima para empezar a deslizar")]
    public float minSpeed = 0.1f;

    [Header("Detección")]
    public LayerMask iceLayer;  // aquí pones el layer "Hielo" del piso

    CharacterController controller;
    Vector3 iceVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Hacer raycast para detectar suelo de hielo
        if (IsOnIce())
        {
            ApplyIceMovement();
        }
        else
        {
            iceVelocity = Vector3.zero;
        }
    }

    bool IsOnIce()
    {
        RaycastHit hit;

        // raycast desde los pies del character controller
        Vector3 origin = transform.position + Vector3.up * 0.2f;

        if (Physics.Raycast(origin, Vector3.down, out hit, 1f, iceLayer))
        {
            return true;
        }

        return false;
    }

    void ApplyIceMovement()
    {
        Vector3 horizontal = new Vector3(controller.velocity.x, 0, controller.velocity.z);

        if (horizontal.magnitude < minSpeed)
            return;

        // Mantener inercia
        iceVelocity *= inertia;

        // Empuje extra en dirección del movimiento
        iceVelocity += horizontal.normalized * glideStrength * Time.deltaTime;

        // Suavizar cambio de dirección
        iceVelocity = Vector3.Lerp(iceVelocity, horizontal.normalized * iceVelocity.magnitude, turnSmoothing);

        // Aplicar movimiento sin romper al controller
        controller.Move(iceVelocity * Time.deltaTime);
    }
}
