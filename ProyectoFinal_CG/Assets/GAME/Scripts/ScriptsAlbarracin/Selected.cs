using UnityEngine;
using TMPro;

public class Selected : MonoBehaviour
{
    public float distancia = 1.5f;
    private LayerMask mask;

    [Header("UI de Interacción")]
    public GameObject panelInteraccion;
    public TextMeshProUGUI textoInteraccion;

    private bool mostrandoUI = false;

    void Start()
    {
        mask = LayerMask.GetMask("POI");
        panelInteraccion.SetActive(false);
    }

    void Update()
    {
        RaycastHit hit;
        int ignorePlayer = ~LayerMask.GetMask("Player");

        if (Physics.Raycast(transform.position, transform.forward, out hit, distancia, mask & ignorePlayer))
        {
            if (hit.collider.CompareTag("ObjetoInteractivo"))
            {
                ObjetoInteractivo obj = hit.collider.GetComponent<ObjetoInteractivo>();

                if (obj != null)
                {
                    
                    textoInteraccion.text = obj.mensaje;

                    if (!mostrandoUI)
                    {
                        panelInteraccion.SetActive(true);
                        mostrandoUI = true;
                    }

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        obj.ActivarObjeto();
                        panelInteraccion.SetActive(false);
                        mostrandoUI = false;
                    }
                }

                return;
            }
        }

        if (mostrandoUI)
        {
            panelInteraccion.SetActive(false);
            mostrandoUI = false;
        }
    }
}
