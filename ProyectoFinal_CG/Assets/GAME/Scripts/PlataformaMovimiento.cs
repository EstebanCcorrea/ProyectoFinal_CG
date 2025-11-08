using UnityEngine;

public class PlataformaMovimiento : MonoBehaviour
{
 
    public float velocidad = 2f;
    public float altura = 2f;
    private Vector3 inicio;

    void Start()
    {
        inicio = transform.position;
    }

    void Update()
    {
        transform.position = inicio + Vector3.up * Mathf.Sin(Time.time * velocidad) * altura;
    }
}

