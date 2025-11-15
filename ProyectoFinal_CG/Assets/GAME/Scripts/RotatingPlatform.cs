using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RotatingPlatformWithPlayerRotation : MonoBehaviour
{
    public float rotationSpeed = 50f;    // grados por segundo
    public float pushMultiplier = 1f;    // cuánto arrastra la plataforma (1 = natural)

    // Seguimos a todos los players / charactercontrollers que estén sobre la plataforma
    private HashSet<Transform> players = new HashSet<Transform>();
    private HashSet<CharacterController> controllers = new HashSet<CharacterController>();

    void Reset()
    {
        // Asegurar que el collider sea trigger por defecto
        Collider c = GetComponent<Collider>();
        if (c) c.isTrigger = true;
    }

    void Update()
    {
        // Ángulo que rota esta frame
        float angle = rotationSpeed * Time.deltaTime;

        // Rotar la propia plataforma (si quieres que la mueva visualmente)
        transform.Rotate(0f, angle, 0f, Space.Self);

        // Para cada player encima, aplicar desplazamiento y rotación
        Vector3 platformPos = transform.position;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.up);

        // Usamos un array para evitar colección modificada durante iteración
        Transform[] playersArray = new Transform[players.Count];
        players.CopyTo(playersArray);

        foreach (Transform p in playersArray)
        {
            if (p == null) continue;

            CharacterController cc = p.GetComponent<CharacterController>();

            // Si hay charactercontroller, movemos por la diferencia de rotación
            if (cc != null)
            {
                // Vector desde el centro de plataforma al player (en world space)
                Vector3 rel = p.position - platformPos;

                // Aplicar rotación al vector relativo
                Vector3 relRotated = rot * rel;

                // Diferencia de posición que el player debe recibir
                Vector3 deltaPos = (relRotated - rel) * pushMultiplier;

                // Mover al player (CharacterController.Move espera unidades por frame)
                // Asegurarnos de mantener movimiento en el plano horizontal
                cc.Move(deltaPos);

                // Además rotamos el transform del player para que "gire" visualmente
                // Rotamos alrededor de su propio eje Y la misma cantidad de grados
                p.Rotate(0f, angle * pushMultiplier, 0f);
            }
            else
            {
                // Si por algún motivo el player no tiene CharacterController, fallback: RotateAround
                p.RotateAround(platformPos, Vector3.up, angle * pushMultiplier);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Transform t = other.transform;
        players.Add(t);

        CharacterController cc = other.GetComponent<CharacterController>();
        if (cc != null) controllers.Add(cc);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Transform t = other.transform;
        players.Remove(t);

        CharacterController cc = other.GetComponent<CharacterController>();
        if (cc != null) controllers.Remove(cc);
    }
}
