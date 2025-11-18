using UnityEngine;

public class PlataformasGiratoriasLava : MonoBehaviour

{
    [Header("Rotación")]
    public float rotationSpeed = 50f;
    private bool canRotate = true;

    [Header("Movimiento")]
    public Transform targetPoint;
    public float moveSpeed = 3f;

    private Vector3 startPoint;
    private bool moveToTarget = false;
    private bool playerOnPlatform = false;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        startPoint = transform.position;
    }

    void Update()
    {
        // Rotación solo cuando esté permitido
        if (canRotate)
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    void FixedUpdate()
    {
        if (moveToTarget)
        {
            MoveTowards(targetPoint.position);

            // Si llegó al destino
            if (Vector3.Distance(transform.position, targetPoint.position) < 0.3f)
            {
                moveToTarget = false;

                // Cuando llegue, si el jugador NO está encima este regresa
                Invoke(nameof(ReturnToStart), 1f);
            }
        }
    }

    void MoveTowards(Vector3 destination)
    {
        Vector3 direction = (destination - transform.position).normalized;
        rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
    }

    void ReturnToStart()
    {
        StartCoroutine(ReturnRoutine());
    }

    System.Collections.IEnumerator ReturnRoutine()
    {
        canRotate = false; // no girar mientras vuelve

        while (Vector3.Distance(transform.position, startPoint) > 0.2f)
        {
            MoveTowards(startPoint);
            yield return null;
        }

        transform.position = startPoint;
        canRotate = true; // vuelve a girar
    }

    // Detectamos cuándo el player se sube
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Player"))
        {
            playerOnPlatform = true;

            // detener rotación y empezar movimiento
            canRotate = false;
            moveToTarget = true;
        }
    }

    // Detectamos cuándo el player se baja
    void OnCollisionExit(Collision col)
    {
        if (col.collider.CompareTag("Player"))
        {
            playerOnPlatform = false;

            // si ya no está y no estamos moviéndonos, volver a girar
            if (!moveToTarget)
            {
                canRotate = true;
                ReturnToStart();
            }
        }
    }
}


