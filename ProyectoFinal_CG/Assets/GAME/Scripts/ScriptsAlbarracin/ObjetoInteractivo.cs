using UnityEngine;

public class ObjetoInteractivo : MonoBehaviour
{
    [TextArea]
    public string mensaje = "Presiona E para interactuar";

    public void ActivarObjeto()
    {
        Destroy(gameObject);
    }
}
