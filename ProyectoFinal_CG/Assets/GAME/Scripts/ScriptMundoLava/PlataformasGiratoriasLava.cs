using UnityEngine;

public class PlataformasGiratoriasLava : MonoBehaviour
{
    [Header("Rotación")]
    public float rotationSpeed = 50f;
    private bool canRotate = true;

    [Header("Movimiento Vertical")]
    public float altura = 5f;     // cuánto sube
    public float velocidad = 2f;  // qué tan rápido sube y baja

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
        if (subir)
        {
            rb.MovePosition(Vector3.MoveTowards(transform.position, puntoArriba, velocidad * Time.deltaTime));
        }
        else
        {
            rb.MovePosition(Vector3.MoveTowards(transform.position, puntoInicial, velocidad * Time.deltaTime));
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Player"))
        {
            playerOnPlatform = true;
            canRotate = false;   // deja de girar
            subir = true;        // empieza a subir
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.collider.CompareTag("Player"))
        {
            playerOnPlatform = false;
            subir = false;       // baja
            canRotate = true;    // vuelve a girar al llegar abajo
        }
    }
}



