using UnityEngine;
using TMPro;

public class PoiRaycastDetector : MonoBehaviour
{
    public float distancia = 3f;
    public LayerMask poiLayer; // Capa para los puntos de interés

    public GameObject panelUI;
    public TextMeshProUGUI textoUI;

    private PoiRayInfo poiActual = null;

    void Update()
    {
        DetectarPoi();
        Interactuar();
    }

    void DetectarPoi()
    {
        Ray rayo = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(rayo, out hit, distancia, poiLayer))
        {
            poiActual = hit.collider.GetComponent<PoiRayInfo>();
        }
        else
        {
            poiActual = null;
            panelUI.SetActive(false);
        }
    }

    void Interactuar()
    {
        if (poiActual != null && Input.GetKeyDown(KeyCode.E))
        {
            panelUI.SetActive(true);
            textoUI.text = poiActual.mensaje;
        }

        if (panelUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            panelUI.SetActive(false);
        }
    }
}
