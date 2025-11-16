using UnityEngine;

public class MovimientoPlataforma : MonoBehaviour
{
    public float amplitude = 2f;
    public float speed = 2f;
    public bool invertMovement = false;  // Activa esto en la segunda plataforma

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float phase = invertMovement ? Mathf.PI : 0f;  // 180° de diferencia
        float offset = Mathf.Sin(Time.time * speed + phase) * amplitude;

        transform.position = new Vector3(startPos.x, startPos.y + offset, startPos.z);
    }
}
