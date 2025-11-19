using UnityEngine;

public class PlataformasGiratoriasLava : MonoBehaviour
{
    [Header("Rotación")]
    public float rotationSpeed = 50f;
    private bool canRotate = true;

    [Header("Movimiento Vertical")]
    public float altura = 5f;     // cuánto sube
    public float velocidad = 2f;  // qué tan rápido sube y baja
    public float alturaMaxima = 10f; // Altura máxima a la que debe llegar la plataforma

    private Vector3 puntoInicial;
    private Vector3 puntoArriba;
    private bool subir = false;
    private bool playerOnPlatform = false;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        puntoInicial = transform.position;
        puntoArriba = puntoInicial + new Vector3(0, altura, 0);
    }

    void Update()
    {
        if (canRotate)
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    void FixedUpdate()
    {
        // Movimiento vertical de la plataforma
        if (subir && transform.position.y < alturaMaxima)
        {
            rb.MovePosition(Vector3.MoveTowards(transform.position, puntoArriba, velocidad * Time.deltaTime));
        }
        else if (!subir && transform.position.y > puntoInicial.y)
        {
            rb.MovePosition(Vector3.MoveTowards(transform.position, puntoInicial, velocidad * Time.deltaTime));
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Player"))
        {
            playerOnPlatform = true;
            canRotate = false;   // Detener la rotación mientras sube
            subir = true;        // Empieza a subir
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.collider.CompareTag("Player"))
        {
            playerOnPlatform = false;
            subir = false;       // Baja la plataforma
            canRotate = true;    // Vuelve a girar al llegar abajo
        }
    }
}
