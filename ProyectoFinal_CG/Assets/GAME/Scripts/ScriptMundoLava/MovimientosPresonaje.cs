using UnityEngine;

public class MovimientosPresonaje : MonoBehaviour
{
    public CharacterController Controlador;
    public float Velocidad = 8f;
    public float Gravedad = -10f;
    public float Saltar = 3f;

    public Transform EnElPiso;
    public float DistaciaDelPiso = 0.4f;
    public LayerMask MascaraDelPiso;

    Vector3 VelocidadAbajo;
    bool EstaEnElPiso;

    void Start()
    {
        if (Controlador == null)
            Controlador = GetComponent<CharacterController>();
    }

    void Update()
    {
        EstaEnElPiso = Physics.CheckSphere(EnElPiso.position, DistaciaDelPiso, MascaraDelPiso);

        if (EstaEnElPiso && VelocidadAbajo.y < 0)
            VelocidadAbajo.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 mover = transform.right * x + transform.forward * z;

        if (Controlador != null && Controlador.enabled)
        {
            Controlador.Move(mover * Velocidad * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && EstaEnElPiso)
                VelocidadAbajo.y = Mathf.Sqrt(Saltar * -2f * Gravedad);

            VelocidadAbajo.y += Gravedad * Time.deltaTime;
            Controlador.Move(VelocidadAbajo * Time.deltaTime);
        }
    }
}

