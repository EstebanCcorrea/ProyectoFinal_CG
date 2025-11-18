using UnityEngine;

public class PlataformasV : MonoBehaviour

   {
    public float altura = 3f;
    public float velocidad = 2f;

    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        float nuevoY = posicionInicial.y + Mathf.Sin(Time.time * velocidad) * altura;
        transform.position = new Vector3(transform.position.x, nuevoY, transform.position.z);
    }
}
