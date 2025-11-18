using UnityEngine;

public class LinearPlatform : MonoBehaviour
{
    public float distance = 5f;       // Qué tan lejos se mueve
    public float speed = 2f;          // Qué tan rápido
    public bool invertMovement = false; // Actívelo en la segunda plataforma

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float phase = invertMovement ? Mathf.PI : 0f;  // 180° de diferencia
        float offset = Mathf.Sin(Time.time * speed + phase) * distance;

        // Movimiento lineal en la dirección forward
        transform.position = startPos + transform.forward * offset;
    }
}
