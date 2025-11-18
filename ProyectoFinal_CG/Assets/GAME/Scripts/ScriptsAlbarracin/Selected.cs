using UnityEngine;

public class Selected : MonoBehaviour

{
    LayerMask mask;
    public float distancia = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mask = LayerMask.GetMask("POI");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),out hit, distancia,mask))
        {
             if(hit.collider.tag == "POI")
                {
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        hit.collider.GetComponent<ObjetoInteractivo>().ActivarObjeto();
                }
            }



        }
    }
}
