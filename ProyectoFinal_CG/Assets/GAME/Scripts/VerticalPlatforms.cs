using UnityEngine;

public class VerticalPlatforms : MonoBehaviour
{
    [Header("Movimiento Vertical")]
    public float altura = 3f;       // qué tanto sube
    public float velocidad = 2f;     // velocidad del movimiento

    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        //  (sube y baja suavemente)
        float nuevoY = posicionInicial.y + Mathf.Sin(Time.time * velocidad) * altura;
        transform.position = new Vector3(transform.position.x, nuevoY, transform.position.z);
    }
}