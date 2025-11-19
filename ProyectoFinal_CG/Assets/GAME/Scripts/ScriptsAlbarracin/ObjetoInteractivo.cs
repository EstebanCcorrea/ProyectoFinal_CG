using UnityEngine;

public class ObjetoInteractivo : MonoBehaviour
{
    [TextArea]
    public string mensaje = "";

    [Header("Acciones")]
    public bool puedeDesaparecer = false;

    public void ActivarObjeto()
    {
        if (puedeDesaparecer)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Este objeto no desaparece, solo muestra información.");
        }
    }
}
