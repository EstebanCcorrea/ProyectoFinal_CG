using UnityEngine;

public class PlataformsG: MonoBehaviour
{
    [Header("Rotación")]
    public float velocidad = 40f;
    public Vector3 eje = new Vector3(0, 1, 0); // rota en el eje Y

    void Update()
    {
        transform.Rotate(eje * velocidad * Time.deltaTime);
    }
}
